using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private AddressService addressService;
        public AddressController(AddressService addressService)
        {
            this.addressService = addressService;
        }
        [HttpPost("insert-address")]
        public IActionResult insertAddress(InsertAddress inAddress)
        {
            Address address = new Address();
            address.Id = Guid.NewGuid();
            address.UserId = inAddress.UserId;
            address.Name = inAddress.Name;
            address.Addres = inAddress.Addres;
            address.Phone = inAddress.Phone;
            address.Email = inAddress.Email;
            List<Address> addresslist = addressService.getAddressList(inAddress.UserId);
            if(addresslist == null || addresslist.Count == 0)
            {
                address.IsDefault = true;
            } 
            else if (inAddress.IsDefault)
            {
                foreach(Address address1 in addresslist)
                {
                    address1.IsDefault = false;
                    addressService.updateAddress(address1);
                }
                address.IsDefault = true;
            }
            addressService.insertAddress(address);
            return Ok(new { status = true, message = "success"});
        }
        [HttpPost("update-address")]
        public IActionResult updateAddress(Address upAddress)
        {
            Address address = addressService.getAddress(upAddress.Id);
            address.UserId = upAddress.UserId;
            address.Name = upAddress.Name;
            address.Addres = upAddress.Addres;
            address.Phone = upAddress.Phone;
            address.Email = upAddress.Email;
            address.IsDefault = upAddress.IsDefault;
            addressService.updateAddress(address);
            if(upAddress.IsDefault)
            {
                List<Address> addresslist = addressService.getAddressList(upAddress.UserId);
                if(addresslist.Count > 1)
                {
                    foreach(Address address1 in addresslist)
                    {
                        if(address1.Id != address.Id) {
                            address1.IsDefault = false;
                            addressService.updateAddress(address1);
                        }
                    }
                }
            }
            return Ok(new { status = true, message = "success" });
        }
        [HttpGet("get-sl-address-isdefault")]
        public IActionResult getSlAddressIsDefault(string uid)
        {
            int sl = 0;
            List<Address> addresslist = addressService.getAddressList(uid);
            foreach(Address address in addresslist)
            {
                if (address.IsDefault)
                {
                    sl++;
                }
            }
            return Ok(new { status = true, message = "success", data = sl });
        }
        [HttpGet("get-address-list")]
        public IActionResult getAddressList(string uid)
        {
            List<Address> addresslist = addressService.getAddressList(uid);
            return Ok(new { status = true, message = "success", data = addresslist });
        }
        [HttpGet("get-address-isdefault")]
        public IActionResult getAddressIsDefault(string uid)
        {
            List<Address> addresslist = addressService.getAddressList(uid);
            if(addresslist.Count == 0){
                return Ok(new { status = false, message = "failed" });
            } else {
                Address addre = addressService.getAddressIsDefault(uid);
                AddressInOrder addressInOrder = new AddressInOrder();
                addressInOrder.Name = addre.Name;
                addressInOrder.Phone = addre.Phone;
                addressInOrder.Addres = addre.Addres;
                return Ok(new { status = true, message = "success", data = addressInOrder });
            }
        }
        [HttpGet("delete-address")]
        public IActionResult getAddressList(Guid id)
        {
            Address address = addressService.getAddress(id);
            addressService.deleteAddress(address);
            return Ok(new { status = true, message = "success"});
        }
    }
}
