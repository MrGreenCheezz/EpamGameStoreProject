using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace CapstoneProjectAPI.Controllers
{
    public class ImagesController : Controller
    {
        [Route("api/pictures/getPicture")]
        [HttpGet]
        public ActionResult GetPicture(string PicName)
        {
            return base.PhysicalFile(Directory.GetCurrentDirectory() + "/Images/" + PicName, "image/jpeg");
        }
    }
}
