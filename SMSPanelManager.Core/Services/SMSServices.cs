using Microsoft.EntityFrameworkCore;
using SMSPanelManager.Data.Context;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Humanizer;

namespace SMSPanelManager.Core.Services
{
    public class SMSServices : ISMSService
    {
        private readonly SMSPanelContext _context;

        public SMSServices(SMSPanelContext context)
        {
            _context = context;
        }

        public string ProcessInput(string input)
        {
            string[] words = input.Split(' ');
            StringBuilder result = new StringBuilder();

            foreach (string word in words)
            {
                string cleanedWord = CleanWord(word);
                result.Append(cleanedWord).Append(" ");
            }

            return result.ToString().Trim();
        }

        public string CleanWord(string word)
        {
            StringBuilder cleanedWord = new StringBuilder();
            foreach (char character in word)
            {
                if (char.IsLetter(character) || char.IsDigit(character))
                {
                    cleanedWord.Append(character);
                }
            }
            return cleanedWord.ToString();
        }

        public string[] ExtractItems(string input)
        {
            var humanizedItem = input.Humanize();
            var products = _context.Products.ToList();
            List<string> items = new List<string>();

            string[] words = humanizedItem.Split(' ');
            bool isRecording = false;
            string currentItem = "";

            foreach (string word in words)
            {
                var matchingProducts = products.Where(p => p.ProductName.Contains(word) || word.Contains(p.ProductName)).ToList();

                if (matchingProducts.Any() && !isRecording)
                {
                    isRecording = true;
                    currentItem = word;
                }
                else if (isRecording && int.TryParse(word, out _))
                {
                    currentItem += " " + word;
                    items.Add(currentItem);
                    isRecording = false;
                }
                else if (isRecording)
                {
                    currentItem += " " + word;
                }
            }

            return items.ToArray();
        }

        public void AddNewSMS(SMS sms)
        {
            _context.SMSs.Add(sms);
            _context.SaveChanges();

            var latestSMS = _context.SMSs.OrderByDescending(c => c.SMSId).FirstOrDefault();
            string processedText = ProcessInput(latestSMS.Text);
            string[] extractedItems = ExtractItems(processedText);


            var printableSMS = new PrinteableSMS
            {
                PhoneNumber = latestSMS.Sender,
                SMSText = latestSMS.Text,
                SMSId = latestSMS.SMSId,


            };

            _context.PrinteableSMs.Add(printableSMS);
            _context.SaveChanges();
            var contact = _context.Contacts.FirstOrDefault(c => c.PhoneNumber == printableSMS.PhoneNumber);
            if (contact != null)
            {
                printableSMS.ContactId = contact.ContactId;
                _context.PrinteableSMs.Update(printableSMS);
                _context.SaveChanges();
            }
            foreach (var item in extractedItems)
            {
                ParseProductDetails(item, printableSMS.Id);
            }
        }
        /// <summary>
        /// //////////////////////////////////////////////////////////////////////////////
        /// This is second algu
        /// </summary>
        //public void AddNewSMS(SMS sms)
        //{
        //    _context.SMSs.Add(sms);
        //    _context.SaveChanges();
        //    // var humanizedItem = item.Humanize();
        //    var latestSMS = _context.SMSs.OrderByDescending(c => c.SMSId).FirstOrDefault();
        //    var printableSMS = new PrinteableSMS
        //    {
        //        PhoneNumber = latestSMS.Sender,
        //        SMSText = latestSMS.Text,
        //        SMSId = latestSMS.SMSId,


        //    };
        //    _context.PrinteableSMs.Add(printableSMS);
        //    _context.SaveChanges();
        //    var contact = _context.Contacts.FirstOrDefault(c => c.PhoneNumber == printableSMS.PhoneNumber);
        //    if (contact != null)
        //    {
        //        printableSMS.ContactId = contact.ContactId;
        //        _context.PrinteableSMs.Update(printableSMS);
        //        _context.SaveChanges();
        //    }

