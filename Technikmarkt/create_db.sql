﻿CREATE TABLE a_anbieter (
  a_anbietername VARCHAR(100) UNIQUE NOT NULL,
  a_anbieterwebseite VARCHAR(150) NOT NULL,
  PRIMARY KEY (a_anbietername));

CREATE TABLE p_produkt (
  p_gtin DECIMAL(13,0) CHECK(p_gtin > 1000000000000) NOT NULL,
  p_name VARCHAR(40) NOT NULL,
  p_a_anbietername VARCHAR(100) NOT NULL,
  p_speicherkapazitaetgb DECIMAL(7,3) CHECK(p_speicherkapazitaetgb >= 4 AND p_speicherkapazitaetgb <= 8192) NOT NULL,
  p_preis DECIMAL(6,2) CHECK(p_preis > 0 AND p_preis < 5000) NOT NULL,
  PRIMARY KEY CLUSTERED (p_gtin),
  CONSTRAINT fk_p_produkt_a_anbieter1
    FOREIGN KEY (p_a_anbietername)
    REFERENCES a_anbieter (a_anbietername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE c_computer (
  c_prozessor VARCHAR(40) NOT NULL,
  c_grafikkarte VARCHAR(40) NOT NULL,
  c_p_gtin DECIMAL(13,0) CHECK(c_p_gtin > 1000000000000) NOT NULL,
  PRIMARY KEY (c_p_gtin asc),
  CONSTRAINT fk_c_computer_p_produkt1
    FOREIGN KEY (c_p_gtin)
    REFERENCES p_produkt (p_gtin)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

CREATE TABLE s_smartphone (
  s_farbe VARCHAR(25) NOT NULL,
  s_p_gtin DECIMAL(13,0) CHECK(s_p_gtin > 1000000000000) NOT NULL,
  PRIMARY KEY (s_p_gtin),
    CONSTRAINT fk_s_smartphone_p_produkt1
    FOREIGN KEY (s_p_gtin)
    REFERENCES p_produkt (p_gtin)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

CREATE TABLE l_laufwerk(
  l_isssd TINYINT CHECK(l_isssd < 2) NOT NULL,
  l_p_gtin DECIMAL(13,0) CHECK(l_p_gtin > 1000000000000) NOT NULL,
  PRIMARY KEY (l_p_gtin),
  CONSTRAINT fk_l_laufwerk_p_produkt1
    FOREIGN KEY (l_p_gtin)
    REFERENCES p_produkt (p_gtin)
    ON DELETE CASCADE
    ON UPDATE CASCADE);

CREATE TABLE v_verkaufsraum (
  v_adresse VARCHAR(200) NOT NULL,
  PRIMARY KEY (v_adresse));

CREATE TABLE an_anbieterfiliale (
  an_a_anbietername VARCHAR(100) NOT NULL,
  a_v_adresse VARCHAR(200) NOT NULL,
  PRIMARY KEY (an_a_anbietername, a_v_adresse),
  CONSTRAINT fk_an_anbieterfiliale_a_anbieter1
    FOREIGN KEY (an_a_anbietername)
    REFERENCES a_anbieter (a_anbietername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_an_anbieterfiliale_v_verkaufsraum1
    FOREIGN KEY (a_v_adresse)
    REFERENCES v_verkaufsraum(v_adresse)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE h_haendler(
  h_haendlername VARCHAR(100) NOT NULL,
  h_haendlerwebseite VARCHAR(150) NULL,
  PRIMARY KEY (h_haendlername));

CREATE TABLE g_geschaeft (
  g_v_adresse VARCHAR(200) NOT NULL,
  g_h_haendlername VARCHAR(100) NOT NULL,
  PRIMARY KEY (g_v_adresse, g_h_haendlername),
  CONSTRAINT fk_g_geschaeft_v_verkaufsraum1
    FOREIGN KEY (g_v_adresse)
    REFERENCES v_verkaufsraum (v_adresse)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_g_geschaeft_h_haendler1
    FOREIGN KEY (g_h_haendlername)
    REFERENCES h_haendler (h_haendlername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE ve_verkauftan (
  ve_gtin DECIMAL(13,0) CHECK(ve_gtin > 1000000000000) NOT NULL,
  ve_h_haendlervon VARCHAR(100) UNIQUE NOT NULL,
  ve_h_haendleran VARCHAR(100) UNIQUE NOT NULL,
  PRIMARY KEY (ve_gtin),
  CONSTRAINT fk_ve_verkauftan_h_haendler1
    FOREIGN KEY (ve_h_haendlervon)
    REFERENCES h_haendler (h_haendlername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_ve_verkauftan_h_haendler2
    FOREIGN KEY (ve_h_haendleran)
    REFERENCES h_haendler (h_haendlername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);

CREATE TABLE hp_haendlerkauftprodukt (
  hp_h_haendlername VARCHAR(100) NOT NULL,
  hp_p_gtin DECIMAL(13,0) CHECK(hp_p_gtin > 1000000000000) NOT NULL,
  hp_p_anbietername VARCHAR(100) NOT NULL,
  PRIMARY KEY (hp_h_haendlername, hp_p_gtin, hp_p_anbietername),
  CONSTRAINT fk_h_haendler_has_p_produkt_h_haendler1
    FOREIGN KEY (hp_h_haendlername)
    REFERENCES h_haendler (h_haendlername)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT fk_h_haendler_has_p_produkt_p_produkt1
    FOREIGN KEY (hp_p_gtin)
    REFERENCES p_produkt (p_gtin)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION);



insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('Samsung', 'samsung.com');
insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('Apple', 'apple.com');
insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('Lenovo', 'lenovo.com');
insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('chiliGreen', 'chiligreen.com');
insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('ASUS', 'asus.com');
insert into a_anbieter (a_anbietername, a_anbieterwebseite) values ('Acer', 'acer.com');

insert into h_haendler (h_haendlername, h_haendlerwebseite) values ('Saturn', 'saturn.at');
insert into h_haendler (h_haendlername, h_haendlerwebseite) values ('Media Markt', 'mediamarkt.at');
insert into h_haendler (h_haendlername, h_haendlerwebseite) values ('Conrad', 'conrad.at');
insert into h_haendler (h_haendlername, h_haendlerwebseite) values ('DiTech', 'ditech.at');
insert into h_haendler (h_haendlername, h_haendlerwebseite) values ('Libro', 'libro.at');

insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (3491259353835,'Samsung Galaxy S7','Samsung',64, 699);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (5155067118967, 'Apple IPhone 6s','Apple',128, 500);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (6311141528556, 'Lenovo Yoga 500','Lenovo',256, 1120);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (1678229819502,'Acer aspire s7','Acer',1024, 670);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (9280930334340, 'ASUS PF301','ASUS',2048, 699);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (5524683880690, '4XA0E97775','Lenovo',256, 699);
insert into p_produkt (p_gtin, p_name, p_a_anbietername,p_speicherkapazitaetgb,p_preis) values (5912725125416, 'Tirita','chiliGreen',256, 699);

insert into s_smartphone (s_farbe, s_p_gtin) values ('Gunmetal grey', 3491259353835);
insert into s_smartphone (s_farbe, s_p_gtin) values ('Rose gold',  5155067118967);

insert into c_computer (c_prozessor, c_grafikkarte, c_p_gtin) values ('AMD FX-8300', 'NVIDIA GTX 960M', 6311141528556);
insert into c_computer (c_prozessor, c_grafikkarte, c_p_gtin) values ('Intel i7-6700HQ', 'AMD Radeon R9 390X',1678229819502);

insert into l_laufwerk (l_isssd,l_p_gtin) values (0,9280930334340);
insert into l_laufwerk (l_isssd,l_p_gtin) values (1,5524683880690);
insert into l_laufwerk (l_isssd,l_p_gtin) values (1,5912725125416);