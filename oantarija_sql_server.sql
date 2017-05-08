use master;
go
create database oantarija;
go
use oantarija;
go
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
constraint unique unq_usuario_correo(correo),
constraint unique unq_usuario_usermane(username),
primary key(id),
constraint fk_persona_usuario(id),
    foreign key (id)
    references persona(id)
    on delete cascade on update cascade,
constraint fk_rol_usuario(rol),
    foreign key (rol)
    references rol(id)
    on delete cascade on update cascade
);

create table tipo_grupo
(
id int identity(1,1) not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id),
constraint unique unq_tipo_grupo_nombre(nombre)
);

create table sala
(
id int identity(1,1) not null,
nombre varchar(50)not null,
capacidad int not null,
primary key(id),
estado bit not null,
constraint unique unq_sala_nombre(nombre)
);

create table horario
(
id int identity(1,1) not null,
nombre varchar(50)not null,
horainicio time not null,
horafin time not null,
primary key(id),
estado bit not null,
constraint unique unq_(nombre)
);
create table disertante
(
id int not null,
fecharegistro datetime not null,
primary key(id),
constraint fk_persona_disertante foreign key(id)
    references persona(id)
    on delete cascade on update cascade
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
constraint fk_disertante_reserva foreign key (disertante)
    references disertante(id)
    on delete cascade on update cascade,
constraint fk_sala_reserva foreign key (sala)
    references sala(id)
    on delete cascade on update cascade
);

create table reserva
(
id int identity(1,1) not null,
fecha date not null,
cantidad int not null,
vehiculo bit not null,
horario int not null,
usuario int not null,

tipo_grupo int not null,

estado bit not null,
primary key(id),
constraint fk_horario_reserva foreign key references horario(id)
    on delete cascade on update cascade,
constraint fk_usuario_reserva foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade,

constraint fk_tipo_grupo_reserva foreign key (tipo_grupo)
    references tipo_grupo(id)
    on delete cascade on update cascade
);

create table detalle_reserva_tema(
    id int identity(1,1),
    reserva int not null,
    tema int not null,
    primary key(id),
    constraint fk_reserva_detalle_reserva_tema foreign key (reserva)
        references reserva(id)
        on delete cascade on update cascade,
    constraint fk_tema_detalle_reserva_tema foreign key (tema)
        references tema(id)
        on delete cascade on update cascade
);

create table adicionar_reserva(
    id int identity(1,1) not null,
    cantidad int not null,
    usuario int not null,
    reserva int not null,
    primary key(id),
    constraint fk_usuario_adicionar_reserva foreign key(usuario)
        references usuario(id)
        on delete cascade on update cascade,
    constraint fk_reserva_adicionar_reserva foreign key(reserva)
        references reserva(id)
        on delete cascade on update cascade     
);

create table visita
(
id int not null,
estado bit not null,
primary key(id),
constraint fk_visita_programacion foreign key (id)
    references reserva(id)
    on delete cascade on update cascade
);
create table suscripcion
(
id int identity(1,1) not null,
fecharegistro datetime not null,
usuario int not null,
estado bit not null,
primary key(id),
constraint fk_persona_suscripcion foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade
);
create table boletin
(
id int identity(1,1) not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id)
);
create table envio
(
id int identity(1,1) not null,
boletin int not null,
suscripcion int not null,
estado bit not null,
primary key(id),
constraint fk_boletin_envio foreign key (boletin)
    references boletin(id)
    on delete cascade on update cascade,
constraint fk_suscripcion_envio foreign key (suscripcion)
    references suscripcion(id)
    on delete cascade on update cascade
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
constraint fk_usuario_inscripcion foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade,
constraint fk_curso_inscripcion foreign key (curso)
    references curso(id)
    on delete cascade on update cascade
);
create table registro_nubosidad
(
id int identity(1,1) not null,
fecha date not null,
nubosidad varchar(50)not null,
temperatura varchar(50)not null,
observaciones varchar(50)not null,
estado bit not null,
primary key(id)
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
estado bit not null,
primary key(id),
telescopio int,
camara int,
software int,
constraint fk_telescopio_actividad_solar foreign key (telescopio)
    references telescopio(id)
    on delete cascade on update cascade,
    constraint fk_camara_actividad_solar foreign key (camara)
    references camara(id)
    on delete cascade on update cascade,
    constraint fk_software_actividad_solar oreign key (software)
    references software(id)
    on delete cascade on update cascade
);

INSERT INTO `rol` (`id`, `nombre`, `estado`) VALUES (NULL, 'admin', b'1'), (NULL, 'user', b'1');
INSERT INTO `persona` (`id`, `nombre`, `apellido`, `estado`) VALUES (NULL, 'admin', 'admin', b'1');
INSERT INTO `usuario` (`id`, `correo`, `username`, `pass`, `rol`, `estado`) VALUES ('1', 'admin@admin', 'admin', '123', '1', b'1');
