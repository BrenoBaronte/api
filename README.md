# Api

An API with CRUD example using REST principles. 
This project is based on .Net Core 2.2.0 and ASP.NET Core 2.2.0 

## Database

In this project we have integration with a SqlServer Database. 
This repo shows two differents examples of how to do it: 
- <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Queries/GoalQuery.cs">GoalQuery</a>, using Dapper ORM.
- <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Queries/GoalQuery2.cs">GoalQuery2</a>, using simple ADO.NET.
 
## Cache

This project has two endpoints(Get by identifier and Get all resources) using Cache with Redis. 

### What's comming?

- Logging(propaly with Serilog).
- Memory Cache implementation.
- Integration with SqlServer using EntityFramework Core.
- Integration with NoSql Database.

### Issues

- Feel free to open any issue if you look anything suspect and I'll check it out ;)
