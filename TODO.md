
# Blazor Upload File &rarr; Api

## Drag & Drop Files

[Component DropZone](https://github.com/adriana634/BlazorFileUpload/blob/main/BlazorFileUpload/Shared/DropZone.razor)  
[Css DropZone](https://github.com/adriana634/BlazorFileUpload/blob/main/BlazorFileUpload/Shared/DropZone.razor.css)  

## Use Blazor <InputFile>

```html
<div class="custom-file" style="overflow: hidden; white-space: nowrap;" id="customFile">
    <InputFile OnChange="OnInputFileChange" class="custom-file-input" id="exampleInputFile" aria-describedby="fileHelp" multiple></InputFile>
    <label class="custom-file-label" for="exampleInputFile">
        @InputFileMessage 
    </label>
</div>
```
```csharp
IBrowserFile File;
public string InputFileMessage = "Select a file...";
public void OnInputFileChange(InputFileChangeEventArgs e)
    {
        File=e.File;
        InputFileMessage=e.File.Name;
    }

[...]
InputFileMessage = "Select a file...";
```

How to call web API with iformfile and another string parameter in C#

```csharp
var request = new RestRequest(Method.POST);
request.AddHeader("Postman-Token", "94b86506-cfb0-476e-aaa4-a10b083e32ad");
request.AddHeader("cache-control", "no-cache");
request.AddFile("file", System.IO.File.ReadAllBytes(@"F:\Vishal\tt12.txt"), "tt12.txt");
request.AddParameter("ClientName", "test");
```
