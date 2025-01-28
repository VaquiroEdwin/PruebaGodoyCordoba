


create database GodoyCordoba;

USE GodoyCordoba;

CREATE TABLE Usuarios
(
	Cedula bigint not null
	,Nombre varchar(100) not null
	,Apellido varchar(100) not null
	,Email varchar(100) not null
	,FechaAcceso datetime not null
	,Puntaje bigint not null
	CONSTRAINT Pk_Usuarios PRIMARY KEY(Cedula)
);
