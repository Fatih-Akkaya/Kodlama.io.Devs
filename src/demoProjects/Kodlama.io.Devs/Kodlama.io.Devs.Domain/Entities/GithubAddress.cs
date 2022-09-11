using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kodlama.io.Devs.Domain.Entities
{
    public class GithubAddress : Entity
    {       

        public int UserId { get; set; }
        public string GithubUrl { get; set; }
        public GithubAddress()
        {
        }

        public GithubAddress(int id, int userId, string githubUrl)
        {
            Id = id;
            UserId = userId;
            GithubUrl = githubUrl;
        }
    }
}
