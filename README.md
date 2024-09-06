# learn-dotnet-webapi

- Add Package
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
```

- Make Migrations
```bash
dotnet ef migrations add init
```

- Migrate
```bash
dotnet ef database update
```

- Run
```
dotnet run
# or
dotnet watch
```