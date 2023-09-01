# CSLA.NET Blazor Training

`$ dotnet new blazorwasm -o CslaBlazorApp -h

## To Execute in vscode (in Terminal)
`$ dotnet build`  
`$ dotnet run --project Server`  
In Browser : https://localhost:7001/Publications  


## CSLA 6.2.2 PackageReferences 
`grep -R -i "csla.*version=" --include \*.csproj `  

## **DataAccess**  
csproj :  
>	`<PackageReference Include="Csla" Version="6.2.2" />`  

> PublicationDTO.cs  
> IPublicationDal.cs `Get, Insert, Update, Delete` 

## **DataAccess.Mock**  
csproj :  
>	`<PackageReference Include="Csla" Version="6.2.2" />`  
> PublicationDal.cs `implements IPublicationDal`  

## **DataAccess.MSSQL**  
csproj :  
>	`<PackageReference Include="Csla" Version="6.2.2" />`  
> PublicationDal.cs `implements IPublicationDal`  

## **Shared**  
csproj :  
>	`<PackageReference Include="Csla" Version="6.2.2" />`  

> Publications.cs `[Fetch]`   
> Publication.cs `RegisterProperty, BusinessRules, [Fetch], [FetchChild], [Insert], [Update], [Delete]`  

## **Server**  
csproj :  
>	`<PackageReference Include="Csla.AspNetCore" Version="6.2.2" />`  
>	`<PackageReference Include="Csla.Blazor" Version="6.2.2" />`  

> Controllers/DataPortalController.cs `[Route("api/[controller]")]`  

## **Client**
csproj :  
>	`<PackageReference Include="Csla.Blazor.WebAssembly" Version="6.2.2" />`  

Pages :  
> Pages/Publications/Publications.razor  
> Pages/Publications/PublicationEdit.razor   

Components :  
> Shared/TextInput.razor  
> Shared/DateInput.razor  
> Shared/DateTimePicker.razor [cf.github](https://github.com/pekspro/BlazorTimePicker)  
> Shared/TimePicker.razor  [cf. github](https://github.com/pekspro/BlazorTimePicker)  
> Shared/SelectEnum.razor

## Log4net
Import log4net in projects "MSSQL", "Server", "Shared" :  

`<PackageReference Include="log4net" Version="2.0.15" />` 

Add file AssemblyInfo.cs :  
`[assembly: log4net.Config.XmlConfigurator(ConfigFile = "log4net.config")]
`  
Add log4net.config :  

```xml
<log4net>
	<root>
		<level value="ALL" />
		<appender-ref ref="console" />
		<appender-ref ref="file" />
	</root>
	<appender name="console" type="log4net.Appender.ConsoleAppender">
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="%date %level %logger - %message%newline" />
		</layout>
	</appender>
	<appender name="file" type="log4net.Appender.RollingFileAppender">
		<file value="log4net.log" />
		<appendToFile value="true" />
		<rollingStyle value="Size" />
		<maxSizeRollBackups value="5" />
		<maximumFileSize value="10MB" />
		<staticLogFileName value="true" />
		<layout type="log4net.Layout.PatternLayout">
			<conversionPattern value="[%level] %d{yyy-MM-dd hh:mm:ss.ffff} [%thread] %logger - %message%newline" />
		</layout>
	</appender>
</log4net>
```

Let's Log :  

```csharp
using log4net;
using log4net.Config;
[...]
private static readonly ILog _log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
[...]
_log.Info("Message to log");
_log.Error(ex.ToString());
```  


## Animate images on hover

CslaBlazorApp\Client\wwwroot\css\image.css :  
```css
/* Zoom  */
img {
    height: 120px;
    transition: transform .5s;
}

img:hover {
    -ms-transform: scale(1.5); /* IE 9 */
    -webkit-transform: scale(1.5); /* Safari 3-8 */
    transform: scale(1.5);
}
```

## API Upload Pdfs

To Add Swagger Component  

CslaBlazorApp.Server.csproj :  
```xml
<ItemGroup>
	<PackageReference Include="Swashbuckle.AspNetCore" Version="6.2.3" />
