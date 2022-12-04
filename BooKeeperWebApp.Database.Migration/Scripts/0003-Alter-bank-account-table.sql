DELETE FROM [dbo].[BankAccount];
ALTER TABLE [dbo].[BankAccount] ADD [Number] [NVARCHAR](40) NOT NULL;
ALTER TABLE [dbo].[BankAccount] ADD [StartAmount] [FLOAT] NOT NULL;
ALTER TABLE [dbo].[BankAccount] ADD [CurrentAmount] [FLOAT] NULL;
ALTER TABLE [dbo].[BankAccount] ADD [Type] [SMALLINT] NOT NULL;
