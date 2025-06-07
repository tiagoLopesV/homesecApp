-- Insert data into Entity
INSERT INTO Entity (name) VALUES
('Entity A'), ('Entity B'), ('Entity C'), ('Entity D'), ('Entity E'),
('Entity F'), ('Entity G'), ('Entity H'), ('Entity I'), ('Entity J'),
('Entity K'), ('Entity L'), ('Entity M'), ('Entity N'), ('Entity O'),
('Entity P'), ('Entity Q'), ('Entity R'), ('Entity S'), ('Entity T');

-- Insert data into Division
INSERT INTO Division (name, id_entity) VALUES
('Division A1', 1), ('Division A2', 1), ('Division B1', 2), ('Division B2', 2), ('Division C1', 3),
('Division D1', 4), ('Division D2', 4), ('Division E1', 5), ('Division E2', 5), ('Division F1', 6),
('Division G1', 7), ('Division G2', 7), ('Division H1', 8), ('Division I1', 9), ('Division J1', 10),
('Division K1', 11), ('Division L1', 12), ('Division M1', 13), ('Division N1', 14), ('Division O1', 15);

-- Insert data into Site
INSERT INTO Site (name, id_division) VALUES
('Site A', 1), ('Site B', 2), ('Site C', 3), ('Site D', 4), ('Site E', 5),
('Site F', 6), ('Site G', 7), ('Site H', 8), ('Site I', 9), ('Site J', 10),
('Site K', 11), ('Site L', 12), ('Site M', 13), ('Site N', 14), ('Site O', 15),
('Site P', 16), ('Site Q', 17), ('Site R', 18), ('Site S', 19), ('Site T', 20);

-- Insert data into Building
INSERT INTO Building (designation, id_site) VALUES
('Building 1', 1), ('Building 2', 2), ('Building 3', 3), ('Building 4', 4), ('Building 5', 5),
('Building 6', 6), ('Building 7', 7), ('Building 8', 8), ('Building 9', 9), ('Building 10', 10),
('Building 11', 11), ('Building 12', 12), ('Building 13', 13), ('Building 14', 14), ('Building 15', 15),
('Building 16', 16), ('Building 17', 17), ('Building 18', 18), ('Building 19', 19), ('Building 20', 20);

-- Insert data into Space
INSERT INTO Space (designation, id_building) VALUES
('Room 101', 1), ('Room 102', 2), ('Room 103', 3), ('Room 104', 4), ('Room 105', 5),
('Room 106', 6), ('Room 107', 7), ('Room 108', 8), ('Room 109', 9), ('Room 110', 10),
('Room 111', 11), ('Room 112', 12), ('Room 113', 13), ('Room 114', 14), ('Room 115', 15),
('Room 116', 16), ('Room 117', 17), ('Room 118', 18), ('Room 119', 19), ('Room 120', 20);

-- Insert data into Person
INSERT INTO Person (username, password, usertype, fullname, email, id_site) VALUES
('user1', 'pass1', 'admin', 'John Doe', 'john1@example.com', 1),
('user2', 'pass2', 'user', 'Jane Smith', 'jane2@example.com', 2),
('user3', 'pass3', 'user', 'Alice Brown', 'alice3@example.com', 3),
('user4', 'pass4', 'admin', 'Bob White', 'bob4@example.com', 4),
('user5', 'pass5', 'user', 'Charlie Black', 'charlie5@example.com', 5),
('user6', 'pass6', 'user', 'Daniel Grey', 'daniel6@example.com', 6),
('user7', 'pass7', 'admin', 'Eve Green', 'eve7@example.com', 7),
('user8', 'pass8', 'user', 'Frank Blue', 'frank8@example.com', 8),
('user9', 'pass9', 'user', 'Grace Red', 'grace9@example.com', 9),
('user10', 'pass10', 'admin', 'Henry Yellow', 'henry10@example.com', 10),
('user11', 'pass11', 'user', 'Ivy Purple', 'ivy11@example.com', 11),
('user12', 'pass12', 'user', 'Jack Orange', 'jack12@example.com', 12),
('user13', 'pass13', 'admin', 'Kate Cyan', 'kate13@example.com', 13),
('user14', 'pass14', 'user', 'Leo Brown', 'leo14@example.com', 14),
('user15', 'pass15', 'user', 'Mia Pink', 'mia15@example.com', 15),
('user16', 'pass16', 'admin', 'Nathan Silver', 'nathan16@example.com', 16),
('user17', 'pass17', 'user', 'Olivia Gold', 'olivia17@example.com', 17),
('user18', 'pass18', 'user', 'Paul Copper', 'paul18@example.com', 18),
('user19', 'pass19', 'admin', 'Quinn Steel', 'quinn19@example.com', 19),
('user20', 'pass20', 'user', 'Rachel Bronze', 'rachel20@example.com', 20);

-- Insert data into Category
INSERT INTO Category (designation) VALUES
('Electrical'), ('Mechanical'), ('Plumbing'), ('HVAC'), ('IT Equipment'),
('Security'), ('Furniture'), ('Medical'), ('Automobile'), ('Industrial'),
('Food Processing'), ('Aerospace'), ('Marine'), ('Textile'), ('Chemical'),
('Mining'), ('Robotics'), ('Printing'), ('Power Tools'), ('Agricultural');

-- Insert data into Equipment
INSERT INTO Equipment (designation, id_space, id_category) VALUES
('Equipment 1', 1, 1), ('Equipment 2', 2, 2), ('Equipment 3', 3, 3), ('Equipment 4', 4, 4),
('Equipment 5', 5, 5), ('Equipment 6', 6, 6), ('Equipment 7', 7, 7), ('Equipment 8', 8, 8),
('Equipment 9', 9, 9), ('Equipment 10', 10, 10), ('Equipment 11', 11, 11), ('Equipment 12', 12, 12),
('Equipment 13', 13, 13), ('Equipment 14', 14, 14), ('Equipment 15', 15, 15),
('Equipment 16', 16, 16), ('Equipment 17', 17, 17), ('Equipment 18', 18, 18),
('Equipment 19', 19, 19), ('Equipment 20', 20, 20);

-- Insert data into Situation
INSERT INTO Situation (id_equipment) VALUES
(1), (2), (3), (4), (5), (6), (7), (8), (9), (10), (11), (12), (13), (14), (15),
(16), (17), (18), (19), (20);

-- Insert data into Ticket
INSERT INTO Ticket (createdByUser_id, assignedToUser_id, id_situation) VALUES
(1, 2, 1), (3, 4, 2), (5, 6, 3), (7, 8, 4), (9, 10, 5),
(11, 12, 6), (13, 14, 7), (15, 16, 8), (17, 18, 9), (19, 20, 10);