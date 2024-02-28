using Libs.Data;
using Libs.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Repositories
{
    public interface IAddressRepository : IRepository<Address>
    {
        public List<Address> getAddressList(string uid);
        public Address getAddress(Guid id);
        public Address getAddressIsDefault(string uid);
    }
    public class AddressRepository : RepositoryBase<Address>, IAddressRepository
    {
        public AddressRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public List<Address> getAddressList(string uid)
        {
            return _dbContext.Address.Where(x=>x.UserId == uid).ToList();
        }
        public Address getAddress(Guid id)
        {
            return _dbContext.Address.FirstOrDefault(x=>x.Id == id);
        }
        public Address getAddressIsDefault(string uid)
        {
            return _dbContext.Address.FirstOrDefault(x=>x.UserId == uid && x.IsDefault == true);
        }
    }
}
