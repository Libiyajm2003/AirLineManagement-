--create database
CREATE DATABASE FlyWithMeDB;
--use database 
USE FlyWithMeDB;
--create administrator table
CREATE TABLE TblAirport (
    AirportId INT IDENTITY(1,1) PRIMARY KEY,
    AirportCode NVARCHAR(10) UNIQUE NOT NULL,
    AirportName NVARCHAR(100) NOT NULL,
    City NVARCHAR(50) NOT NULL,
    Country NVARCHAR(50) NOT NULL
);
-- Sample Airports
INSERT INTO TblAirport (AirportCode, AirportName, City, Country)
VALUES 
('CHN', 'Chennai Egmore International', 'Chennai', 'India'),
('BLR', 'Shivaji International', 'Bangalore', 'India'),
('MDU', 'Rajiv Gandhi International', 'Madurai', 'India');

CREATE TABLE TblAdministrator (
    AdminId INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) UNIQUE NOT NULL,
    Password NVARCHAR(50) NOT NULL
);

-- Sample Admin
INSERT INTO TblAdministrator (Username, Password)
VALUES ('Admin', 'Admin@123');

CREATE TABLE TblFlight (
    FlightId INT IDENTITY(1,1) PRIMARY KEY,
    DepAirportId INT NOT NULL,
    ArrAirportId INT NOT NULL,
    DepDate DATE NOT NULL,
    DepTime TIME NOT NULL,
    ArrDate DATE NOT NULL,
    ArrTime TIME NOT NULL,
    FOREIGN KEY (DepAirportId) REFERENCES TblAirport(AirportId),
    FOREIGN KEY (ArrAirportId) REFERENCES TblAirport(AirportId)
);

-- Sample Flights
INSERT INTO TblFlight (DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime)
VALUES 
(1, 2, '2025-10-01', '06:00', '2025-10-01', '08:30'), 
(2, 1, '2025-10-02', '09:00', '2025-10-02', '11:30'), 
(1, 3, '2025-10-03', '14:00', '2025-10-03', '16:30'); 

CREATE PROCEDURE sp_ValidateAdminLogin
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    IF EXISTS(
        SELECT 1 
        FROM TblAdministrator 
        WHERE Username = @Username AND Password = @Password
    )
        SELECT 1 AS IsValid;
    ELSE
        SELECT 0 AS IsValid;
END;

CREATE PROCEDURE sp_GetAllFlights
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FlightId, DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime
    FROM TblFlight
    ORDER BY FlightId;
END;

CREATE PROCEDURE sp_GetFlightById
    @FlightId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT FlightId, DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime
    FROM TblFlight
    WHERE FlightId = @FlightId;
END;

CREATE PROCEDURE sp_AddFlight
    @DepAirportId INT,
    @ArrAirportId INT,
    @DepDate DATE,
    @DepTime TIME,
    @ArrDate DATE,
    @ArrTime TIME
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO TblFlight (DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime)
    VALUES (@DepAirportId, @ArrAirportId, @DepDate, @DepTime, @ArrDate, @ArrTime);
END;

CREATE PROCEDURE sp_UpdateFlight
    @FlightId INT,
    @DepAirportId INT,
    @ArrAirportId INT,
    @DepDate DATE,
    @DepTime TIME,
    @ArrDate DATE,
    @ArrTime TIME
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE TblFlight
    SET DepAirportId = @DepAirportId,
        ArrAirportId = @ArrAirportId,
        DepDate = @DepDate,
        DepTime = @DepTime,
        ArrDate = @ArrDate,
        ArrTime = @ArrTime
    WHERE FlightId = @FlightId;
END;




CREATE PROCEDURE sp_DeleteFlight
    @FlightId INT
AS
BEGIN
    SET NOCOUNT ON;

    DELETE FROM TblFlight
    WHERE FlightId = @FlightId;
END;

SELECT f.FlightId,
       d.AirportName AS DepAirport,
       f.DepDate,
       f.DepTime,
       a.AirportName AS ArrAirport,
       f.ArrDate,
       f.ArrTime
FROM TblFlight f
JOIN TblAirport d ON f.DepAirportId = d.AirportId
JOIN TblAirport a ON f.ArrAirportId = a.AirportId
ALTER PROCEDURE sp_AddFlight
    @DepAirportId INT,
    @ArrAirportId INT,
    @DepDate DATE,
    @DepTime TIME,
    @ArrDate DATE,
    @ArrTime TIME,
    @NewFlightId INT OUTPUT
AS
BEGIN
    INSERT INTO TblFlight (DepAirportId, ArrAirportId, DepDate, DepTime, ArrDate, ArrTime)
    VALUES (@DepAirportId, @ArrAirportId, @DepDate, @DepTime, @ArrDate, @ArrTime)

    SET @NewFlightId = SCOPE_IDENTITY()
END

-- 1️ Get All Airports
CREATE PROCEDURE sp_GetAllAirports
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM TblAirport;
END


-- 2️ Get Airport By Id
CREATE PROCEDURE sp_GetAirportById
    @AirportId INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT * FROM TblAirport
    WHERE AirportId = @AirportId;
END

-- 3️ Add Airport
CREATE PROCEDURE sp_AddAirport
    @AirportCode NVARCHAR(10),
    @AirportName NVARCHAR(100),
    @City NVARCHAR(50),
    @Country NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO TblAirport (AirportCode, AirportName, City, Country)
    VALUES (@AirportCode, @AirportName, @City, @Country);

    SELECT SCOPE_IDENTITY() AS NewAirportId;
END


-- 4️ Update Airport
CREATE PROCEDURE sp_UpdateAirport
    @AirportId INT,
    @AirportCode NVARCHAR(10),
    @AirportName NVARCHAR(100),
    @City NVARCHAR(50),
    @Country NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE TblAirport
    SET AirportCode = @AirportCode,
        AirportName = @AirportName,
        City = @City,
        Country = @Country
    WHERE AirportId = @AirportId;
END

-- 5️ Delete Airport
CREATE PROCEDURE sp_DeleteAirport
    @AirportId INT
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM TblAirport
    WHERE AirportId = @AirportId;
END
