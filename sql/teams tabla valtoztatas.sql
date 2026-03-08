INSERT INTO engines (engine_name, manufacturer) VALUES (N'Ferrari', N'Ferrari');   -- ID: 1 lesz
INSERT INTO engines (engine_name, manufacturer) VALUES (N'Mercedes', N'Mercedes');  -- ID: 2 lesz
GO

ALTER TABLE teams
ADD engine_id INT;
GO

UPDATE teams SET engine_id = 1 WHERE engine_supplier = 'Ferrari';

-- Ha a régi beszállító 'Mercedes', akkor az engine_id legyen 2
UPDATE teams SET engine_id = 2 WHERE engine_supplier = 'Mercedes';
GO

ALTER TABLE teams
ADD CONSTRAINT FK_Teams_Engines 
FOREIGN KEY (engine_id) REFERENCES engines(engine_id);
GO

ALTER TABLE teams
DROP COLUMN engine_supplier;
GO

SELECT * FROM teams;

-- ez azert kellet hogy ne keljen mindig kiirni a motorszallito nevet ami van hogy nagyon hosszu, igy az engines tabla kezeli amihez itt kotottuk be az id-t