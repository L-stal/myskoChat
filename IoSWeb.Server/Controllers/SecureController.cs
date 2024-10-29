using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ProtectedController : ControllerBase
{
    [HttpGet]
    [Authorize]  
    public IActionResult GetProtectedData()
    {
        return Ok("This is secure data");
    }
}
