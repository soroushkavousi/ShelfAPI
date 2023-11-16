USE [ShelfApiDb]
GO

INSERT INTO [dbo].[Roles] ([Name] ,[NormalizedName] ,[ConcurrencyStamp]) VALUES 
	(N'user', N'USER' , N'05ab85c5-9b38-4a07-bf0d-3cac4e7fb3b5'),
    (N'admin', N'ADMIN' , N'81a1ef6a-6775-421d-80e1-30bdcf491305')

GO