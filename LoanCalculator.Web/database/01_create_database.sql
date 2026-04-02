IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'LoanCalculator')
BEGIN
    CREATE DATABASE LoanCalculator;
END
GO