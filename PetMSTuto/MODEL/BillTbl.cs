namespace PetMSTuto.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BillTbl")]
    public partial class BillTbl
    {
        [Key]
        public int BNum { get; set; }

        [Column(TypeName = "date")]
        public DateTime BDate { get; set; }

        public int CustId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustName { get; set; }

        [Required]
        [StringLength(50)]
        public string PrName { get; set; }

        public int Amt { get; set; }
    }
}
