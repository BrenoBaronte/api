CREATE TABLE Goal (
    [ID] [UNIQUEIDENTIFIER] PRIMARY KEY DEFAULT newsequentialid(),
    [Title] [VARCHAR](64),
    [Count] [INT]
);