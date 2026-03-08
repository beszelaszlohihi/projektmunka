--teszteles

USE f1_history;
GO


INSERT INTO seasons (season_year, technical_regulations) VALUES 
(2024, N'V6 hibrid éra, 18 colos kerekek, szívóhatású aerodinamika.'),
(2004, N'V10-es motorok, kipörgésgátló engedélyezett, tankolás engedélyezett.');

INSERT INTO circuits (circuit_name, location, country, length_km, turns) VALUES 
(N'Hungaroring', N'Mogyoród', N'Magyarország', 4.381, 14),
(N'Monaco', N'Monte-Carlo', N'Monaco', 3.337, 19);

INSERT INTO races (season_year, circuit_id, race_date, round_number) VALUES 
(2024, 1, '2024-07-21', 13);

INSERT INTO race_results (race_id, driver_id, team_id, grid_position, final_position, points_earned, status) 
VALUES (1, 2, 2, 5, 3, 15.0, N'Finished');

INSERT INTO sponsors (sponsor_name, industry) VALUES 
(N'Oracle', N'Technológia'),
(N'Petronas', N'Olajipar');

INSERT INTO team_sponsors (team_id, sponsor_id, contract_value) 
VALUES (2, 2, 75000000);

SELECT 
    R.race_date, 
    C.circuit_name, 
    D.driver_name, 
    T.team_name, 
    RR.final_position
FROM race_results RR
JOIN races R ON RR.race_id = R.race_id
JOIN circuits C ON R.circuit_id = C.circuit_id
JOIN drivers D ON RR.driver_id = D.driver_id
JOIN teams T ON RR.team_id = T.team_id;