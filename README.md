# SmartMed Back End code challenge

## Considerations
- A Service/Application layer was not created due to the simplicity of the challenge.
- No configuration files or environment variables were used.
- No advanced validation or exception handling was implemented. 

## Dependencies
- This application has these dependencies:
	- .NET Core 3.1
	- MongoDB

- MongoDB can be resolved using Docker. Simply access the application folder and run `docker-compose up`.

## How to build
- Access application folder and run `dotnet build`.

## How to run
- Access application folder and run `dotnet run --project .\src\SmartMed.Web\SmartMed.Web.csproj`.

## Additional info
- SwaggerUI can be used to interact with the application at https://localhost:5001/swagger/index.html