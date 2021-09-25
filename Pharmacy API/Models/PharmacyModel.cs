using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Pharmacy_API.Models
{
    public partial class PharmacyModel : DbContext
    {
        public PharmacyModel()
            : base("name=Pharmacy_Model")
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Invoice_Detail> Invoice_Details { get; set; }
        public virtual DbSet<Medicine> Medicines { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>()
                .Property(e => e.Salary)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Invoices)
                .WithRequired(e => e.Employee)
                .HasForeignKey(e => e.Emp_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice>()
                .Property(e => e.Total)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Invoice>()
                .HasMany(e => e.Invoice_Details)
                .WithRequired(e => e.Invoice)
                .HasForeignKey(e => e.Invoice_ID)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Invoice_Detail>()
                .Property(e => e.Price)
                .IsFixedLength();

            modelBuilder.Entity<Medicine>()
                .Property(e => e.price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Medicine>()
                .HasMany(e => e.Invoice_Details)
                .WithRequired(e => e.Medicine)
                .HasForeignKey(e => e.Medicine_ID)
                .WillCascadeOnDelete(false);
        }
    }
}
