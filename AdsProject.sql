create database AdsProject;
go

use AdsProject;

create table Category(
Id int not null identity(1,1),
[Name] nvarchar(50) not null,
primary key(Id)
);
go

create table Ad(
Id int not null identity(1,1),
IdCategory int not null,
Title nvarchar(200) not null, 
[Description] nvarchar(max) not null,
Price money not null,
RegistrationDate date not null,
[State] nvarchar(20) not null,
primary key(Id),
foreign key(IdCategory) references Category(Id)
);
go

create table AdImage(
Id int not null identity(1,1),
IdAd int not null,
[Path] nvarchar(max) not null,
primary key(Id),
foreign key(IdAd) references Ad(Id)
);
go