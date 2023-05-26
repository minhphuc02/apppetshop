namespace PetMSTuto.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CustomerTbl")]
    public partial class CustomerTbl
    {
        [Key]
        public int CustId { get; set; }

        [Required]
        [StringLength(50)]
        public string CustName { get; set; }

        [Required]
        [StringLength(50)]
        public string CustAdd { get; set; }

        [Required]
        [StringLength(50)]
        public string CustPhone { get; set; }
    }
}
