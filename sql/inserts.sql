 
Use BackEndTestDocker

INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20210325174213_Initial', N'5.0.4')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'408d3aa6-b39f-473e-a275-4f54303bbb7f', N'Junior', N'95312167037', N'junior@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'2a7e6458-98fd-4415-a7b7-7c11fc6270fa', N'Paulo Henrique', N'08272217627', N'paulo@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'bda93dbb-4ca2-48a6-8637-a176eb4a6098', N'Paulo', N'64188680059', N'paulo@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'1e5c0a5f-7f5e-4eb0-a4a7-bd6a35bb053e', N'Mauro', N'37747423080', N'mauro@gmail.com')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'e69f58f3-5465-4619-8f9b-cbd0c13d93b0', N'Teste', N'11111111111', N'teste@teste')
GO
INSERT [dbo].[Desenvolvedor] ([Id], [Nome], [CPF], [Email]) VALUES (N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'Marcelo', N'08272217627', N'marcelo@gmail.com')
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'7ebb7935-ed98-444a-9247-4390d16ed615', N'Google', CAST(N'2021-05-12T00:00:00' AS SmallDateTime), 2, NULL)
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'277ebfc8-a657-4714-bd3b-45645ff2a82e', N'Bradesco', CAST(N'2021-10-29T00:00:00' AS SmallDateTime), 1, N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef')
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'9c8d30a6-586e-418d-98bb-7eedb1f612b8', N'Bradesco1', CAST(N'2021-10-29T00:00:00' AS SmallDateTime), 1, NULL)
GO
INSERT [dbo].[Aplicativo] ([Id], [Nome], [DataLancamento], [Plataforma], [Id_DesenvolvedorResponsavel]) VALUES (N'790687bb-682c-480d-811e-c999790c298f', N'Facebook', CAST(N'2021-03-11T00:00:00' AS SmallDateTime), 1, N'e69f58f3-5465-4619-8f9b-cbd0c13d93b0')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'a3e2d4fb-9e97-401c-847c-23fb5b3f684a', N'408d3aa6-b39f-473e-a275-4f54303bbb7f', N'790687bb-682c-480d-811e-c999790c298f')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'd06ef029-a104-4f8c-94b6-2209c67a17ea', N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'7ebb7935-ed98-444a-9247-4390d16ed615')
GO
INSERT [dbo].[DesenvolvedorAplicativo] ([Id], [Id_Desenvolvedor], [Id_Aplicativo]) VALUES (N'5ed2e9fe-4a41-4b0b-b860-77522d8638fc', N'9a82f9ca-0bc5-4e11-a417-e2bf8b7308ef', N'790687bb-682c-480d-811e-c999790c298f')
GO
 