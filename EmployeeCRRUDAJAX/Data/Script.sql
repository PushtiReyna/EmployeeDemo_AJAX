
CREATE DATABASE ClientDB

CREATE TABLE UserMST
(
Id int NOT NULL Identity(1,1) Primary Key,
FirstName [Varchar](100) NOT NULL,
LastName [Varchar](100) NOT NULL,
Gender [Varchar](50) NOT NULL,
DOB  [date] NOT NULL,
MobileNo [nVarchar](30) NOT NULL,
UserName [Varchar](50) NOT NULL,
Password [Varchar](50) NOT NULL,
Email [Varchar](50) NOT NULL,
IsActive [Bit] NOT NULL,
IsDelete [Bit] NOT NULL
)

Alter Table UserMST
  add  Profilephoto varchar(100)  null;