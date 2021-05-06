using Rule.WebAPI.Model.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services.Interface
{
    public interface IPersonData
    {
        Task<bool> SavePerson(PersonRequestModel personRuleRequestModel);
        Task<PersonRequestModel> GetPerson(int personId);
        Task<IEnumerable<PersonRequestModel>> GetPersons();
        Task<bool> UpdatePerson(PersonRequestModel personRequestModel);
    }
}
