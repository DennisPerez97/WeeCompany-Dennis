--CREACION DE LA BASE DE DATOS
USE master

IF NOT EXISTS(SELECT name FROM master.dbo.sysdatabases WHERE NAME = 'DBPRUEBAS')
CREATE DATABASE DBPRUEBAS

GO 

USE DBPRUEBAS

-- CREACION DE LA TABLA
if not exists (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_SCHEMA = 'dbo' AND TABLE_NAME = 'Users')

CREATE TABLE Users (
Id int primary key identity (1,1),
Company varchar(30) not null,
Cedula varchar(10) unique not null,
Name varchar(80) not null,
Degree varchar(80) not null,
Email varchar(50) not null,
Phone varchar(10) not null,
);

GO

-- CRUD DE LA BASE DE DATOS
INSERT INTO Users (Company, Cedula, Name, Degree, Email, Phone)
VALUES ('Wee Company', '13761837', 'DENNIS YASSEF PEREZ ROSADO', 'INGENIERIA EN MECATRONICA', 'dennis.perez@hotmail.com', '9851017692');

SELECT * FROM dbo.Users ;

SELECT * FROM dbo.Users WHERE Cedula = '13761837';

UPDATE Users SET Name = 'DENNIS JUAN PEREZ ROSADO' WHERE Cedula = '13761837';

DELETE FROM Users WHERE Cedula = '13761837';