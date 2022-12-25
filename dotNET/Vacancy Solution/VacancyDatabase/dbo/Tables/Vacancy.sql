CREATE TABLE [dbo].[Vacancy] (
    [ID]             INT             IDENTITY (1, 1) NOT NULL,
    [Name]           NVARCHAR (256)  NULL,
    [Url]            NVARCHAR (256)  NULL,
    [Requirement]    NVARCHAR (2048) NULL,
    [Responsibility] NVARCHAR (2048) NULL,
    CONSTRAINT [PK_Vacancy] PRIMARY KEY CLUSTERED ([ID] ASC)
);





