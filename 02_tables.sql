USE LoanCalculator;
GO

CREATE TABLE AgeRates (
    Age INT PRIMARY KEY,
    Rate DECIMAL(5,2)
);

CREATE TABLE LoanTerms (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Description VARCHAR(50),
    Months INT
);

CREATE TABLE LoanLogs (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    QueryDate DATETIME,
    Age INT,
    Amount DECIMAL(10,2),
    Months INT,
    MonthlyPayment DECIMAL(10,2),
    IpAddress VARCHAR(50)
);