        //    //This Place is made for extracting data from SMSs Text
        //    string HumanizedText = printableSMS.SMSText.Humanize();
        //    string[] StringTextArray = HumanizedText.Split(' ');
        //    var Products = _context.Products.ToList();
        //    List<string> ProdusctNames = new List<string>();
        //    int ProductCount = 0;
        //    for (int i = 0; i < StringTextArray.Length; i++)
        //    {
        //        ProdusctNames = new List<string>();
        //        ProductCount = 0;
        //        if (Products.Any(x => x.ProductName == StringTextArray[i]))
        //        {
        //            for (int j = i; j < StringTextArray.Length; j++)
        //            {
        //                try
        //                {
        //                    ProductCount = Convert.ToInt32(StringTextArray[j]);
        //                    i += 1;
        //                    break;

        //                }
        //                catch
        //                {
        //                    ProdusctNames.Add(StringTextArray[j].ToString());
        //                    i += 1;
        //                }
        //            }
        //            string Name = string.Join(" ", ProdusctNames).ToString();
        //            _context.OrderedProducts.Add(new OrderedProducts()
        //            {
        //                Count = ProductCount,
        //                OrderedProduct = Name,
        //                PrintableSMSId = printableSMS.Id
        //            });
        //        }
        //        _context.SaveChanges();
        //    }

        //    /////////////////////////////////////////////////////////
        //}


        //////////////////////////////////////////////////////////////////////////////////
        private void ParseProductDetails(string item, int printableSMSId)
        {
            // تجزیه و تحلیل نام و تعداد محصول با استفاده از Humanizer.Core.fa
            var humanizedItem = item.Humanize();
            var splitItem = humanizedItem.Split(' ');

            if (splitItem.Length >= 2)
            {
                string name = string.Join(" ", splitItem.Take(splitItem.Length - 1)).Trim();
                if (int.TryParse(splitItem.Last(), out int count))
                {
                    _context.OrderedProducts.Add(new OrderedProducts
                    {
                        Count = count,
                        OrderedProduct = name,
                        PrintableSMSId = printableSMSId

                    });

                    _context.SaveChanges();
                }
            }
        }

        public List<PrinteableSMS> GetSMSList()
        {
            IQueryable<PrinteableSMS> sms = _context.PrinteableSMs;
            sms = sms.OrderByDescending(x => x.CreatedTime);
            sms = sms.Include(x => x.Contact);
            return sms.ToList();
        }
        public Tuple<List<OrderedProducts>, List<PrinteableSMS>> GetSMSsToPrint()
        {
         
                IQueryable<PrinteableSMS> sms = _context.PrinteableSMs;
                sms = sms.OrderByDescending(x => x.CreatedTime);
                sms = sms.Include(x => x.Contact);
                var orderedproducts = _context.OrderedProducts.ToList();
                return Tuple.Create(orderedproducts, sms.ToList());
            
      
           

        }

        public void DeleteSMSs(string Type)
        {
            switch (Type)
            {
                case "All":
                    foreach (var item in _context.OrderedProducts)
                    {
                        _context.OrderedProducts.Remove(item);
                   
                    }
                    foreach (var item in _context.PrinteableSMs)
                    {
                        _context.PrinteableSMs.Remove(item); 
                       
                    }
                    foreach (var item in _context.SMSs)
                    {
                        _context.SMSs.Remove(item);
                        
                    }
                    _context.SaveChanges();
                    break;
            }
        }

        public Tuple<List<OrderedProducts>, PrinteableSMS> GetSmsToPrint(int SmsiD)
        {

            var sms = _context.PrinteableSMs.Include(x=>x.Contact).FirstOrDefault(x => x.Id == SmsiD);
            var orderedproducts = _context.OrderedProducts.Where(c=>c.PrintableSMSId == sms.Id).ToList();
            return Tuple.Create(orderedproducts, sms);

        }

        public void MakeASMSPrinted(int Id)
        {
            var sms = _context.PrinteableSMs.FirstOrDefault(x => x.Id == Id);
            if (sms != null)
            {
                sms.IsPrinted = true;
                _context.PrinteableSMs.Update(sms);
                _context.SaveChanges();
            }

        }
    }
}
