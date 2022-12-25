CREATE FULLTEXT INDEX ON [dbo].[Vacancy]
    ([Name] LANGUAGE 1049, [Requirement] LANGUAGE 1049, [Responsibility] LANGUAGE 1049)
    KEY INDEX [PK_Vacancy]
    ON [FtsCatalog];

