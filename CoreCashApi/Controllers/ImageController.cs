using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreCashApi.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace CoreCashApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ImageController : ControllerBase
    {
        private readonly ImageUtility _util;

        public ImageController(ImageUtility util)
        {
            _util = util;
        }

        [HttpGet("{fileName}")]
        public async Task<IActionResult> GetImage([FromRoute] string fileName)
        {
            try
            {
                var imageData = await _util.GetImageAsync(fileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();
                string contentType = fileExtension switch
                {
                    ".jpeg" => "image/jpeg",
                    ".jpg" => "image/jpeg",
                    ".png" => "image/png",
                    _ => "application/octet-stream"
                };
                // TODO: Lanjut
                return File(imageData, contentType);
            }
            catch (FileNotFoundException)
            {
                return Ok("Gambar tidak ditemukan.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpDelete("{fileName}")]
        public IActionResult DeleteImage([FromRoute] string fileName)
        {
            try
            {
                var exist = _util.DeleteImage(fileName);
                if (!exist) return NotFound();
                return NoContent();
            }
            catch (FileNotFoundException)
            {
                return NotFound("Gambar tidak ditemukan.");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

    }
}