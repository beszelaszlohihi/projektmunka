USE f1_history;
GO

--1. motorgyartok
create table engines (
	engine_id INT IDENTITY(1,1) PRIMARY KEY,
	engine_name NVARCHAR(100) NOT NULL,
	manufacturer NVARCHAR(100)
);

--2. szezonok

create table seasons(
	season_year INT PRIMARY KEY,
	technical_regulations NVARCHAR(MAX)
);

--3. palyak

create table circuits(
	circuit_id INT IDENTITY(1,1) PRIMARY KEY,
	circuit_name NVARCHAR(100) NOT NULL,
	location NVARCHAR(100),
	country NVARCHAR(50),
	length_km DECIMAL(5,3),
	turns INT
);

--4. nagydijak
create table races(
	race_id INT IDENTITY(1,1) PRIMARY KEY,
	season_year INT FOREIGN KEY REFERENCES seasons(season_year),
	circuit_id INT FOREIGN KEY REFERENCES circuits(circuit_id),
	race_date DATE,
	round_number INT
);

--5. versernyeredmenyek, osszekoti a versenyzot a csapatot es a nagydijat
create table race_results(
	result_id INT IDENTITY(1,1) PRIMARY KEY,
	race_id INT FOREIGN KEY REFERENCES races(race_id),
	driver_id INT FOREIGN KEY REFERENCES drivers(driver_id),
	team_id INT FOREIGN KEY REFERENCES teams(team_id),
	grid_position INT,
	final_position INT,
	points_earned DECIMAL(4,1),
	status NVARCHAR(50)
);

--6. idomero eredmenyek
create table qualifying_results(
	qualifying_id INT IDENTITY(1,1) PRIMARY KEY,
	race_id INT FOREIGN KEY REFERENCES races(race_id),
	driver_id INT FOREIGN KEY REFERENCES drivers(driver_id),
	q1_time NVARCHAR(20),
	q2_time NVARCHAR(20),
	q3_time NVARCHAR(20)
);

--7. boxkiallasok
create table pit_stops(
	stop_id INT IDENTITY(1,1) PRIMARY KEY,
	race_id INT FOREIGN KEY REFERENCES races(race_id),
	driver_id INT FOREIGN KEY REFERENCES drivers(driver_id),
	stop_number INT,
	lap INT,
	duration_seconds DECIMAL(5,3)
);

--8. leggyorsabb korok
create table fastest_laps(
	lap_id INT IDENTITY(1,1) PRIMARY KEY,
	race_id INT FOREIGN KEY REFERENCES races(race_id),
	driver_id INT FOREIGN KEY REFERENCES drivers(driver_id),
	lap_time NVARCHAR(20),
	average_speed DECIMAL(6,3)
);

--9. szponzorok
create table sponsors(
	sponsor_id INT IDENTITY(1,1) PRIMARY KEY,
	sponsor_name NVARCHAR(100) NOT NULL,
	industry NVARCHAR(50)
);

--10. csapat-szponzor osszekapcsolo
create table team_sponsors(
	team_id INT FOREIGN KEY REFERENCES teams(team_id),
	sponsor_id INT FOREIGN KEY REFERENCES sponsors(sponsor_id),
	contract_value DECIMAL(15,2),
	PRIMARY KEY (team_id, sponsor_id)
);


--11. versenyzoi szerzodesek
create table driver_contracts(
	contract_id INT IDENTITY(1,1) PRIMARY KEY,
	driver_id INT FOREIGN KEY REFERENCES drivers(driver_id),
	team_id INT FOREIGN KEY REFERENCES teams(team_id),
	season_year INT FOREIGN KEY REFERENCES seasons(season_year),
	salary_estimate DECIMAL(15,2)
);