using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class ProfileService
    {
        private ApplicationDbContext dbContext;
        private IProfileRepository profileRepository;

        public ProfileService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.profileRepository = new ProfileRepository(this.dbContext);
        }
        public Profile getProfile(string id)
        {
            return profileRepository.getProfile(id);
        }
        public void insertProfile(Profile profile)
        {
            dbContext.Profile.Add(profile);
            Save();
        }
        public void updateProfile(Profile profile)
        {
            dbContext.Profile.Update(profile);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
