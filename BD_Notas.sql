CREATE DATABASE BD_Notas;
GO

USE BD_Notas;
GO

CREATE TABLE Estudiantes (
    EstudianteID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Cedula NVARCHAR(20) NOT NULL UNIQUE,
    FechaNacimiento DATE,
    Telefono NVARCHAR(15)
);
GO


CREATE TABLE Materias (
    MateriaID INT PRIMARY KEY IDENTITY(1,1),
    Nombre NVARCHAR(100) NOT NULL,
    Creditos INT NOT NULL
);
GO

CREATE TABLE Notas (
    NotasID INT PRIMARY KEY IDENTITY(1,1),
    EstudianteID INT NOT NULL,
    MateriaID INT NOT NULL,
    Calificacion1 DECIMAL(5,2) CHECK (Calificacion1 BETWEEN 0 AND 100),
    Calificacion2 DECIMAL(5,2) CHECK (Calificacion2 BETWEEN 0 AND 100),
    Calificacion3 DECIMAL(5,2) CHECK (Calificacion3 BETWEEN 0 AND 100),
    Calificacion4 DECIMAL(5,2) CHECK (Calificacion4 BETWEEN 0 AND 100),
    Examen DECIMAL(5,2) CHECK (Examen BETWEEN 0 AND 100),

    CONSTRAINT FK_Notas_Estudiantes FOREIGN KEY (EstudianteID)
        REFERENCES Estudiantes(EstudianteID),

    CONSTRAINT FK_Notas_Materias FOREIGN KEY (MateriaID)
        REFERENCES Materias(MateriaID)
);
GO

INSERT INTO Estudiantes (Nombre, Cedula, FechaNacimiento, Telefono) VALUES
('Winnifer Acosta', '001-1234567-8', '2001-05-10', '809-555-1111'),
('Pablo Manuel', '001-7654321-0', '2000-11-25', '809-555-2222'),
('Eddy Sanchez', '002-1122334-5', '1999-03-14', '829-555-3333'),
('Gabriela Santiago', '023-1582264-7', '1998-07-20', '849-874-3613'),
('Fernanda Santa', '014-1822634-2', '2002-06-14', '829-025-3874'),
('Eduardo Martinez', '023-1122334-5', '2000-12-02', '809-241-9945'),
('Javier Poncion', '003-1426337-4', '1999-03-14', '829-845-0233'),
('Felix de los Santos', '009-11535423-8', '2005-01-17', '849-874-3581');
GO

INSERT INTO Materias (Nombre, Creditos) VALUES
('Matemática', 4),
('Lengua Española', 3),
('Ciencias Naturales', 3);
GO

INSERT INTO Notas (EstudianteID, MateriaID, Calificacion1, Calificacion2, Calificacion3, Calificacion4, Examen) VALUES
(1, 1, 85, 90, 88, 92, 95),
(1, 2, 75, 78, 80, 70, 85),
(2, 1, 60, 65, 70, 68, 75),
(2, 3, 88, 87, 90, 85, 80),
(3, 2, 95, 98, 92, 96, 100);
GO
