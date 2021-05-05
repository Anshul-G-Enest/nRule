using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rule.WebAPI.Model.DTO
{
    public class NRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; }
        public virtual ICollection<RuleEngine> RuleEngines { get; set; }
    }
}
