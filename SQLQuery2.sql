-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Ngoc
-- Create date: 27-8-2024
-- Description:	GetListPagingProgram
-- =============================================
CREATE PROCEDURE [dbo].[sp_Category_Program_GetListPaging]
	-- Add the parameters for the stored procedure here
	--<@Param1, sysname, @p1> <Datatype_For_Param1, , int> = <Default_Value_For_Param1, , 0>, 
	--<@Param2, sysname, @p2> <Datatype_For_Param2, , int> = <Default_Value_For_Param2, , 0>
	@iTextSearch NVARCHAR(4000),
	@iPageIndex INT,
	@iRowsPerPage int,
	@oTotalRow BIGINT OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	SELECT *
	INTO #TEMP
	FROM [dbo].Programs D
	WHERE ISNULL(D.IsDeleted, 0) =0
	AND ISNULL(D.IsActived, 1) = 1
	AND (ISNULL (@iTextSearch, "") = " " OR 
	D.Name LIKE N'%' + @iTextSeach + '%')

	SET @oTotalRow = (SELECT COUNT(*) FROM #TEMP)
	SELECT *
	FROM #TEMP
	ORDER BY Name ASC UpdateAt DESC, CreateAt DESC
	OFFSET @iPageIndex * @iRowsPerPage ROWS
	FETCH NEXT @iRowsPerPage ROWS ONLY
    -- Insert statements for procedure here
	--SELECT <@Param1, sysname, @p1>, <@Param2, sysname, @p2>
END
GO