</ItemGroup>
```
Program.cs :  
```csharp
[...]
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
if (app.Environment.IsDevelopment()) {
	[..]
	app.UseSwagger();
	app.UseSwaggerUI();
}
```

Define API to upload Pdfs :  
Server\Controllers\DocumentController.cs

Test with Swagger GUI : https://localhost:7001/swagger/index.html
API :  https://localhost:7001/api/Document/pdf/upload
`$ curl -k --location --request POST 'https://localhost:7001/api/Document/pdf/upload' --form 'pdf=@"Publication_04_NL.pdf"' --output acknowledged.pdf`  
`
## ConnectionStrings appsettings.json

[CCREK.Internet.Thot.Server.csproj]
appsettings.json :  
```json
{
    ...,
    "ConnectionStrings": {
        "WebsiteDG": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=WebsiteDG;Integrated Security=True",
        "ApplicationsDG": "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Applications;Integrated Security=True"
    }
}

```
  
[CCREK.Internet.Thot.MSSQL.csproj]
> Include="Microsoft.Extensions.Conf  iguration" Version="7.0.0" />    
> Include="Microsoft.Extensions.Config  uration.FileExtensions" Version  ="7.0.0" />  
> Include="Microsoft.Extensions.Configuration.Json" Version="7.0.0" />    
> Include="System.Configuration.ConfigurationManager" Version="7.0.0" />  


PublicatieDal.cs :  
```csharp
using Microsoft.Extensions.Configuration;
...
public PublicatieDal(){
    var configuration = new ConfigurationBuilder()
        .SetBasePath(AppContext.BaseDirectory)
		.AddJsonFile("appsettings.json", optional:false)
        .Build();
	connectionString = configuration.GetConnectionString("CslaDb");
    conn = new SqlConnection(connectionString);
}
```

---
## Tips

Clean up before copying, backuping   
`$ find . -type d -name bin -exec rm -rf "{}" +`  
`$ find . -type d -name obj -exec rm -rf "{}" +`  
Or  
`$ dotnet clean`  

"Failed to bind to address https://127.0.0.1:7001: address already in use"  
`$ lsof -i:7001`  
`$ kill -9 <PID>`  


How to start|stop SQL Express :  

> SqlLocalDB.exe start [instance]     
> SqlLocalDB.exe stop [instance] [-i nowait|-k kill]`   

```cmd
"C:\Program Files\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe" stop "MSSQLLocalDB" -k   
"C:\Program Files\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe" stop "MSSQLLocalDB" -i   
"C:\Program Files\Microsoft SQL Server\150\Tools\Binn\SqlLocalDB.exe" start "MSSQLLocalDB"   
```

---
## References

[CSS tools for web designers](https://www.cssmatic.com/box-shadow)  

[Templated Components in Blazor WebAssembly - Modal Popup - AK Academy](https://www.youtube.com/watch?v=g3vH-KYmsHQ)  
[GitHub](https://github.com/aksoftware98/blazor-modal-popup)  

[working DateTimePicker](https://github.com/pekspro/BlazorTimePicker)  

[How do I use jQuery UI components in a Blazor application?](https://www.syncfusion.com/faq/blazor/general/how-do-i-use-jquery-ui-components-in-a-blazor-application)  

[Blazor with jQuery](https://blog.yudiz.com/blazor-with-jquery/)

[Globalization and Localization](https://learn.microsoft.com/en-us/aspnet/core/blazor/globalization-localization?view=aspnetcore-7.0&pivots=webassembly#dynamically-set-the-culture-by-user-preference)  

[Log4net](https://stackify.com/log4net-guide-dotnet-logging/)  

[font-awesome Cheat list](https://fontawesome.com/v4/icons/)  
[Download font-awesome-4.7.0](https://src.fedoraproject.org/lookaside/extras/fontawesome-fonts/font-awesome-4.7.0.zip/4d7d73ec30555f5351db74f6cfebe91e/)  


SQLServer Locking [TO TEST]  
[SQL Server UPDATE lock and UPDLOCK Table Hints](https://www.mssqltips.com/sqlservertip/6290/sql-server-update-lock-and-updlock-table-hints/)    
[SQL Server Locking, Blocking, And Deadlocks](https://www.bps-corp.com/post/sql-server-locking-and-blocking)      
[Introduction to Locking in SQL Server](https://www.sqlteam.com/articles/introduction-to-locking-in-sql-server)   

