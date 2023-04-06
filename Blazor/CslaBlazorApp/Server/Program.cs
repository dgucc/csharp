using Csla.Configuration;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Server.Kestrel.Core;

string BlazorClientPolicy = "AllowAllOrigins";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options => {
	options.AddPolicy(BlazorClientPolicy,
	  builder => {
		  builder
		.AllowAnyOrigin()
		.AllowAnyHeader()
		.AllowAnyMethod();
	  });
});

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddHttpContextAccessor();
builder.Services.AddCsla(o => o
  .AddAspNetCore()
  .DataPortal(dpo => dpo
	.AddServerSideDataPortal()
	.UseLocalProxy()));

// for Mock Db
builder.Services.AddTransient(typeof(DataAccess.IPublicationDal), typeof(DataAccess.Mock.PublicationDal));
builder.Services.AddTransient(typeof(DataAccess.IDocumentDal), typeof(DataAccess.Mock.DocumentDal));

// for SQL Server
//builder.Services.AddTransient(typeof(dataaccess.ipublicationdal), typeof(dataaccess.mssql.publicationdal));
//builder.services.addtransient(typeof(DataAccess.IDocumentDal), typeof(DataAccess.MSSQL.DocumentDal));

// If using Kestrel:
builder.Services.Configure<KestrelServerOptions>(options => {
	options.AllowSynchronousIO = true;
});

// If using IIS:
builder.Services.Configure<IISServerOptions>(options => {
	options.AllowSynchronousIO = true;
});

// HttpClient for API calls
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
	app.UseWebAssemblyDebugging();
	app.UseSwagger();
	app.UseSwaggerUI();
} else {
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

//app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseRouting();


app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
