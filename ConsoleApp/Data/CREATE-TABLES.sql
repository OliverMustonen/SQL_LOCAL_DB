CREATE TABLE Products 
(
	ArticleNumber nvarchar(200) not null primary key,
	Title nvarchar(200) not null,
	Description nvarchar(max) null,
	Price money not null
)

CREATE TABLE Users 
(
	Id int not null identity primary key,
	FirstName nvarchar(50) not null,
	LastName nvarchar(50) not null,
	Email nvarchar(100) not null unique,
	Password nvarchar(100) not null
)