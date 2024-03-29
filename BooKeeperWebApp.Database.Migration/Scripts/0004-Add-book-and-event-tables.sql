﻿CREATE TABLE [dbo].[Book](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[UserId] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL
 CONSTRAINT [PK_Book] PRIMARY KEY CLUSTERED
 (
	[Id] ASC
 )
 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Book] ADD CONSTRAINT FK_Book_UserId FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id);
GO

CREATE TABLE [dbo].[Event](
	[Id] [UNIQUEIDENTIFIER] NOT NULL,
	[UserId] [UNIQUEIDENTIFIER] NOT NULL,
	[Name] [NVARCHAR](100) NOT NULL
 CONSTRAINT [PK_Event] PRIMARY KEY CLUSTERED
 (
	[Id] ASC
 )
 WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Event] ADD CONSTRAINT FK_Event_UserId FOREIGN KEY (UserId) REFERENCES [dbo].[User](Id);
GO

