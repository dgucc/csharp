# ASP.NET Core Web API 
# Upload pdf => Return 1st page as jpg

References :  
- [Uploading Files in Blazor Web Assembly & ASP.NET Core Web API [Blazor Topics] | AK Academy](https://www.youtube.com/watch?v=i6C6ospRrYI&list=PLFJQnCcZXWjsHh_-fdpNmZJn1LhNm7ck0&index=3)  ([Github](https://github.com/aksoftware98/blazorfiles))
- [DocNET.Core To convert pdf to jpg](https://github.com/GowenGit/docnet)


## Create project BlazorFiles.Api

- Create **ASP.NET Core Web API** project  
- Folder Controllers : Add **API Controller - Empty**  


## Define Cors Policy + Enable Static Files
**Program.cs** :  
```csharp
builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
});
[...]
app.UseStaticFiles();
app.UseCors("CorsPolicy");
```

## Create project BlazorFiles.Client

Solution : Add > New Project... > **Blazor WebAssembly App**  


## Start <F5> to check it's a working basis
```bash
$ curl -k -X 'GET' 'https://localhost:7001/WeatherForecast' -H 'accept: text/plain' | jq
[
  {
    "date": "2022-10-15T09:14:57.2192893+02:00",
    "temperatureC": 9,
    "temperatureF": 48,
    "summary": "Scorching"
  },
  ...
]

```

---

## Implement API to upload images or pdf2jpg (BlazorFile.Api)

Create folder **wwwroot/Images**  

BlazorFile.Api.csproj :  

```xml
	<ItemGroup>
		<PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
		<PackageReference Include="Docnet.Core" Version="2.3.1" />
		<PackageReference Include="System.Drawing.Common" Version="6.0.0" />
	</ItemGroup>
```

### Upload Image [POST api/images]  

Controllers/ImagesController.cs :  
```csharp
    [HttpPost]
    public async Task<IActionResult> Post([FromForm] IFormFile image) 
```
- Store uploaded Image in wwwroot/Images   
- and return the path of the image

```bash
$ curl -k --location --request POST 'https://localhost:7001/api/images' --form 'pdf=@"example.png"'
Images/047542dc-9341-4632-b594-e76a2f61fccc.png
```
    https://localhost:7001/Images/047542dc-9341-4632-b594-e76a2f61fccc.pdf


### Upload pdf => Return 1st page as jpg [POST api/pdf/pdf2jpg]

Controllers/PdfsController.cs :  
```csharp
    [HttpPost("pdf2jpg")]
    public async Task<IActionResult> PdfPage2Jpg(IFormFile pdf, [FromForm] int page = 1) 
```
    - Get uploaded pdf 
    - Convert pdf page into jpg
    - Return the generated jpg

```bash
$ curl -k --remote-header-name --request POST 'https://localhost:7001/api/pdf/pdf2jpg' --form 'pdf=@"example1.pdf"' --form page=1 -O 

# curl :
#  -k | --insecure :            accept https and ignore certificate errors
#  -J | --remote-header-name :  Write output to a local file using name specified in Content-Disposition HTTP response header
#  -O :                         Write output to a local file named as the remote file

```

### Specify Width [POST api/pdf/page2jpg/fixedwidth]
```csharp
    [HttpPost("page2jpg/fixedWidth")]
    public async Task<IActionResult> PdfPage2JpgFixedWidth(IFormFile pdf, [FromForm] int width, [FromForm] int page = 1)
```
    - fixed Width => scale Height 
```bash
$ curl -k --remote-header-name --request POST 'https://localhost:7001/api/pdf/page2jpg/fixedwidth' --form 'pdf=@"example1.pdf"' --form width=300 --form page=1 -O

```

### Specify Height [POST api/pdf/page2jpg/fixedheight]
```csharp
    [HttpPost("page2jpg/fixedHeight")]
    public async Task<IActionResult> PdfPage2JpgFixedHeight(IFormFile pdf, [FromForm] int height, [FromForm] int page = 1)
```
    - fixed Height => scale Width  

```bash
$ curl -k --remote-header-name --request POST 'https://localhost:7001/api/pdf/page2jpg/fixedheight' --form 'pdf=@"example1.pdf"' --form height=500 --form page=1 -O

```
