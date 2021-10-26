/****** Object:  Table [dbo].[Comentarios]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Comentarios](
	[Idcoment] [int] IDENTITY(1,1) NOT NULL,
	[comentario] [nvarchar](250) NOT NULL,
	[DateComent] [datetime] NOT NULL,
	[postID] [int] NULL,
	[userID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Idcoment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

ALTER TABLE [dbo].[Comentarios] ADD  DEFAULT (getdate()) FOR [DateComent]
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [fk_ArtComent] FOREIGN KEY([postID])
REFERENCES [dbo].[Articulo] ([IdArticulo])
ON UPDATE CASCADE
ON DELETE CASCADE
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [fk_ArtComent]
ALTER TABLE [dbo].[Comentarios]  WITH CHECK ADD  CONSTRAINT [fk_UserComent] FOREIGN KEY([userID])
REFERENCES [dbo].[Usuarios] ([IdUser])
ON UPDATE CASCADE
ON DELETE CASCADE
ALTER TABLE [dbo].[Comentarios] CHECK CONSTRAINT [fk_UserComent]