CREATE TABLE [dbo].[Users] (
    [UserID]			INT           IDENTITY (1, 1) NOT NULL,
    [Name]				NVARCHAR (50) NULL,
    [LastName]			NVARCHAR (50) NULL,
    [Role]				BIT           NOT NULL,
	[Status]			BIT NOT NULL,
    [DateOfEmployment]	DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([UserID] ASC)
);

