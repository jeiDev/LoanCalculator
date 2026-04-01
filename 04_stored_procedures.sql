USE LoanCalculator;
GO

CREATE PROCEDURE sp_GetRateByAge
    @Age INT
AS
BEGIN
    SELECT Rate FROM AgeRates WHERE Age = @Age
END
GO

CREATE PROCEDURE sp_GetLoanTerms
AS
BEGIN
    SELECT * FROM LoanTerms
END
GO

CREATE PROCEDURE sp_CalculateLoan
    @Age INT,
    @Amount DECIMAL(10,2),
    @Months INT
AS
BEGIN
    DECLARE @Rate DECIMAL(5,2)
    DECLARE @MonthlyPayment DECIMAL(10,2)

    SELECT @Rate = Rate FROM AgeRates WHERE Age = @Age

    SET @MonthlyPayment = (@Amount * @Rate) / @Months

    SELECT 
        @Age AS Age,
        @Rate AS Rate,
        @MonthlyPayment AS MonthlyPayment
END
GO

CREATE PROCEDURE sp_SaveLoanLog
    @Age INT,
    @Amount DECIMAL(10,2),
    @Months INT,
    @MonthlyPayment DECIMAL(10,2),
    @IpAddress VARCHAR(50)
AS
BEGIN
    INSERT INTO LoanLogs
    (QueryDate, Age, Amount, Months, MonthlyPayment, IpAddress)
    VALUES
    (GETDATE(), @Age, @Amount, @Months, @MonthlyPayment, @IpAddress)
END
GO