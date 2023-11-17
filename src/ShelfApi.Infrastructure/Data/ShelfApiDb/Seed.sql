USE [ShelfApiDb]
GO

INSERT INTO [dbo].[Roles] ([Name], [NormalizedName], [ConcurrencyStamp]) VALUES 
	(N'user', N'USER', N'05ab85c5-9b38-4a07-bf0d-3cac4e7fb3b5'),
    (N'admin', N'ADMIN', N'81a1ef6a-6775-421d-80e1-30bdcf491305')

GO

INSERT INTO [dbo].[Configs] ([Id], [EnvironmentName], [Category], [Value]) VALUES 
	(0, 'PRODUCTION', 'JWT', N'{"key":"jwt-key", "issuer":"http://localhost:5251", "audience":"http://localhost:5251"}'),
	(100, 'DEVELOPMENT', 'JWT', N'{"key":"jwt-key", "issuer":"http://localhost:5251", "audience":"http://localhost:5251"}')

GO