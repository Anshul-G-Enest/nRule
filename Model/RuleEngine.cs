using Rule.WebAPI.Model.DTO;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Rule.WebAPI.Model
{
    public class RuleEngine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int Id { get; set; }
        [Required]
        public string PropertyName { get; set; }
        [Required]
        public string Value { get; set; }
        public string SecondValue { get; set; }
        public int ConnectorId { get; set; }
        [ForeignKey("ConnectorId")]
        public virtual StatementConnector Connector { get; set; }
        public int OperationId { get; set; }
        [ForeignKey("OperationId")]
        public virtual Operation Operation { get; set; }
        public int? NRuleId { get; set; }
        [ForeignKey("NRuleId")]
        public virtual NRule NRule { get; set; }
    }
}
