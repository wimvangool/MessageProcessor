﻿CREATE TABLE [dbo].[ReadModel_ActiveGames]
(
	[GameKey] UNIQUEIDENTIFIER NOT NULL PRIMARY KEY,
	[WhitePlayerKey] UNIQUEIDENTIFIER NOT NULL,
	[BlackPlayerKey] UNIQUEIDENTIFIER NOT NULL
)