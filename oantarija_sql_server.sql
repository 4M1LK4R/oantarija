create database oantarija;
use oantarija;
create table persona
(
id int identity(1,1) not null,
nombre varchar (50)not null,
apellido varchar(50)not null,
estado bit not null,
primary key(id)
);
create table rol
(
id int identity(1,1) not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id)
);

create table usuario
(
id int not null,
correo varchar(50)not null,
username varchar(50) not null,
pass varchar(50)not null,
rol int not null,
estado bit not null,
unique(correo),
unique(username),
primary key(id),
index fk_persona_usuario(id),
    foreign key (id)
    references persona(id)
    ,
index fk_rol_usuario(rol),
    foreign key (rol)
    references rol(id)
    
);

create table tipo_grupo
(
id int identity(1,1) not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id),
unique(nombre)
);

create table sala
(
id int identity(1,1) not null,
nombre varchar(50)not null,
capacidad int not null,
primary key(id),
estado bit not null,
unique(nombre)
);

create table horario
(
id int identity(1,1) not null,
nombre varchar(50)not null,
horainicio time not null,
horafin time not null,
primary key(id),
estado bit not null,
unique(nombre)
);
create table disertante
(
id int not null,
fecharegistro datetime not null,
primary key(id),
index fk_persona_disertante(id),
    foreign key(id)
    references persona(id)
    
);
create table tema
(
id int identity(1,1) not null,
nombre varchar(300)not null,
descripcion varchar(500)not null,
disertante int not null,
estado bit not null,
sala int not null,
primary key(id),
index fk_disertante_reserva(disertante),
    foreign key (disertante)
    references disertante(id)
    ,
index fk_sala_reserva(sala),
    foreign key (sala)
    references sala(id)
    
);

create table reserva
(
id int identity(1,1) not null,
fecha date not null,
cantidad int not null,
horario int not null,
usuario int not null,
tipo_grupo int not null,
estado bit not null,
primary key(id),
index fk_horario_reserva(horario),
    foreign key (horario)
    references horario(id)
    ,
index fk_usuario_reserva(usuario),
    foreign key (usuario)
    references usuario(id)
    ,

index fk_tipo_grupo_reserva(tipo_grupo),
    foreign key (tipo_grupo)
    references tipo_grupo(id)
    
);

create table detalle_reserva_tema(
    id int identity(1,1),
    reserva int not null,
    tema int not null,
    primary key(id),
    index fk_reserva_detalle_reserva_tema(reserva),
        foreign key (reserva)
        references reserva(id)
        ,
    index fk_tema_detalle_reserva_tema(tema),
        foreign key (tema)
        references tema(id)
        
);

create table adicionar_reserva(
    id int identity(1,1) not null,
    cantidad int not null,
    usuario int not null,
    reserva int not null,
    primary key(id),
    index fk_usuario_adicionar_reserva(usuario),
        foreign key(usuario)
        references usuario(id)
        ,
    index fk_reserva_adicionar_reserva(reserva),
        foreign key(reserva)
        references reserva(id)
             
);

create table visita
(
id int not null,
fecha date not null,
hora_entrada time,
hora_salida time,
cantidad_personas int not null,
proposito varchar(50),
vehiculo bit not null,
placa_vehiculo varchar(50),
tipo_vehiculo varchar(50),
color_vehiculo varchar(50),
procedencia varchar(50),
usuario int not null,
estado bit not null,
primary key(id),
index fk_reserva_visita(id),
    foreign key (id)
    references reserva(id)
    
);
create table suscripcion
(
id int identity(1,1) not null,
fecha_registro date not null,
usuario int not null,
estado bit not null,
primary key(id),
index fk_persona_suscripcion(usuario),
    foreign key (usuario)
    references usuario(id)
    
);
create table boletin
(
id int identity(1,1) not null,
nombre varchar(50)not null,
descripcion varchar(200),
fecha_registro date not null,
url varchar(200) not null,
estado bit not null,
primary key(id)
);
create table curso
(
id int identity(1,1) not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id)
);
create table inscripcion
(
id int identity(1,1) not null,
usuario int not null,
curso int not null,
estado bit not null,
primary key(id),
index fk_usuario_inscripcion(usuario),
    foreign key (usuario)
    references usuario(id)
    ,
index fk_curso_inscripcion(curso),
    foreign key (curso)
    references curso(id)
    
);
create table registro_nubosidad
(
id int identity(1,1) not null,
fecha date not null,
hora time not null,
nubosidad varchar(50)not null,
temperatura varchar(50)not null,
observaciones varchar(50),
usuario int not null,
estado bit not null,
primary key(id),
index fk_usuario_registro_nubosidad(usuario),
    foreign key (usuario)
    references usuario(id)
    
);

create table telescopio
(
id int identity(1,1) not null,
nombre varchar(50)not null,
marca varchar(50)not null,
tipo varchar(50)not null,
diametro varchar(50)not null,
dis_focal varchar(50)not null,
montura varchar(50)not null,
estado bit not null,
primary key(id)
);
create table camara
(
id int identity(1,1) not null,
nombre varchar(50)not null,
marca varchar(50)not null,
dim_chip varchar(50)not null,
estado bit not null,
primary key(id)
);
create table software
(
id int identity(1,1) not null,
nombre varchar(50)not null,
objetivo varchar(50)not null,
descripcion varchar(50)not null,
estado bit not null,
primary key(id)
);

create table actividad_solar
(
id int identity(1,1) not null,
fecha date not null,
hora varchar(50) not null,
cantidad_manchas varchar(50)not null,
usuario int not null,
estado bit not null,
primary key(id),
camara int,
telescopio int,
software int,
index fk_usuario_actividad_solar(usuario),
    foreign key (usuario)
    references usuario(id)
    ,
index fk_telescopio_actividad_solar(telescopio),
    foreign key (telescopio)
    references telescopio(id)
    ,
    index fk_camara_actividad_solar(camara),
    foreign key (camara)
    references camara(id)
    ,
    index fk_software_actividad_solar(software),
    foreign key (software)
    references software(id)
    
);
select * from rol
INSERT INTO rol ( nombre, estado) VALUES ( 'admin',1), ( 'user', 1);
INSERT INTO persona ( nombre, apellido, estado) VALUES ( 'admin', 'admin', 1);
INSERT INTO usuario (id, correo, username, pass, rol, estado) VALUES (1, 'admin@admin', 'admin', '123', '1', 1);
