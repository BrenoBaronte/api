USE ApiDatabaseTest

-- Goal Repository - Test GetAllAsync
INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Study English', 110)

INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Workout', 77)

INSERT INTO [dbo].[Goal] (Title, Count)
VALUES ('Create Portfolio', 40)

-- Goal Repository - Test GetAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('ca41679d-ffb0-4899-a357-9f4de75d278a', 'Check E-mails', 50)

-- Goal Repository - Test UpdateAsync
INSERT INTO [dbo].[Goal] (Id, Title, Count)
VALUES ('f4f25a21-6a88-4623-89cc-7d0ed349e0ea', 'Invest', 35)