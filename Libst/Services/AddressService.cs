using Libs.Entity;
using Libs.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Libs.Services
{
    public class AddressService
    {
        private ApplicationDbContext dbContext;
        private IAddressRepository addressRepository;

        public AddressService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.addressRepository = new AddressRepository(this.dbContext);
        }
        public List<Address> getAddressList(string uid)
        {
            return addressRepository.getAddressList(uid);
        }
        public Address getAddress(Guid id)
        {
            return addressRepository.getAddress(id);    
        }
        public Address getAddressIsDefault(string uid)
        {
            return addressRepository.getAddressIsDefault(uid);
        }
        public void insertAddress(Address address)
        {
            dbContext.Address.Add(address);
            Save();
        }
        public void updateAddress(Address address)
        {
            dbContext.Address.Update(address);
            Save();
        }
        public void deleteAddress(Address address)
        {
            dbContext.Address.Remove(address);
            Save();
        }
        public void Save()
        {
            this.dbContext.SaveChanges();
        }
    }
}
