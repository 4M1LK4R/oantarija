create database oantarija;
use oantarija;
create table persona
(
id int auto_increment not null,
nombre varchar (50)not null,
apellido varchar(50)not null,
estado bit not null,
primary key(id)
);
create table rol
(
id int auto_increment not null,
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
    on delete cascade on update cascade,
index fk_rol_usuario(rol),
    foreign key (rol)
    references rol(id)
    on delete cascade on update cascade
);

create table tipo_grupo
(
id int auto_increment not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id),
unique(nombre)
);

create table sala
(
id int auto_increment not null,
nombre varchar(50)not null,
capacidad int not null,
primary key(id),
estado bit not null,
unique(nombre)
);

create table horario
(
id int auto_increment not null,
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
    on delete cascade on update cascade
);
create table tema
(
id int auto_increment not null,
nombre varchar(300)not null,
descripcion varchar(500)not null,
disertante int not null,
estado bit not null,
sala int not null,
primary key(id),
index fk_disertante_reserva(disertante),
    foreign key (disertante)
    references disertante(id)
    on delete cascade on update cascade,
index fk_sala_reserva(sala),
    foreign key (sala)
    references sala(id)
    on delete cascade on update cascade
);

create table reserva
(
id int auto_increment not null,
fecha date not null,
cantidad int not null,
vehiculo bit not null,
horario int not null,
usuario int not null,

tipo_grupo int not null,

estado bit not null,
primary key(id),
index fk_horario_reserva(horario),
    foreign key (horario)
    references horario(id)
    on delete cascade on update cascade,
index fk_usuario_reserva(usuario),
    foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade,

index fk_tipo_grupo_reserva(tipo_grupo),
    foreign key (tipo_grupo)
    references tipo_grupo(id)
    on delete cascade on update cascade
);

create table detalle_reserva_tema(
    id int auto_increment,
    reserva int not null,
    tema int not null,
    primary key(id),
    index fk_reserva_detalle_reserva_tema(reserva),
        foreign key (reserva)
        references reserva(id)
        on delete cascade on update cascade,
    index fk_tema_detalle_reserva_tema(tema),
        foreign key (tema)
        references tema(id)
        on delete cascade on update cascade
);

create table adicionar_reserva(
    id int auto_increment not null,
    tema int not null,
    cantidad int not null,
    usuario int not null,
    reserva int not null,
    primary key(id),
    index fk_usuario_adicionar_reserva(usuario),
        foreign key(usuario)
        references usuario(id)
        on delete cascade on update cascade,
    index fk_reserva_adicionar_reserva(reserva),
        foreign key(reserva)
        references reserva(id)
        on delete cascade on update cascade     
);

create table visita
(
id int not null,
estado bit not null,
primary key(id),
index fk_visita_programacion(id),
    foreign key (id)
    references reserva(id)
    on delete cascade on update cascade
);
create table suscripcion
(
id int auto_increment not null,
fecharegistro datetime not null,
usuario int not null,
estado bit not null,
primary key(id),
index fk_persona_suscripcion(usuario),
    foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade
);
create table boletin
(
id int auto_increment not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id)
);
create table envio
(
id int auto_increment not null,
boletin int not null,
suscripcion int not null,
estado bit not null,
primary key(id),
index fk_boletin_envio(boletin),
    foreign key (boletin)
    references boletin(id)
    on delete cascade on update cascade,
index fk_suscripcion_envio(suscripcion),
    foreign key (suscripcion)
    references suscripcion(id)
    on delete cascade on update cascade
);
create table curso
(
id int auto_increment not null,
nombre varchar(50)not null,
estado bit not null,
primary key(id)
);
create table inscripcion
(
id int auto_increment not null,
usuario int not null,
curso int not null,
estado bit not null,
primary key(id),
index fk_usuario_inscripcion(usuario),
    foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade,
index fk_curso_inscripcion(curso),
    foreign key (curso)
    references curso(id)
    on delete cascade on update cascade
);

create table registro_meteo
(
id int auto_increment not null,
usuario int not null,
nombre varchar(50) not null,
documento varchar(200) not null,
estado bit not null,
primary key(id),
index fk_usuario_registro_meteo(usuario),
    foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade
);

create table detalle_registro_meteo
(
id int auto_increment not null,
registro_meteo int not null,
estado bit not null,
primary key(id),
index fk_registro_meteo_detalle_registro_meteo(registro_meteo),
    foreign key (registro_meteo)
    references registro_meteo(id)
    on delete cascade on update cascade
);

create table registro_sol
(
id int auto_increment not null,
usuario int not null,
estado bit not null,
primary key(id),
index fk_usuario_detalle_registro_meteo(usuario),
    foreign key (usuario)
    references usuario(id)
    on delete cascade on update cascade
);

create table detalle_registro_sol
(
id int auto_increment not null,
registro_sol int not null,
estado bit not null,
primary key(id),
index fk_registro_soldetalle_registro_meteosol(registro_sol),
    foreign key (registro_sol)
    references registro_sol(id)
    on delete cascade on update cascade
);

INSERT INTO `rol` (`id`, `nombre`, `estado`) VALUES (NULL, 'admin', b'1'), (NULL, 'user', b'1');
INSERT INTO `persona` (`id`, `nombre`, `apellido`, `estado`) VALUES (NULL, 'admin', 'admin', b'1');
INSERT INTO `usuario` (`id`, `correo`, `username`, `pass`, `rol`, `estado`) VALUES ('1', 'admin@admin', 'admin', '123', '1', b'1');
