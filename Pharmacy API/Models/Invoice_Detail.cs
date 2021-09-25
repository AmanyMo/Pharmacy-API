namespace Pharmacy_API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using Newtonsoft.Json;

    [Table("Invoice Details")]
    public partial class Invoice_Detail
    {
        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Invoice_ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Medicine_ID { get; set; }

        public int? Quantity { get; set; }

        [StringLength(10)]
        public string Price { get; set; }
        [JsonIgnore]

        public virtual Invoice Invoice { get; set; }
        [JsonIgnore]

        public virtual Medicine Medicine { get; set; }
    }
}
