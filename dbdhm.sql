
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 10/28/2018 12:27:18
-- Generated from EDMX file: D:\SampleProject\SampleProject\Models\DHMModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [DBDHM];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_CustomerService_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerServices] DROP CONSTRAINT [FK_CustomerService_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerService_Services]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CustomerServices] DROP CONSTRAINT [FK_CustomerService_Services];
GO
IF OBJECT_ID(N'[dbo].[FK_Payment_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_Payment_Customer];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[CustomerServices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomerServices];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO
IF OBJECT_ID(N'[dbo].[Services]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Services];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [CId] int IDENTITY(1,1) NOT NULL,
    [CName] nvarchar(50)  NOT NULL,
    [Address] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [Phone] nvarchar(50)  NOT NULL,
    [Photo] varchar(max)  NULL
);
GO

-- Creating table 'CustomerServices'
CREATE TABLE [dbo].[CustomerServices] (
    [CSID] int IDENTITY(1,1) NOT NULL,
    [CId] int  NOT NULL,
    [SId] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [PId] int IDENTITY(1,1) NOT NULL,
    [CId] int  NOT NULL,
    [Discount] decimal(18,0)  NULL,
    [VAT] decimal(18,0)  NOT NULL,
    [TotalPaidAmount] decimal(18,0)  NOT NULL,
    [PaymentDate] datetime  NOT NULL,
    [ValidDate] datetime  NOT NULL
);
GO

-- Creating table 'Services'
CREATE TABLE [dbo].[Services] (
    [SId] int IDENTITY(1,1) NOT NULL,
    [SName] nvarchar(50)  NOT NULL,
    [SCost] decimal(19,4)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [CId] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([CId] ASC);
GO

-- Creating primary key on [CSID] in table 'CustomerServices'
ALTER TABLE [dbo].[CustomerServices]
ADD CONSTRAINT [PK_CustomerServices]
    PRIMARY KEY CLUSTERED ([CSID] ASC);
GO

-- Creating primary key on [PId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([PId] ASC);
GO

-- Creating primary key on [SId] in table 'Services'
ALTER TABLE [dbo].[Services]
ADD CONSTRAINT [PK_Services]
    PRIMARY KEY CLUSTERED ([SId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CId] in table 'CustomerServices'
ALTER TABLE [dbo].[CustomerServices]
ADD CONSTRAINT [FK_CustomerService_Customer]
    FOREIGN KEY ([CId])
    REFERENCES [dbo].[Customers]
        ([CId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerService_Customer'
CREATE INDEX [IX_FK_CustomerService_Customer]
ON [dbo].[CustomerServices]
    ([CId]);
GO

-- Creating foreign key on [CId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_Payment_Customer]
    FOREIGN KEY ([CId])
    REFERENCES [dbo].[Customers]
        ([CId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Payment_Customer'
CREATE INDEX [IX_FK_Payment_Customer]
ON [dbo].[Payments]
    ([CId]);
GO

-- Creating foreign key on [SId] in table 'CustomerServices'
ALTER TABLE [dbo].[CustomerServices]
ADD CONSTRAINT [FK_CustomerService_Services]
    FOREIGN KEY ([SId])
    REFERENCES [dbo].[Services]
        ([SId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerService_Services'
CREATE INDEX [IX_FK_CustomerService_Services]
ON [dbo].[CustomerServices]
    ([SId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------