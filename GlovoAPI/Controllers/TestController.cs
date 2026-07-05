using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GlovoAPI.Controllers;


public class ImageClass
{
    public IFormFile Image { get; set; }
}


[Route("api/[controller]")]
[ApiController]
public class TestController(IImageService imageService) : ControllerBase
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> TestImage([FromForm] ImageClass image)
    {
        string path = await imageService.SaveImageAsync(image.Image);

        return Ok(path);
    }
}
