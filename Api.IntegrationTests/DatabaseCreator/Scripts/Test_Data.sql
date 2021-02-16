USE ApiDatabaseTest

-- Goal Query - Test GetAllAsync
INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Study English', 110)

INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Workout', 77)

INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Create Portfolio', 40)

-- Goal Query - Test GetAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('ca41679d-ffb0-4899-a357-9f4de75d278a', 'Check E-mails', 50)

-- Goal Command - Test UpdateAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('f4f25a21-6a88-4623-89cc-7d0ed349e0ea', 'Invest', 35)

-- Goal Command2 - Test UpdateAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('82f1c9f8-b009-471b-abbc-fde7d33ed1e4', 'Invest2', 45)

-- Goal Command - Test DeleteAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('74e2948a-37a4-457d-9254-2cbed39ae27f', 'I will be deleted', 100)

-- Goal Command2 - Test DeleteAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('fee01c19-8cf4-431d-b233-d1238c6966c2', 'I will be deleted 2', 100)