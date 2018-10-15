create database [CodeChallenge]
go

use [CodeChallenge]
go

create schema [Warehouse]
go

create schema [Sales]
go

create table [Warehouse].[Product]
(
    [ProductID] int not null identity(1000, 1000),
    [ProductName] nvarchar(100) not null,
    [ProductDescription] nvarchar(max) null,
    [Price] decimal(8, 4) not null,
    [Likes] int not null,
    [Stocks] int not null,
    [Available] bit not null,
    [CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Warehouse].[ProductPriceHistory]
(
    [ProductPriceHistoryID] int not null identity(1, 1),
    [ProductID] int not null,
    [Price] decimal(8, 4) not null,
    [StartDate] datetime not null,
    [CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[OrderHeader]
(
    [OrderHeaderID] int not null identity(1, 1),
    [OrderDate] datetime not null,
    [Total] decimal(12, 4) not null,
    [CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)

create table [Sales].[OrderDetail]
(
    [OrderDetailID] int not null identity(1, 1),
    [OrderHeaderID] int not null,
    [ProductID] int not null,
    [ProductName] nvarchar(100) not null,
    [UnitPrice] decimal(8, 4) not null,
    [Quantity] int not null,
    [Total] decimal(12, 4) not null,
    [CreationUser] varchar(25) not null,
	[CreationDateTime] datetime not null,
	[LastUpdateUser] varchar(25) null,
	[LastUpdateDateTime] datetime null,
	[Timestamp] rowversion null
)
go

alter table [Warehouse].[Product]
	add constraint [PK_Product] primary key ([ProductID])

alter table [Warehouse].[Product]
	add constraint [U_Warehouse_Product_ProductName] unique ([ProductName])

alter table [Warehouse].[ProductPriceHistory]
    add constraint [PK_Warehouse_ProductPriceHistory] primary key ([ProductPriceHistoryID])

alter table [Sales].[OrderHeader]
    add constraint [PK_Sales_OrderHeader] primary key ([OrderHeaderID])

alter table [Sales].[OrderDetail]
    add constraint [PK_Sales_OrderDetail] primary key ([OrderDetailID])

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Sales_OrderHeader] foreign key (OrderHeaderID)
		references [Sales].[OrderHeader]

alter table [Sales].[OrderDetail]
	add constraint [FK_Sales_OrderDetail_Warehouse_Product] foreign key (ProductID)
		references [Warehouse].[Product]
go

insert into [Warehouse].[Product]
	([ProductName], [ProductDescription], [Price], [Likes], [Stocks], [Available], [CreationUser], [CreationDateTime])
values
	('Coke bottle 24 fl oz', 'Coke bottle 24 fl oz', 1.99, 0, 100, 1, 'seed', getdate())
go
