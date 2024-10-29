using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.WithOrigins("https://localhost:5173")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });
});

builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = "Keycloak";
})
.AddCookie()  // Add cookie-based authentication
.AddOpenIdConnect("Keycloak", options =>
{
    options.Authority = "http://localhost:8080/realms/chatRealm";  // Keycloak realm URL
    options.ClientId = "myskoChat";  // Client ID , Realm > Client > Client Id 
    options.ClientSecret = "jHcQRVak2F25CNOFRlL6AMXANL4UvtWQ";  // Client secret from Keycloak (keyvaluta)?
    options.ResponseType = "code";  // OpenID Connect flow: Authorization code flow(Read more about it)
    options.SaveTokens = true;  // Save tokens in the cookie (outdated? decrypted?)
    options.GetClaimsFromUserInfoEndpoint = true;  // Retrieve user claims
    options.RequireHttpsMetadata = false;
    // Token validation parameters
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = "http://localhost:8080/realms/chatRealm",  // Must Match Relm url
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
app.UseCors();
app.MapControllers();

app.MapFallbackToFile("/index.html");

app.Run();
