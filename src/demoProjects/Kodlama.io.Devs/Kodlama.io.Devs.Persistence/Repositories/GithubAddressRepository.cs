using Core.Persistence.Repositories;
using Kodlama.io.Devs.Application.Services.Repositories;
using Kodlama.io.Devs.Domain.Entities;
using Kodlama.io.Devs.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Persistence.Repositories
{
    public class GithubAddressRepository : EfRepositoryBase<GithubAddress, BaseDbContext>, IGithubAddressRepository
    {
        public GithubAddressRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
