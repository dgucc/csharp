# CSLA.NET Blazor Training

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



---
## Tips

Clean up before copying, backuping   
`$ find . -type d -name bin -exec rm -rf "{}" +`  
`$ find . -type d -name obj -exec rm -rf "{}" +`  


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

