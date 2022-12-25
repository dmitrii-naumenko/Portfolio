-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [Search]

	@FirstName nvarchar(50),
	@LastName nvarchar(50),	
	@Sex nchar(10),	
	@Nationality [NationalityTypeTable] READONLY,

	@AgeFrom int,	
	@AgeBefore int,	

	@HeightFrom float,	
	@HeightBefore float,
		
	@WeightFrom float,	
	@WeightBefore float,	

	@FreeText nvarchar(50)	--! подстановка для полнотекстового поиска - в установленной версии SQL не поддерживается. поэтому не используется (резерв)
								--! в случае полнотекстового поиска надо сортировать результат по балам соотвествия
							--! оэтому просто попробуем использовать LIKE
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SELECT 
		p.FirstName AS 'First name', 
		p.LastName AS 'Last name', 
		s.Alias AS 'Sex', 
		p.DateOfBirth AS 'Date of birth', 
		DATEPART(year, GETDATE() - p.DateOfBirth) - 1900 AS 'Age',
		p.Height AS 'Height', 
		p.Weight AS 'Weigth', 
		n.Alias AS 'Nationality', 
		p.Text AS 'Text'
	FROM Person p
	JOIN Nationality n 	ON p.Nationality = n.id
	JOIN Sex s ON p.Sex = s.id
	WHERE 
		((NOT EXISTS(SELECT * FROM @Nationality)) OR (n.Alias IN (SELECT * FROM @Nationality))) AND
		((@FirstName IS NULL) OR (p.FirstName = @FirstName)) AND
		((@LastName IS NULL) OR (p.LastName = @LastName)) AND
		((@Sex IS NULL) OR (s.Alias = @Sex)) AND
		((@AgeFrom IS NULL) OR (@AgeBefore IS NULL) OR (p.DateOfBirth BETWEEN 
									DATEADD(year, 0 - @AgeBefore,GETDATE()) AND DATEADD(year, 0 - @AgeFrom,GETDATE()))) AND
		((@HeightFrom IS NULL) OR (@HeightBefore IS NULL) OR (p.Height BETWEEN @HeightFrom AND @HeightBefore)) AND
		((@WeightFrom IS NULL) OR (@WeightBefore IS NULL) OR (p.Height BETWEEN @WeightFrom AND @WeightBefore)) AND
		((@FreeText IS NULL) OR (p.Text LIKE '%'+@FreeText+'%'))
		
	ORDER BY p.FirstName


	-- Надо возвращать код ошибки True/False
END
