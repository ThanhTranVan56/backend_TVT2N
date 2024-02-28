using backend_TVT2N.Models;
using Libs.Entity;
using Libs.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend_TVT2N.Controllers.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private ProfileService profileService;
        public ProfileController(ProfileService profileService)
        {
            this.profileService = profileService;
        }
        [HttpGet("get-profile")]
        public IActionResult getProfile(string uid)
        {
            Profile profile = profileService.getProfile(uid);
            return Ok(new { status = true, message = "success", data = profile});   
        }
        [HttpPost("update-profile")]
        public IActionResult updateProfile(UpdateProfile upProfile) {
            Profile profile = profileService.getProfile(upProfile.UserId);
            profile.Name = upProfile.Name;
            profile.Image = upProfile.Image;
            profile.Phone = upProfile.Phone;
            profile.Email = upProfile.Email;
            profileService.updateProfile(profile);
            return Ok(new { status = true, message = "success"});
        }
        [HttpGet("get-type-payment")]
        public IActionResult getTypePayment(string uid) {
            Profile profile = profileService.getProfile(uid);
            return Ok(new { status = true, message = "success", data = profile.TypePayment });
        }
        [HttpPost("update-type-payment")]
        public IActionResult updateTypePayment(string uid, int type)
        {
            Profile profile = profileService.getProfile(uid);
            profile.TypePayment = type;
            profileService.updateProfile(profile);
            return Ok(new { status = true, message = "success" });
        }
    }
}
