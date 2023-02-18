# csharp

git clone with GitHub CLI  

`$ gh repo clone dgucc/csharp` 

## ContosoUniversity_CodeFirst

mdf,ldf files are not versioned  
ensure attaching those files before hitting CTRL+F5
App_Data/

## Tutorials

[Build a Web App with ASP.NET Core MVC and EF Core](https://medium.com/net-core/building-a-web-application-using-asp-net-core-mvc-and-entity-framework-core-15ee6192b3f3)


## Topics

[Delegates and Business Objects](https://www.codeproject.com/Articles/14178/Delegates-and-Business-Objects)  
[C# Delegates](https://www.tutorialsteacher.com/csharp/csharp-delegates)  
[C# Lambda expressions](https://www.tutorialsteacher.com/linq/linq-lambda-expression)  


## vscode Tips

Format curly brackets on the same line c# 

Create a omnisharp.json file in the root of your project :   
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
or 
<Ctrl+Shift+P> Restart Omnisharp  
