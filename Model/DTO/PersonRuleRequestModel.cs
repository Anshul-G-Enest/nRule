using System;
using System.Collections.Generic;

namespace Rule.WebAPI.Model.DTO
{
    public class PersonRuleRequestModel
    {
        public List<int> RuleIds { get; set; }
        public PersonRequestModel Person { get; set; }
    }
    public class PersonRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsMale { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age { get; set; }
    }
}
