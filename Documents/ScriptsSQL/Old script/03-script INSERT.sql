USE [Form115]
GO
SET IDENTITY_INSERT [dbo].[Categories] ON 

INSERT [dbo].[Categories] ([IdCategorie], [Description]) VALUES (1, N'1 étoiles')
INSERT [dbo].[Categories] ([IdCategorie], [Description]) VALUES (2, N'2 étoiles')
INSERT [dbo].[Categories] ([IdCategorie], [Description]) VALUES (3, N'3 étoiles')
INSERT [dbo].[Categories] ([IdCategorie], [Description]) VALUES (4, N'4 étoiles')
INSERT [dbo].[Categories] ([IdCategorie], [Description]) VALUES (5, N'5 étoiles')
SET IDENTITY_INSERT [dbo].[Categories] OFF
SET IDENTITY_INSERT [dbo].[Hotels] ON 

INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (2, N'Hotel Manhattan', 1, 2, N'Dans ce luxurieux hotel...', NULL)
INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (3, N'Hotel California', 5, 3, N'Hotel de passe...', NULL)
INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (5, N'El Gringo Hotel', 84, 5, N'Hotel festif', NULL)
INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (6, N'El Gringo Hotel', 1, 2, N'Hotel festif', NULL)
INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (8, N'El Gringo Hotel', 14, 2, N'Hotel festif', N'download.jpg')
INSERT [dbo].[Hotels] ([IdHotel], [Nom], [IdVille], [Categorie], [Description], [Photo]) VALUES (1002, N'Raymond Bar/Hotel', 2974, 4, N'Hotel politiquement correct', NULL)
SET IDENTITY_INSERT [dbo].[Hotels] OFF
SET IDENTITY_INSERT [dbo].[Sejours] ON 

INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (5, 2, 1)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (3, 2, 5)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (1, 2, 11)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (4, 2, 45)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (2, 2, 50)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (6, 3, 5)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (1004, 8, 8)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (1005, 8, 10)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (1002, 1002, 7)
INSERT [dbo].[Sejours] ([IdSejour], [IdHotel], [Duree]) VALUES (1003, 1002, 14)
SET IDENTITY_INSERT [dbo].[Sejours] OFF
SET IDENTITY_INSERT [dbo].[Produits] ON 

INSERT [dbo].[Produits] ([IdProduit], [IdSejour], [NbPlaces], [DateDepart], [Prix]) VALUES (2, 1, 50, CAST(N'2015-11-09' AS Date), CAST(15000 AS Numeric(18, 0)))
INSERT [dbo].[Produits] ([IdProduit], [IdSejour], [NbPlaces], [DateDepart], [Prix]) VALUES (1002, 1005, 25, CAST(N'2015-10-15' AS Date), CAST(900 AS Numeric(18, 0)))
INSERT [dbo].[Produits] ([IdProduit], [IdSejour], [NbPlaces], [DateDepart], [Prix]) VALUES (1003, 6, 10, CAST(N'2015-12-15' AS Date), CAST(259 AS Numeric(18, 0)))
INSERT [dbo].[Produits] ([IdProduit], [IdSejour], [NbPlaces], [DateDepart], [Prix]) VALUES (1004, 6, 10, CAST(N'2015-11-15' AS Date), CAST(310 AS Numeric(18, 0)))
INSERT [dbo].[Produits] ([IdProduit], [IdSejour], [NbPlaces], [DateDepart], [Prix]) VALUES (1005, 1005, 70, CAST(N'2015-12-03' AS Date), CAST(307 AS Numeric(18, 0)))
SET IDENTITY_INSERT [dbo].[Produits] OFF
SET IDENTITY_INSERT [dbo].[Utilisateurs] ON 

INSERT [dbo].[Utilisateurs] ([IdUtilisateur], [Nom], [Prenom], [Telephone]) VALUES (1, N'Hungis', N'Martina', N'0123456789')
INSERT [dbo].[Utilisateurs] ([IdUtilisateur], [Nom], [Prenom], [Telephone]) VALUES (2, N'Martinez', N'Jacques', N'0198765432')
INSERT [dbo].[Utilisateurs] ([IdUtilisateur], [Nom], [Prenom], [Telephone]) VALUES (3, N'Camara', N'Aramac', N'0144778899')
INSERT [dbo].[Utilisateurs] ([IdUtilisateur], [Nom], [Prenom], [Telephone]) VALUES (4, N'Jones', N'Jason', NULL)
SET IDENTITY_INSERT [dbo].[Utilisateurs] OFF
SET IDENTITY_INSERT [dbo].[Reservations] ON 

INSERT [dbo].[Reservations] ([IdReservation], [IdProduit], [Quantity], [IdUtilisateur]) VALUES (1, 1002, 2, 1)
INSERT [dbo].[Reservations] ([IdReservation], [IdProduit], [Quantity], [IdUtilisateur]) VALUES (2, 1005, 5, 2)
INSERT [dbo].[Reservations] ([IdReservation], [IdProduit], [Quantity], [IdUtilisateur]) VALUES (3, 2, 1, 3)
INSERT [dbo].[Reservations] ([IdReservation], [IdProduit], [Quantity], [IdUtilisateur]) VALUES (4, 1005, 1, 4)
INSERT [dbo].[Reservations] ([IdReservation], [IdProduit], [Quantity], [IdUtilisateur]) VALUES (5, 1003, 1, 2)
SET IDENTITY_INSERT [dbo].[Reservations] OFF

