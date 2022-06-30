CREATE TABLE [users].[Table] (
    [id]         INT          NOT NULL,
    [first_name] VARCHAR (50) NOT NULL,
    [last_name]  VARCHAR (50) NOT NULL,
    [email]      VARCHAR (50) NOT NULL,
    [password]   VARCHAR (50) NOT NULL,
    [gender]     SMALLINT     NOT NULL,
    PRIMARY KEY CLUSTERED ([id] ASC, [email] ASC)
);

