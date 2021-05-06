using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NRules;
using Rule.WebAPI.Context;
using Rule.WebAPI.Model;
using Rule.WebAPI.Model.DTO;
using Rule.WebAPI.Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Rule.WebAPI.Services
{
    public class PersonData : IPersonData
    {
        private readonly RuleDbContext _ruleDbContext;
        private readonly IMapper _mapper;
        public PersonData(RuleDbContext ruleDbContext, IMapper mapper)
        {
            _ruleDbContext = ruleDbContext;
            _mapper = mapper;
        }

        public async Task<bool> SavePerson(PersonRequestModel personRequestModel)
        {
            return await AddPerson(personRequestModel);
        }

        private async Task<bool> AddPerson(PersonRequestModel personRequestModel)
        {
            _ruleDbContext.Persons.Add(_mapper.Map<Model.Person>(personRequestModel));

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdatePerson(PersonRequestModel personRequestModel)
        {
            var person = await _ruleDbContext.Persons.FirstOrDefaultAsync(x => x.Id == personRequestModel.Id);
            if (person == null)
                return false;

            _ruleDbContext.Persons.Update(_mapper.Map(personRequestModel, person));

            return await _ruleDbContext.SaveChangesAsync() > 0;
        }

        public async Task<PersonRequestModel> GetPerson(int personId)
        {
            return await _mapper.ProjectTo<PersonRequestModel>(_ruleDbContext.Persons.Where(x => x.Id == personId)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<PersonRequestModel>> GetPersons()
        {
            return await _mapper.ProjectTo<PersonRequestModel>(_ruleDbContext.Persons).ToListAsync();
        }

    }
}
