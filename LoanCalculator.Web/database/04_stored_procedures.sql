USE LoanCalculator;
GO

IF OBJECT_ID('sp_CalculateLoan', 'P') IS NOT NULL
    DROP PROCEDURE sp_CalculateLoan;
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

IF OBJECT_ID('sp_SaveLoanLog', 'P') IS NOT NULL
    DROP PROCEDURE sp_SaveLoanLog;
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

IF OBJECT_ID('sp_GetLoanLogs', 'P') IS NOT NULL
    DROP PROCEDURE sp_GetLoanLogs;
GO

CREATE PROCEDURE sp_GetLoanLogs
    @PageNumber INT,
    @PageSize INT
AS
BEGIN
    SELECT 
        QueryDate,
        Age,
        Amount,
        Months,
        MonthlyPayment,
        IpAddress
    FROM LoanLogs
    ORDER BY QueryDate DESC
    OFFSET (@PageNumber - 1) * @PageSize ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    SELECT COUNT(*) AS TotalRecords FROM LoanLogs;
END
GO