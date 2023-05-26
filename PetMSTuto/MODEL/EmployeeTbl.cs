namespace PetMSTuto.MODEL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("EmployeeTbl")]
    public partial class EmployeeTbl
    {
        [Key]
        public int EmpNum { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpName { get; set; }

        [Required]
        [StringLength(100)]
        public string EmpAdd { get; set; }

        [Column(TypeName = "date")]
        public DateTime EmpDOB { get; set; }

        [Required]
        [StringLength(50)]
        public string EmpPhone { get; set; }

        [Required]
        [StringLength(20)]
        public string EmpPass { get; set; }
    }
}
