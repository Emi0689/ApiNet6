CREATE DATABASE DBAPI

USE DBAPI

CREATE TABLE CATEGORY
(
	[IdCategory] int primary key identity(1,1),
	[Description] varchar (50)
)

CREATE TABLE PRODUCT
(
	IdProduct int primary key identity (1,1),
	Barcode varchar(20),
	[Description] varchar(50),
	Brand varchar(50),
	IdCategory int,
	Price decimal(10,2)
	CONSTRAINT FK_IDCATEGORY FOREIGN KEY (IdCategory) REFERENCES CATEGORY(IdCategory)
)


INSERT INTO CATEGORY(Description) values
('Technology'),('ElectroHome'),('Accessories')

INSERT INTO PRODUCT(Barcode, Description, Brand, IdCategory, Price) values
('50910010', 'Monitor Aoc - Gaming', 'AOC', 1, 1200),
('50910012', 'Disher 21 WLA-21', 'WINTIA', 2, 1749)


