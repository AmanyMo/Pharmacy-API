namespace Pharmacy_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Invoice")]
    public partial class Invoice
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Invoice()
        {
            Invoice_Details = new HashSet<Invoice_Detail>();
        }

        public int ID { get; set; }

        public DateTime Date { get; set; }

        public int Emp_ID { get; set; }

        [Column(TypeName = "money")]
        public decimal? Total { get; set; }

        [JsonIgnore]
        public virtual Employee Employee { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [JsonIgnore]

        public virtual ICollection<Invoice_Detail> Invoice_Details { get; set; }
    }
}
