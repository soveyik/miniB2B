dotnet new sln -n MiniB2B
dotnet new classlib -n MiniB2B.Entities
dotnet new classlib -n MiniB2B.DataAccess
dotnet new classlib -n MiniB2B.Business
dotnet new mvc -n MiniB2B.Web

dotnet sln add MiniB2B.Entities/MiniB2B.Entities.csproj
dotnet sln add MiniB2B.DataAccess/MiniB2B.DataAccess.csproj
dotnet sln add MiniB2B.Business/MiniB2B.Business.csproj
dotnet sln add MiniB2B.Web/MiniB2B.Web.csproj

dotnet add MiniB2B.DataAccess/MiniB2B.DataAccess.csproj reference MiniB2B.Entities/MiniB2B.Entities.csproj
dotnet add MiniB2B.Business/MiniB2B.Business.csproj reference MiniB2B.Entities/MiniB2B.Entities.csproj
dotnet add MiniB2B.Business/MiniB2B.Business.csproj reference MiniB2B.DataAccess/MiniB2B.DataAccess.csproj
dotnet add MiniB2B.Web/MiniB2B.Web.csproj reference MiniB2B.Business/MiniB2B.Business.csproj
dotnet add MiniB2B.Web/MiniB2B.Web.csproj reference MiniB2B.DataAccess/MiniB2B.DataAccess.csproj
dotnet add MiniB2B.Web/MiniB2B.Web.csproj reference MiniB2B.Entities/MiniB2B.Entities.csproj

dotnet add MiniB2B.DataAccess/MiniB2B.DataAccess.csproj package Microsoft.EntityFrameworkCore.SqlServer -v 8.0.0
dotnet add MiniB2B.DataAccess/MiniB2B.DataAccess.csproj package Microsoft.EntityFrameworkCore.Tools -v 8.0.0
dotnet add MiniB2B.Web/MiniB2B.Web.csproj package Microsoft.EntityFrameworkCore.Design -v 8.0.0
