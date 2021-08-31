using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Company.API.Persistence.Entities
{
    [Table("Company")]
    public class Company
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CompanyId { get; set; }
        public virtual ICollection<History> History { get; set; }
    }
}
