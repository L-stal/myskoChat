using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
})
.AddCookie()  // Add cookie-based authentication
.AddOpenIdConnect("Keycloak", options =>
{
    options.Authority = "http://localhost:8080/realms/myskoChat";  // Keycloak realm URL
    options.ClientId = "myskoChat";  // Client ID , Realm > Client > Client Id 
    options.ClientSecret = "W2hhE7wq5wIWCfzkxHc4bMW3LYA8Wq17";  // Client secret from Keycloak (keyvaluta)?
    options.ResponseType = "code";  // OpenID Connect flow: Authorization code flow(Read more about it)
    options.SaveTokens = true;  // Save tokens in the cookie (outdated? decrypted?)
    options.GetClaimsFromUserInfoEndpoint = true;  // Retrieve user claims
    options.RequireHttpsMetadata = false;
    // Token validation parameters
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/myskoChat",  // Must Match Relm url
        ValidateAudience = true,
        ValidAudience = "myskoChat",  // Client ID
        ValidateLifetime = true
    };
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication();  
app.UseAuthorization();   

app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
