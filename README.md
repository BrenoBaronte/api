# Api

An API with CRUD example using REST principles. 
This project is based on .Net Core 3.1 

## Database

In this project we have integration with a SqlServer Database. 
This repo shows two differents examples of how to do it following CQS(<a href="https://martinfowler.com/bliki/CommandQuerySeparation.html">CommandQuerySeparation</a>): 
- <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Queries/GoalQuery.cs">GoalQuery</a> and <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Commands/GoalCommand.cs">GoalCommand</a>, using Dapper ORM.
- <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Queries/GoalQuery2.cs">GoalQuery2</a> and <a href="https://github.com/BrenoBaronte/api/blob/master/Api.Repositories/Commands/GoalCommand2.cs">GoalCommand2</a>, using simple ADO.NET.
 
## Cache

This project has two endpoints(Get by identifier and Get all resources) using Cache with Redis. 

### What's comming?

- Logging(propaly with Serilog).
- Memory Cache implementation.
- Integration with SqlServer using EntityFramework Core.
- Integration with NoSql Database.

### Issues

- Feel free to open any issue if you look anything suspect and I'll check it out ;)
