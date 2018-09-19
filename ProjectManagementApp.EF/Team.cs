namespace ProjectManagementApp.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Team
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Team()
        {
            Employees = new HashSet<Employee>();
        }

        public int Id { get; set; }

        //[Required]
        //[StringLength(50)]
        //public string Name { get; set; }

        [StringLength(50)]
        public string Description { get; set; }

        //[Column(TypeName = "date")]
        //public DateTime StartDate { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ExpectedEnd { get; set; }

        public int? ProjectId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Employee> Employees { get; set; }

        public virtual Project Project { get; set; }
    }
}
