# learn-dotnet-webapi

- dotnet ef
```bash
dotnet tool install --global dotnet-ef --version 8.*

export PATH="$PATH:$HOME/.dotnet/tools"
```

- Add Package
```bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL.Design
```

- Restore package

```bash
dotnet restore
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