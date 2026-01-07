-- Script:  01_CreateDatabase.sql
-- Description: Create Database and Schema for School Supplies Shop
-- SQL Server Version


-- Drop database if exists
IF EXISTS (SELECT name FROM sys.databases WHERE name = N'SchoolSuppliesDB')
BEGIN
    ALTER DATABASE SchoolSuppliesDB SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE SchoolSuppliesDB;
END
GO

-- Create database
CREATE DATABASE SchoolSuppliesDB
    COLLATE Vietnamese_CI_AS;
GO

-- Use the database
USE SchoolSuppliesDB;
GO

-- Create schema
IF NOT EXISTS (SELECT * FROM sys.schemas WHERE name = N'shop')
BEGIN
    EXEC('CREATE SCHEMA shop');
END
GO

-- Confirmation message
PRINT 'Database SchoolSuppliesDB created successfully! ';
GO