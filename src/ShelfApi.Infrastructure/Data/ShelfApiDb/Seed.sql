USE
    [ShelfApiDb]
GO

INSERT INTO [dbo].[Roles] ([Name], [NormalizedName], [ConcurrencyStamp])
VALUES (N'USER', N'USER', N'05ab85c5-9b38-4a07-bf0d-3cac4e7fb3b5'),
       (N'ADMIN', N'ADMIN', N'81a1ef6a-6775-421d-80e1-30bdcf491305')

GO

INSERT INTO public."ProjectSettings" ([Id], [Data])
VALUES (1,
        '{"jwtKey":"some-jwt-key-6775-421d-80e1", "jwtIssuer":"http://localhost:5251", "jwtAudience":"http://localhost:5251"}'),
       (2, '{"TaxPercentage":10}');

GO