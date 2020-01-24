CREATE DATABASE Silownia;
CREATE TABLE Silownia.dbo.UsersCreds
(UserId int IDENTITY (1,1) NOT NULL,
UserName varchar(25) NOT NULL,
UserSurname varchar(25) NOT NULL,
is_admin bit DEFAULT 0 NOT NULL,
PRIMARY KEY (UserId));
CREATE TABLE Silownia.dbo.UsersLogins
(UserId int,
Login varchar(25) NOT NULL,
Passwd varchar(25) NOT NULL);
ALTER TABLE Silownia.dbo.UsersLogins ADD PRIMARY KEY (Login);
ALTER TABLE Silownia.dbo.UsersLogins
ADD FOREIGN KEY (UserId) 
REFERENCES Silownia.dbo.UsersCreds(UserId);
CREATE TABLE Silownia.dbo.Lockers
(Id int IDENTITY NOT NULL,
Area varchar(25) NOT NULL,
PRIMARY KEY (Id));
CREATE TABLE Silownia.dbo.Areas
(areaId int IDENTITY (1,1) NOT NULL,
AreName varchar(25),
Amount int,
PRIMARY KEY (areaId));CREATE TABLE Silownia.dbo.Reservations
(Id int IDENTITY (1,1) NOT NULL,
userId int,
Area varchar(25),
lockerNo int,
startDate datetime NOT NULL,
endDate datetime NOT NULL,
PRIMARY KEY (Id));
ALTER TABLE Silownia.dbo.Reservations
ADD FOREIGN KEY (userId) 
REFERENCES Silownia.dbo.UsersCreds(UserId);
ALTER TABLE Silownia.dbo.Reservations
ADD FOREIGN KEY (lockerNo) 
REFERENCES Silownia.dbo.Lockers(Id);
ALTER TABLE Silownia.dbo.Reservations 
ALTER COLUMN Area intALTER 
TABLE Silownia.dbo.Reservations
ADD FOREIGN KEY (Area) 
REFERENCES Silownia.dbo.Areas(areaId)