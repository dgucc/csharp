# Blazor Upload Files 

[Uploading Files in Blazor Web Assembly & ASP.NET Core Web API [Blazor Topics] | AK Academy](https://www.youtube.com/watch?v=i6C6ospRrYI&list=PLFJQnCcZXWjsHh_-fdpNmZJn1LhNm7ck0&index=3)  
Code in [Github](https://github.com/aksoftware98/blazorfiles)

## Create project BlazorFiles.Api

Create **ASP.NET Core Web API** project  

Folder Controllers : Add **API Controller - Empty**  


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

## Set Startup Projects

Solution : Set Startup Projects... > Multiple Startup Projects > "Start" for all projects  

## Start <F5> and Sanity Test
```bash
$ curl -k -X 'GET' 'https://localhost:7184/WeatherForecast' -H 'accept: text/plain' | jq
[
  {
    "date": "2022-10-15T09:14:57.2192893+02:00",
    "temperatureC": 9,
    "temperatureF": 48,
    "summary": "Scorching"
  },
  {
    "date": "2022-10-16T09:14:57.2193038+02:00",
    "temperatureC": 38,
    "temperatureF": 100,
    "summary": "Mild"
  },
  {
    "date": "2022-10-17T09:14:57.2193042+02:00",
    "temperatureC": 22,
    "temperatureF": 71,
    "summary": "Cool"
  },
  {
    "date": "2022-10-18T09:14:57.2193045+02:00",
    "temperatureC": -11,
    "temperatureF": 13,
    "summary": "Balmy"
  },
  {
    "date": "2022-10-19T09:14:57.2193048+02:00",
    "temperatureC": 41,
    "temperatureF": 105,
    "summary": "Mild"
  }
]

```

---

## Implement API BlazorFile.Api

Create folder **wwwwroot/Images**  
Create folder **wwwwroot/Pdfs**  

### Controllers/PdfsController.cs 

#### POST api/pdfs
- Store uploaded pdf in wwwroot/Pdfs   
- and return the path of the pdf
```csharp
[HttpPost]
public async Task<IActionResult> Post([FromForm] IFormFile pdf) 
```

Test :   
```bash
$ curl -k --location --request POST 'https://localhost:7184/api/pdfs' --form 'pdf=@"example.pdf"'
Pdfs/047542dc-9341-4632-b594-e76a2f61fccc.pdf
```
https://localhost:7184/Pdfs/047542dc-9341-4632-b594-e76a2f61fccc.pdf


#### POST api/pdfs/pdf2jpg
- Store uploaded pdf in wwwroot/Pdfs 
- Convert 1 page of pdf into jpg 
- Return path of the jpg
```csharp
[HttpPost("pdf2jpg")]
public async Task<IActionResult> Pdf2Jpg([FromForm] IFormFile pdf)
```

Test :    
```bash
curl -k --location --request POST 'https://localhost:7184/api/pdfs/pdf2jpg' --form 'pdf=@"example.pdf"'
Images/f41f6a61-6bcd-4374-90bf-f6fd4e5bdd82.jpg
```

https://localhost:7184/Images/f41f6a61-6bcd-4374-90bf-f6fd4e5bdd82.jpg


