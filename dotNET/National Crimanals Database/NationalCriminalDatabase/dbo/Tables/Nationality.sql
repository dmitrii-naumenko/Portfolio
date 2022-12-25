CREATE TABLE [dbo].[Nationality] (
    [id]    INT        IDENTITY (1, 1) NOT NULL,
    [Alias] NCHAR (20) NOT NULL,
    CONSTRAINT [PK_Nationality] PRIMARY KEY CLUSTERED ([id] ASC)
);

