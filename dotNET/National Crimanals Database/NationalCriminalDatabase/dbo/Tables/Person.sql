CREATE TABLE [Person] (
    [id]          INT             IDENTITY (1, 1) NOT NULL,
    [FirstName]   NVARCHAR (50)   NOT NULL,
    [LastName]    NVARCHAR (50)   NULL,
    [Sex]         INT             NOT NULL,
    [DateOfBirth] DATETIME        NULL,
    [Height]      FLOAT (53)      NULL,
    [Weight]      FLOAT (53)      NULL,
    [Nationality] INT             CONSTRAINT [DF_Person_Nationality] DEFAULT ((1)) NOT NULL,
    [Text]        NVARCHAR (1000) NULL,
    CONSTRAINT [PK_Person] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [CK_Person_Height] CHECK ([Height]>(0)),
    CONSTRAINT [CK_Person_Weight] CHECK ([Weight]>(0)),
    CONSTRAINT [FK_Person_Nationality] FOREIGN KEY ([Nationality]) REFERENCES [Nationality] ([id]),
    CONSTRAINT [FK_Person_Sex] FOREIGN KEY ([Sex]) REFERENCES [Sex] ([id])
);




GO
CREATE NONCLUSTERED INDEX [IX_Person_FirstName]
    ON [Person]([FirstName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Person_LastName]
    ON [Person]([LastName] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_Person_DateOfBirth]
    ON [Person]([DateOfBirth] ASC);

