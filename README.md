
git clone with GitHub CLI  

`$ gh repo clone dgucc/csharp` 

# ASP.Net Core MVC 

## ContosoUniversity_CodeFirst

mdf,ldf files are not versioned  
ensure attaching those files before hitting CTRL+F5
App_Data/

## Tutorials

[Build a Web App with ASP.NET Core MVC and EF Core](https://medium.com/net-core/building-a-web-application-using-asp-net-core-mvc-and-entity-framework-core-15ee6192b3f3)

# csharp

## Topics

[Delegates and Business Objects](https://www.codeproject.com/Articles/14178/Delegates-and-Business-Objects)  
[C# Delegates](https://www.tutorialsteacher.com/csharp/csharp-delegates)  
[C# Lambda expressions](https://www.tutorialsteacher.com/linq/linq-lambda-expression)  


## VsCode Tips

### Installation on Linux  

[Install Visual Studio Code and .NET Core for C# coding on Linux](https://www.pragmaticlinux.com/2021/03/install-visual-studio-code-and-net-core-for-c-coding-on-linux/)  

Install the .NET SDK package repository   
`$ wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb`  
`$ sudo dpkg -i packages-microsoft-prod.deb` 

Install .NET SDK   
`$ sudo apt-get update`   
`$ sudo apt-get install dotnet-sdk-5.0`  
`$ dotnet --list-sdks`  

[Setup, Build and Run Blazor WASM App on Linux](https://www.prowaretech.com/articles/current/information-technology/linux/setup-and-configure/build-and-run-blazor-wasm-apps)

Restore Dependencies  
`$ dotnet restore`   
Build (in same folder as "sln")  
`$ dotnet build`   
Run by specifying project  
`$ dotnet run --project Server`   

[3 Ways to install Visual studio code in Ubuntu using terminal](https://www.how2shout.com/linux/3-ways-install-visual-studio-code-in-ubuntu-using-terminal/)  

```bash
$ sudo apt install apt-transport-https
$ wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
$ sudo dpkg -i packages-microsoft-prod.deb
$ sudo apt-get update 
$ sudo apt-get install -y dotnet-sdk-6.0
$ sudo apt-get install -y aspnetcore-runtime-6.0
```

### Format curly brackets on the same line c# 

Create a omnisharp.json file in the root of your project or in `%userprofile%\.omnisharp\` folder :   
```json
{
    "FormattingOptions": {
        "NewLinesForBracesInLambdaExpressionBody": false,
        "NewLinesForBracesInAnonymousMethods": false,
        "NewLinesForBracesInAnonymousTypes": false,
        "NewLinesForBracesInControlBlocks": false,
        "NewLinesForBracesInTypes": false,
        "NewLinesForBracesInMethods": false,
        "NewLinesForBracesInProperties": false,
        "NewLinesForBracesInObjectCollectionArrayInitializers": false,
        "NewLinesForBracesInAccessors": false,
        "NewLineForElse": false,
        "NewLineForCatch": false,
        "NewLineForFinally": false
    }
}
```
<Ctrl+Shif+P> Open User Settings (JSON) :  
```json
  "omnisharp.autoStart": true,
  "omnisharp.enableEditorConfigSupport": false
```

<Ctrl+Shift+P> Restart Omnisharp  

### Hide References count in Editor 

<Ctrl+Shif+P> Open User Settings : uncheck "Controls whether the editor Show CodeLens"  
