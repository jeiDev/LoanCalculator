USE LoanCalculator;
GO

IF OBJECT_ID('AgeRates', 'U') IS NULL
CREATE TABLE AgeRates (
    Age INT PRIMARY KEY,
    Rate DECIMAL(5,2)
);
GO

IF OBJECT_ID('LoanTerms', 'U') IS NULL
CREATE TABLE LoanTerms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Description VARCHAR(50),
    Months INT
);
GO

IF OBJECT_ID('LoanLogs', 'U') IS NULL
CREATE TABLE LoanLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QueryDate DATETIME,
    Age INT,
    Amount DECIMAL(10,2),
    Months INT,
    MonthlyPayment DECIMAL(10,2),
    IpAddress VARCHAR(50)
);
GO