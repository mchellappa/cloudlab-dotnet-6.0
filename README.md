# cloudlab-dotnet-6.0
A tutorial to walk through the dotnet basics

```dotnet new webapi -o CupcakeApi
cd CupcakeApi
dotnet add package Microsoft.EntityFrameworkCore.InMemory
code -r ../CupcakeApi
dotnet dev-certs https --trust
```

Add a folder named Models.

Add a CupcakeItem.cs file to the Models folder with the following code:

```csharp

namespace CupcakeApi.Models
{
    public class CupcakeItem
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
    }
}

```
Add a CupcakeContext.cs file to the Models folder.

```csharp

using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace CupcakeApi.Models
{
    public class CupcakeContext : DbContext
    {
        public CupcakeContext(DbContextOptions<CupcakeContext> options)
            : base(options)
        {
        }

        public DbSet<CupcakeItem> CupcakeItems { get; set; } = null!;
    }
}

```


Update Program.cs with the following code:

```csharp
using Microsoft.EntityFrameworkCore;
using CupcakeApi.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CupcakeContext>(opt =>
    opt.UseInMemoryDatabase("CupcakeList"));
//builder.Services.AddSwaggerGen(c =>
//{
//    c.SwaggerDoc("v1", new() { Title = "CupcakeApi", Version = "v1" });
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    //app.UseSwagger();
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CupcakeApi v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Run the following commands from the project folder, that is, the CupcakeApi folder:

```cmd
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet aspnet-codegenerator controller -name CupcakeItemsController -async -api -m CupcakeItem -dc CupcakeContext -outDir Controllers
```

