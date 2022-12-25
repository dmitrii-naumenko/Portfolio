-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE FUNCTION [dbo].[FTS_VACANTION]
	-- Add the parameters for the stored procedure here
	(@keywords nvarchar(4000))
	RETURNS TABLE
AS
RETURN (SELECT [key],[rank]
            FROM FREETEXTTABLE([dbo].[Vacancy],(Name,Requirement,Responsibility),@keywords))

