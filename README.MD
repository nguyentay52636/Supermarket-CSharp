run Project ; dotnet dev-certs https --trust

migration : 
dotnet ef migrations add InitialCreate
