using Core.CrossCuttingConcerns.Exceptions;
using Core.Persistence.Paging;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Application.Features.Technologies.Rules
{
    public class TechnologyBusinessRules
    {
        private readonly ITechnologyRepository _technologyRepository;

        public TechnologyBusinessRules(ITechnologyRepository technologyRepository)
        {
            _technologyRepository = technologyRepository;
        }

        public async Task TechnologyNameCanNotBeDuplicatedWhenInserted(string name)
        {
            IPaginate<Technology> result = await _technologyRepository.GetListAsync(b => b.Name == name);
            if (result.Items.Any()) throw new BusinessException("Technology name already exists.");
        }

        public void TechnologyShouldExistWhenRequested(Technology technology)
        {
            if (technology == null) throw new BusinessException("Request Technology does not exist.");
        }
    }
}
