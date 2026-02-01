-- Create database if not exists
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'e-commerce')
BEGIN
    CREATE DATABASE [e-commerce];
END
GO

USE [e-commerce];
GO
