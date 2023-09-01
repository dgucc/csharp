# Blazor Webassembly windows authentication


## Server\BlazorApp.Server.csproj

    <PackageReference Include="Microsoft.AspNetCore.Authentication.Negotiate" Version="6.0.21" />

## Server/Program.cs

builder.Services.AddAuthentication(IISDefaults.AuthenticationScheme);
builder.Services.Configure<IISServerOptions>(options => {
    options.AutomaticAuthentication = true;
});

...
app.UseAuthorization();
app.UseAuthentication();


## Server\Properties\launchSettings.json

"iisSettings": {
    "windowsAuthentication": true,
    "anonymousAuthentication": false,
    "iisExpress": {
      "applicationUrl": "http://localhost:11041",
      "sslPort": 44378
    }
  }

## Server\Controllers\WeatherForecastController.cs

[ApiController]
    [Route("[controller]")]
    public class UserController: ControllerBase {

        [Authorize]
        [HttpGet]
        public string? GetUser() {
            if(User.Identity.IsAuthenticated == true) {
                return User?.Identity?.Name;
            } else { 
                return string.Empty; 
            }

        }
    }


## Client\Pages\Index.razor
@inject HttpClient Http
...
@code{
	private string? username;

    protected override async Task OnInitializedAsync() {
        username = await Http.GetStringAsync("user");
    }
}



## Visual Studio 
    Server project => ALT+ENTER => Debug => Open Debug Launch Profile => Select IISExpress => Enable Windows Authentication
    Select "IIS Express" profile before launching => Start (CTRL+F5)...

