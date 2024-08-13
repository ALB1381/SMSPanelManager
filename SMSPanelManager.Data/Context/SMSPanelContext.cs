using Microsoft.EntityFrameworkCore;
using SMSPanelManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace SMSPanelManager.Data.Context
{
    public class SMSPanelContext : DbContext
    {
        public SMSPanelContext(DbContextOptions<SMSPanelContext> options) : base(options)
        {
        }

        public DbSet<SMS> SMSs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<PrinteableSMS> PrinteableSMs { get; set; }

        public DbSet<Products> Products { get; set; }
        public DbSet<OrderedProducts> OrderedProducts { get; set; }

        public DbSet<Contact> Contacts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
         .SelectMany(t => t.GetForeignKeys())
         .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;
            modelBuilder.Entity<User>().HasData(
  new User() { UserName="<Awnm#|$|%!@ll*$>",UserPassword="!!<Q!0!@9f|j|f&#$>!!",UserId=1 }
  );

            modelBuilder.Entity<Products>().HasData(
                new Products() { ProductId = 1, ProductName = "تره" },
                new Products() { ProductId = 2, ProductName = "جعفری" },
                new Products() { ProductId = 3, ProductName = "گشنیز" },
                new Products() { ProductId = 4, ProductName = "اسفناج" },
                new Products() { ProductId = 5, ProductName = "شوید" },
                new Products() { ProductId = 6, ProductName = "شنبلیله" },
                new Products() { ProductId = 7, ProductName = "سیر" },
                new Products() { ProductId = 8, ProductName = "باقالی" },
                new Products() { ProductId = 9, ProductName = "لوبیا" },
                new Products() { ProductId = 10, ProductName = "بادمجان" },
                new Products() { ProductId = 11, ProductName = "سبزی" },
                new Products() { ProductId = 12, ProductName = "بامیه" },
                new Products() { ProductId = 13, ProductName = "مخلوط" },
                new Products() { ProductId = 14, ProductName = "نخود" },
                new Products() { ProductId = 15, ProductName = "ذرت" },
                new Products() { ProductId = 16, ProductName = "رب" },
                new Products() { ProductId = 17, ProductName = "خیار" },
                new Products() { ProductId = 18, ProductName = "زیتون" },
                new Products() { ProductId = 19, ProductName = "پیاز" },
                new Products() { ProductId = 20, ProductName = "نعنا" },
                new Products() { ProductId = 21, ProductName = "ترخون" },
                new Products() { ProductId = 22, ProductName = "مرزه" },
                new Products() { ProductId = 23, ProductName = "تیغ" },
                new Products() { ProductId = 24, ProductName = "شور" },
                new Products() { ProductId = 25, ProductName = "ترشی" },
                new Products() { ProductId = 26, ProductName = "فلفل" },
                new Products() { ProductId = 27, ProductName = "سس" },
                new Products() { ProductId = 28, ProductName = "برگ" }
                );
            
        }
    }
}
