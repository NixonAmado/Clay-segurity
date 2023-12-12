-- Insertar datos en la tabla categoria_persona
INSERT INTO categoria_persona (Id, Nombre) VALUES (1, 'Vigilante');
INSERT INTO categoria_persona (Id, Nombre) VALUES (2, 'Usuario');
INSERT INTO categoria_persona (Id, Nombre) VALUES (3, 'Aseo');


-- Insertar datos en la tabla ciudad
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (1, 'Bucaramanga', 1);
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (2, 'Bogota', 2);
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (3, 'Medellin', 3);
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (4, 'Cali', 4);
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (5, 'Cartagena', 5);
INSERT INTO ciudad (Id, Nombre_Ciudad, Departamento_id) VALUES (6, 'Santa Marta', 6);

-- Insertar datos en la tabla Departamento
INSERT INTO Departamento (Id, Nombre_Departamento, Pais_id)
VALUES 
    (1, 'Santander', 1),
    (2, 'Cundinamarca', 1);

-- Insertar datos en la tabla Direccion
INSERT INTO Direccion (Id, Calle, Numero, Carrera, Barrio, Persona_id, TDireccion_id, TDireccionNavigationId)
VALUES 
    (1, '21', '10', '40', 'Kennedy', 1, 1, NULL),
    (2, '43', '1', '21', 'Giron', 2, 2, NULL),
    (3, '1', '2', '3', 'Piedecuesta', 3, 1, NULL),
    (4, '43', '21', '1', 'Giron', 4, 2, NULL);


-- Insertar datos en la tabla contacto_persona
INSERT INTO contacto_persona (Id, Descripcion, Persona_id, TContacto_id) VALUES (1, '3322131111', 2, 1);
INSERT INTO contacto_persona (Id, Descripcion, Persona_id, TContacto_id) VALUES (2, '2312312111', 2, 2);
INSERT INTO contacto_persona (Id, Descripcion, Persona_id, TContacto_id) VALUES (3, '5555555555', 3, 1);
INSERT INTO contacto_persona (Id, Descripcion, Persona_id, TContacto_id) VALUES (4, '7777777777', 4, 2);
INSERT INTO contacto_persona (Id, Descripcion, Persona_id, TContacto_id) VALUES (5, '9999999999', 5, 1);


-- Insertar datos en la tabla Contrato
INSERT INTO Contrato (Id, FechaContrato, FechaFin, Cliente_id, EmpleadoNavigationId, Estado_id)
VALUES 
    (1, '2023-02-02 00:00:00', '2023-03-03 00:00:00', 1, 2, 1),
    (2, '2022-03-01 00:00:00', '2022-04-03 00:00:00', 5, 6, 2),
    (3, '2022-05-05 00:00:00', '2022-03-03 00:00:00', 4, 3, 1);

-- Insertar datos en la tabla Estado
INSERT INTO Estado (Id, Descripcion)
VALUES 
    (1, 'Activo'),
    (2, 'Inactivo');

-- Insertar datos en la tabla Pais
INSERT INTO Pais (Id, Nombre_Pais)
VALUES 
    (1, 'Colombia'),
    (2, 'Venezuela');

-- Insertar datos en la tabla Persona
INSERT INTO Persona (Id, id_Persona, Nombre, DateReg, IdTPersona, CategoriaPersona_id, Ciudad_id)
VALUES 
    (1, 121321, 'nixon', '2002-01-01 00:00:00', 1, 2, 1),
    (2, 21321, 'Camilo', '2005-01-01 00:00:00', 2, 1, 1),
    (3, 1232112, 'Rolando', '2001-03-03 00:00:00', 2, 3, 1),
    (4, 321312, 'Andres', '2000-01-04 00:00:00', 1, 2, 2),
    (5, 3213121, 'Robal', '2011-03-01 00:00:00', 1, 2, 1),
    (6, 21132321, 'Maria', '2017-03-02 00:00:00', 2, 2, 2)
    (7, 98765, 'Laura', '2003-05-15 00:00:00', 1, 1, 2),
    (8, 12345, 'Javier', '2004-11-20 00:00:00', 2, 2, 1),
    (9, 55555, 'Carolina', '2006-08-07 00:00:00', 1, 3, 2),
    (10, 77777, 'Hugo', '2008-02-28 00:00:00', 2, 2, 1),
    (11, 11111, 'Liliana', '2010-09-12 00:00:00', 1, 1, 2),
    (12, 99999, 'Diego', '2013-07-03 00:00:00', 2, 3, 1);

INSERT INTO Tipo_contacto (Id, Descripcion)
VALUES 
    (1, 'Personal'),
    (2, 'De trabajo');

    -- Insertar datos en la tabla TipoDireccion
INSERT INTO Tipo_direccion (Id, Descripcion)
VALUES 
    (1, 'Casa'),
    (2, 'Apartamento'),
    (3, 'Condominio'),
    (4, 'Otro');

    -- Insertar datos en la tabla CategoriaPersona
INSERT INTO Categoria_persona (Id, Descripcion)
VALUES 
    (1, 'Cliente'),
    (2, 'Empleado');


-- Insertar datos en la tabla Turno
INSERT INTO Turno (Id, Nombre, HoraTurnoI, HoraTurnoF)
VALUES 
    (1, 'mañana', '00:06:06', '00:10:10'),
    (2, 'tarde', '00:12:00', '00:06:20'),
    (3, 'completa', '00:06:01', '00:06:06');

-- Insertar datos en la tabla Programacion
INSERT INTO Programacion (Id, Contrato_id, Turno_id, Empleado_id)
VALUES 
    (1, 1, 1, 2),
    (2, 2, 2, 5),
    (3, 3, 3, 2);
