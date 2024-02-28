using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {
        public Profile getProfile(string uid);
    }
    public class ProfileRepository : RepositoryBase<Profile>, IProfileRepository
    {
        public ProfileRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public Profile getProfile(string id)
        {
            return _dbContext.Profile.FirstOrDefault(x=>x.UserId == id);
        }
    }
}
