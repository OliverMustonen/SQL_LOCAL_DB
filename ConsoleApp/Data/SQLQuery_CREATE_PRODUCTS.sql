DECLARE @ArticleNumber nvarchar(200) SET @ArticleNumber = 'P5'
DECLARE @Title nvarchar(200) SET @Title = 'Product 5'
DECLARE @Description nvarchar(max) SET @Description = ''
DECLARE @Price money = 100 

IF EXISTS (SELECT 1 FROM Products WHERE ArticleNumber = @ArticleNumber) SELECT * FROM Products WHERE ArticleNumber = @ArticleNumber ELSE SELECT 'No product Found' AS Message