﻿CREATE TABLE [dbo].[User](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[IdentityProvider] [NVARCHAR](50) NULL,
	[ProviderId] [NVARCHAR](100) NOT NULL,
	[Details] [NVARCHAR](320) NOT NULL
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED
 (
	[Id] ASC
 )
 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

DELETE FROM [dbo].[BankAccount];
ALTER TABLE [dbo].[BankAccount] ADD [UserId] [UNIQUEIDENTIFIER] NOT NULL;
ALTER TABLE [dbo].[BankAccount] ADD CONSTRAINT FK_BankAccount_UserId FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id);