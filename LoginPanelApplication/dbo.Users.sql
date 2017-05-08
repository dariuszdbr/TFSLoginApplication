CREATE TABLE [dbo].[Users] (
    [UserID]     INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) NULL,
    [Last Name]  NVARCHAR (50) NULL,
    [Login]      NVARCHAR (50) NOT NULL,
    [Password]   BIT		   NULL,
    [Status]     NVARCHAR(50)  NULL,
    [LoginDate]  DATETIME      NULL,
    [LogoutDate] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

