create database SpaDB;
GO

use SpaDB;
GO

create table pacientes (
    pacienteid int primary key identity(1,1),
    nombre nvarchar(100) not null,
    email nvarchar(100) unique,
    telefono nvarchar(20)
);

create table terapeutas (
    terapeutaid int primary key identity(1,1),
    nombre nvarchar(100) not null,
    especialidad nvarchar(100)
);

create table servicios (
    servicioid int primary key identity(1,1),
    nombre nvarchar(100) not null,
    descripcion nvarchar(max),
    precio decimal(18, 2) not null,
    duracion_minutos int not null
);

create table cita (
    citaid int primary key identity(1,1),
    
    pacienteid int not null,
    terapeutaid int not null,
    servicioid int not null,
    
    fechahora datetime not null,
    
    foreign key (pacienteid) references pacientes(pacienteid),
    foreign key (terapeutaid) references terapeutas(terapeutaid),
    foreign key (servicioid) references servicios(servicioid)
);
