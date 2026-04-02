USE LoanCalculator;
GO

IF NOT EXISTS (SELECT 1 FROM AgeRates)
INSERT INTO AgeRates (Age, Rate) VALUES
(18, 1.20),
(19, 1.18),
(20, 1.16),
(21, 1.14),
(22, 1.12),
(23, 1.10),
(24, 1.08),
(25, 1.05);
GO

IF NOT EXISTS (SELECT 1 FROM LoanTerms)
INSERT INTO LoanTerms (Description, Months) VALUES
('3 Months', 3),
('6 Months', 6),
('9 Months', 9),
('12 Months', 12);
GO