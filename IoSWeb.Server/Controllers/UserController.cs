using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using IoSWeb.Server.DTO;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserController(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // POST api/user/signup
    [HttpPost("signup")]
    public async Task<IActionResult> Signup([FromBody] UserSignupDto userSignupDto)
    {
        // Get admin token from Keycloak
        var token = await GetAdminTokenAsync();

        if (token == null)
        {
            return StatusCode(500, "Failed to retrieve admin token from Keycloak.");
        }

        // Create a user in Keycloak
        var success = await CreateUserInKeycloakAsync(token, userSignupDto);

        if (success)
        {
            return Ok("User created successfully in Keycloak.");
        }
        else
        {
            return StatusCode(500, "Failed to create user in Keycloak.");
        }
    }

    // Get admin access token
    private async Task<string> GetAdminTokenAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var tokenUrl = "http://localhost:8080/realms/myskoChat/protocol/openid-connect/token";

        var requestContent = new StringContent(
            $"client_id=myskoChat&client_secret=W2hhE7wq5wIWCfzkxHc4bMW3LYA8Wq17&grant_type=client_credentials",
            Encoding.UTF8, "application/x-www-form-urlencoded");

        var response = await client.PostAsync(tokenUrl, requestContent);

        if (response.IsSuccessStatusCode)
        {
            var tokenResponse = await response.Content.ReadAsStringAsync();
            dynamic tokenData = JsonConvert.DeserializeObject(tokenResponse);
            return tokenData.access_token;
        }

        return null;
    }

    // Create Keycloak user
    private async Task<bool> CreateUserInKeycloakAsync(string token, UserSignupDto userSignupDto)
    {
        var client = _httpClientFactory.CreateClient();
        var userUrl = "http://localhost:8080/admin/realms/myskoChat/users";

        var userData = new
        {
            username = userSignupDto.Username,
            email = userSignupDto.Email,
            firstName = userSignupDto.FirstName, 
            lastName = userSignupDto.LastName,   
            enabled = true,
            credentials = new[]
            {
            new { type = "password", value = userSignupDto.Password, temporary = false }
        }
        };

        var requestContent = new StringContent(JsonConvert.SerializeObject(userData), Encoding.UTF8, "application/json");

        var request = new HttpRequestMessage(HttpMethod.Post, userUrl);
        request.Headers.Add("Authorization", $"Bearer {token}");
        request.Content = requestContent;

        var response = await client.SendAsync(request);

        return response.IsSuccessStatusCode;
    }

}
