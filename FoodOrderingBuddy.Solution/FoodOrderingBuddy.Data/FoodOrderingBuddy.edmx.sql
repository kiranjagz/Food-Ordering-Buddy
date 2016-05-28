
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/10/2015 15:29:33
-- Generated from EDMX file: C:\Kiran TFS Development\FoodOrderingBuddy.Mvc\FoodOrderingBuddy.Solution\FoodOrderingBuddy.Data\FoodOrderingBuddy.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [FoodOrderingBuddy];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_MenuItem_MenuCatergory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[MenuItem] DROP CONSTRAINT [FK_MenuItem_MenuCatergory];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderItems_MenuItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItems_MenuItem];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderItems_Order]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItems_Order];
GO
IF OBJECT_ID(N'[dbo].[FK_ShoppingCart_MenuItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ShoppingCart] DROP CONSTRAINT [FK_ShoppingCart_MenuItem];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Coupons]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Coupons];
GO
IF OBJECT_ID(N'[dbo].[MenuCatergory]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuCatergory];
GO
IF OBJECT_ID(N'[dbo].[MenuItem]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MenuItem];
GO
IF OBJECT_ID(N'[dbo].[Order]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Order];
GO
IF OBJECT_ID(N'[dbo].[OrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderItems];
GO
IF OBJECT_ID(N'[dbo].[ShoppingCart]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ShoppingCart];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'MenuCatergories'
CREATE TABLE [dbo].[MenuCatergories] (
    [MenuCatergoryId] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [DateCreated] datetime  NULL
);
GO

-- Creating table 'MenuItems'
CREATE TABLE [dbo].[MenuItems] (
    [MenuItemId] int IDENTITY(1,1) NOT NULL,
    [MenuCategoryId] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Description] varchar(200)  NOT NULL,
    [Price] decimal(7,2)  NOT NULL,
    [Visible] bit  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'ShoppingCarts'
CREATE TABLE [dbo].[ShoppingCarts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [CartId] varchar(max)  NOT NULL,
    [MenuItemId] int  NOT NULL,
    [UserId] varchar(200)  NOT NULL,
    [Count] int  NOT NULL,
    [DateCreated] datetime  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [OrderItemId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int  NOT NULL,
    [MenuItemId] int  NOT NULL,
    [Quantity] int  NOT NULL,
    [Price] decimal(7,2)  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [OrderId] int IDENTITY(1,1) NOT NULL,
    [Username] varchar(200)  NOT NULL,
    [DateCreated] datetime  NOT NULL,
    [MenuItemsTotal] int  NOT NULL,
    [Total] decimal(7,2)  NOT NULL,
    [OrderResponse] varchar(max)  NULL,
    [QueueId] int  NULL
);
GO

-- Creating table 'Coupons'
CREATE TABLE [dbo].[Coupons] (
    [CouponId] uniqueidentifier  NOT NULL,
    [Code] varchar(50)  NOT NULL,
    [Amount] decimal(7,2)  NOT NULL,
    [DateIssueed] datetime  NOT NULL,
    [DateRedeemed] datetime  NULL,
    [IsRedeemed] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [MenuCatergoryId] in table 'MenuCatergories'
ALTER TABLE [dbo].[MenuCatergories]
ADD CONSTRAINT [PK_MenuCatergories]
    PRIMARY KEY CLUSTERED ([MenuCatergoryId] ASC);
GO

-- Creating primary key on [MenuItemId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [PK_MenuItems]
    PRIMARY KEY CLUSTERED ([MenuItemId] ASC);
GO

-- Creating primary key on [Id] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [PK_ShoppingCarts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [OrderItemId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([OrderItemId] ASC);
GO

-- Creating primary key on [OrderId] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([OrderId] ASC);
GO

-- Creating primary key on [CouponId] in table 'Coupons'
ALTER TABLE [dbo].[Coupons]
ADD CONSTRAINT [PK_Coupons]
    PRIMARY KEY CLUSTERED ([CouponId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MenuCategoryId] in table 'MenuItems'
ALTER TABLE [dbo].[MenuItems]
ADD CONSTRAINT [FK_MenuItem_MenuCatergory]
    FOREIGN KEY ([MenuCategoryId])
    REFERENCES [dbo].[MenuCatergories]
        ([MenuCatergoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_MenuItem_MenuCatergory'
CREATE INDEX [IX_FK_MenuItem_MenuCatergory]
ON [dbo].[MenuItems]
    ([MenuCategoryId]);
GO

-- Creating foreign key on [MenuItemId] in table 'ShoppingCarts'
ALTER TABLE [dbo].[ShoppingCarts]
ADD CONSTRAINT [FK_ShoppingCart_MenuItem]
    FOREIGN KEY ([MenuItemId])
    REFERENCES [dbo].[MenuItems]
        ([MenuItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ShoppingCart_MenuItem'
CREATE INDEX [IX_FK_ShoppingCart_MenuItem]
ON [dbo].[ShoppingCarts]
    ([MenuItemId]);
GO

-- Creating foreign key on [MenuItemId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItems_MenuItem]
    FOREIGN KEY ([MenuItemId])
    REFERENCES [dbo].[MenuItems]
        ([MenuItemId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderItems_MenuItem'
CREATE INDEX [IX_FK_OrderItems_MenuItem]
ON [dbo].[OrderItems]
    ([MenuItemId]);
GO

-- Creating foreign key on [OrderId] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItems_Order]
    FOREIGN KEY ([OrderId])
    REFERENCES [dbo].[Orders]
        ([OrderId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderItems_Order'
CREATE INDEX [IX_FK_OrderItems_Order]
ON [dbo].[OrderItems]
    ([OrderId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------