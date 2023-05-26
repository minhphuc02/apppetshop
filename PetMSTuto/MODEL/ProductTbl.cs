namespace PetMSTuto.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ProductTbl")]
    public partial class ProductTbl
    {
        [Key]
        public int PrId { get; set; }

        [Required]
        [StringLength(50)]
        public string PrName { get; set; }

        [Required]
        [StringLength(20)]
        public string PrCat { get; set; }

        public int PrQty { get; set; }

        public int PrPrice { get; set; }
    }
}
