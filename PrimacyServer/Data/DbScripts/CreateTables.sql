-- Create Tables
CREATE TABLE entity (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL
);

CREATE TABLE division (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    id_entity INT NOT NULL REFERENCES Entity(id)
);

CREATE TABLE site (
    id SERIAL PRIMARY KEY,
    name VARCHAR(255) NOT NULL,
    id_division INT NOT NULL REFERENCES Division(id)
);

CREATE TABLE building (
    id SERIAL PRIMARY KEY,
    designation VARCHAR(255) NOT NULL,
    id_site INT NOT NULL REFERENCES Site(id)
);

CREATE TABLE space (
    id SERIAL PRIMARY KEY,
    designation VARCHAR(255) NOT NULL,
    id_building INT NOT NULL REFERENCES Building(id)
);

CREATE TABLE person (
    id SERIAL PRIMARY KEY,
    username VARCHAR(255) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    usertype VARCHAR(50) NOT NULL,
    fullname VARCHAR(255) NOT NULL,
    email VARCHAR(255) UNIQUE NOT NULL,
    id_site INT NOT NULL REFERENCES Site(id)
);

CREATE TABLE category (
    id SERIAL PRIMARY KEY,
    designation VARCHAR(255) NOT NULL
);

CREATE TABLE equipment (
    id SERIAL PRIMARY KEY,
    designation VARCHAR(255) NOT NULL,
    id_space INT NOT NULL REFERENCES Space(id),
    id_category INT NOT NULL REFERENCES Category(id)
);

CREATE TABLE situation (
    id SERIAL PRIMARY KEY,
    id_equipment INT NOT NULL REFERENCES Equipment(id)
);

CREATE TABLE ticket (
    id SERIAL PRIMARY KEY,
    createdByUser_id INT NOT NULL REFERENCES Person(id),
    assignedToUser_id INT NOT NULL REFERENCES Person(id),
    id_situation INT NOT NULL REFERENCES Situation(id),
    createdAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);