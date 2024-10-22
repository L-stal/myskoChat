using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SecureController : ControllerBase
{
    //Test to see if only authed-users gets the data
    [HttpGet]
    [Authorize]  
    public IActionResult GetSecureData()
    {
        return Ok(new { message = "This is secure data!" });
    }
}
