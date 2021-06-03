# CosmeticOrder Agile


## Changing ConnectionString 
> Path: ~/CosmeticOrder_Agile/Cosmetic/Cosmetic/appsettings.json

  - Linux + PostgreSQL:
    ```sh

        "ConnectionStrings": {
            "WebMyPham": "User ID=netcore;Password=netcore;Host=localhost;Port=5432;Database=mypham;"
        },
    ```
  - Windows + SQL Server, it's similar to this:
    ```sh

        "ConnectionStrings": {
            "WebMyPham": "Server=.; Database=MyPham; Integrated Security=True"
        },
    ```
## Changing Database Connection using a specific EF Core provider  
> Path: ~/CosmeticOrder_Agile/Cosmetic/Cosmetic/Startup.cs

  - Using PostgreSQL Entity Framework Core

    ```sh
    services.AddEntityFrameworkNpgsql().AddDbContext<MyPhamContext>(options => opions.UseNpgsql(Configuration.GetConnectionString("WebMyPham")));
    ```

  - Using Microsoft SQL Server

    ```sh
    services.AddDbContext<MyPhamContext>(options => options.UseSqlServer(Configuration.GetConnectionString("WebMyPham")));       
    ```

> Path: ~/CosmeticOrder_Agile/Cosmetic/Cosmetic/Models/MyPhamContext.cs
    
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                ...

                    string connectionString = configuration.GetConnectionString("WebMyPham");
                    /////////////////////////////////////////////////////////////////////////
                    //If you are using PostgreSQL Entity Framework Core
                    optionsBuilder.UseNpgsql(connectionString);

                    //If you are using Microsoft SQL Server
                    //optionsBuilder.UseSqlServer(connectionString);
                    //////////////////////////////////////////////////////////////////////////
                ...
            }      
  
## Database & Migration
> Path: ~/CosmeticOrder_Agile/Cosmetic/Cosmetic/

  - Create the database:
      
      <i> Using Visual Studio: </i> Tools -> NuGet Package Manger -> Package Manger Console, run the following command:
    
    ```sh
        Install-Package Microsoft.EntityFrameworkCore.Tools
        Add-Migration InitialCreate
        Update-Database
    ```
  
      <i> Using .Net Core CLI:</i>
      
    ```sh
        dotnet tool install --global dotnet-ef
        dotnet add package Microsoft.EntityFrameworkCore.Design
        dotnet ef migrations add InitialCreate
        dotnet ef database update
    ```

  - If you have an Existing Database:
  
    <b>For Windows + SQL Server:</b>
    
      <i> Using Visual Studio: </i> Tools -> NuGet Package Manger -> Package Manger Console, run the following command:

      ```sh
      PM> Scaffold-DbContext "Server=.; Database=MyPham; Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models
      ```

       <i> Using .Net Core CLI:</i>
        
      ```sh  
      > dotnet ef dbcontext scaffold "Server=.; Database=MyPham; Integrated Security=True" Microsoft.EntityFrameworkCore.SqlServer -o Models
      ```
      
    <b>For Linux + PostgreSQL:</b>
   
      ```sh
      $ dotnet ef dbcontext scaffold "User ID=netcore;Password=netcore;Host=localhost;Port=5432;Database=mypham;" Npgsql.EntityFrameworkCore.PostgreSQL
      ```

Note: Please notice <b>Datatype</b> if there is any error.

  - Ex: "timestamp" into "DateTime" if you are using SQL Server 
  
    > Path: ~/CosmeticOrder_Agile/Cosmetic/Cosmetic/Models/MyPhamContext.cs
    
    Changing
                     
                      entity.Property(e => e.NgayGui).HasColumnType("timestamp");
    into                
    
                      entity.Property(e => e.NgayGui).HasColumnType("DateTime");
