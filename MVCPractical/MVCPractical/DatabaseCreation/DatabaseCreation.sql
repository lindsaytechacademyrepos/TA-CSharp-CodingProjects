

CREATE DATABASE db_autoquotes

use db_autoquotes

CREATE TABLE quotes(
	id int primary key identity (1,1),
	firstName varchar(30) NOT NULL,
	lastName varchar(30) NOT NULL,
	emailAddress varchar(80) NOT NULL,
	dob date NOT NULL,
	carYear int NOT NULL,
	carMake varchar(30) NOT NULL,
	carModel varchar(30) NOT NULL,
	dui bit NOT NULL,
	tickets int NOT NULL,
	fullCoverage bit NOT NULL,
	quotedPrice decimal(18,2) NOT NULL
)

select * from quotes