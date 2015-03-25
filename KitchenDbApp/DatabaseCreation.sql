use Kitchen;

create table Potrawy
(
	IdPotrawy int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	NazwaPotrawy varchar(100) NULL,
	Skladniki varchar(500) NULL,
	Przygotowanie varchar(2000) NULL,
);

create table Skladniki
(
	IdSkladnik int PRIMARY KEY IDENTITY(1,1),
	IdPotrawy int foreign key (IdPotrawy) references Potrawy(IdPotrawy),
	Skladnik varchar(50),
);