# Entity Framework and MVC Tutorial 

## ContosoUniversity
[Getting Started with EF using MVC](https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/creating-an-entity-framework-data-model-for-an-asp-net-mvc-application)  

### New web project using the ASP.NET Web Application (.NET Framework) template  
Template : "MVC" , Authentication : none
Create...  

### Views\Shared\_Layout.cshtml
Add menu entries :   
`<li>@Html.ActionLink("Students", "Index", "Student")</li>`

### Views\Home\Index.cshtml
Customize homepage

### Check in browser
Press CTRL+F5 => https://localhost:44371/

---

### Install Entity Framework 6
NuGet Package Manager > Package Manager Console :
`Install-Package EntityFramework`  

### Create Entities
Models\Student.cs  
Models\Course.cs
Models\Enrollment.cs

### Create the database context
create Folder "DAL" (Data Access Layer)
DAL\SchoolContext.cs  - Define entity sets : 
`public DbSet<Student> Students {get; set;}` 
```
protected override void OnModelCreating(DbModelBuilder modelBuilder) {
    modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
}
```  

### Initialize DB with test data
DAL\SchoolInitializer.cs :
```
public class SchoolInitializer : System.Data.Entity. DropCreateDatabaseIfModelChanges<SchoolContext> {
...
    protected override void Seed(SchoolContext context){
        var students = new List<Student>{
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            ...
        };
        students.ForEach(s => context.Students.Add(s));
        context.SaveChanges();
...
    };
}
```
Configure EntityFramework to use the initializer class  - Web.config :  

```
<entityFramework>
	<contexts>
		<context type="ContosoUniversity.DAL.SchoolContext, ContosoUniversity">
			<databaseInitializer type="ContosoUniversity.DAL.SchoolInitializer, ContosoUniversity" />
		</context>
	</contexts>
...
</entityFramework>
```  

### Use LocalDB - SQL Server Express
connectionString to use LocalDB mdf file store in App_Data/ folder  
"AttachDBFilename=|DataDirectory|\ContosoUniversity1.mdf"  

```  
	<connectionStrings>
		<add name="SchoolContext" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;Initial Catalog=ContosoUniversityCF;Integrated Security=SSPI;AttachDBFilename=|DataDirectory|\ContosoUniversityCF.mdf" providerName="System.Data.SqlClient"/>
	</connectionStrings>
```  

### Create Controller and View
Right-click on Controllers folder > Add > New Scaffolded Item  
Select "MVC 5 Controller with views, using Entity Framework" > Add
Add Controller > Select 
- Model class: "Student (ContosoUniversity.Models)"  
- Data context class: "SchoolContext (ContosoUniversity.DAL)"  
- Controller name : "StudentController" [NOT Student***s***Controller]
> Add  

Controllers\StudentController.cs  
Student\Index.cshtml  

Press CTRL+F5 :  
https://localhost:44371/ > Click on "Students" in the menu  

For SSL Port cf. *.csproj.user : `<IISExpressSSLPort>`  

---

## Tips

Cannot drop database "ContosoUniversity1" because it is currently in use.  
Launch Resource Monitor : "resmon"  
    Tab CPU > Search Handles : 'ContosoUniversity1.mdf'  
    kill "sqlserver"  
