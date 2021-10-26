/****** Object:  Table [dbo].[Articulo]    Committed by VersionSQL https://www.versionsql.com ******/

SET ANSI_NULLS ON
SET QUOTED_IDENTIFIER ON
CREATE TABLE [dbo].[Articulo](
	[IdArticulo] [int] IDENTITY(1,1) NOT NULL,
	[Titulo] [nvarchar](75) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Contenido] [nvarchar](max) NOT NULL,
	[categoriaID] [int] NOT NULL,
	[Vista_previa] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Articulo] PRIMARY KEY CLUSTERED 
(
	[IdArticulo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

ALTER TABLE [dbo].[Articulo] ADD  DEFAULT (getdate()) FOR [Fecha]
ALTER TABLE [dbo].[Articulo]  WITH CHECK ADD  CONSTRAINT [fk_Categoria_Art] FOREIGN KEY([categoriaID])
REFERENCES [dbo].[Categoria] ([Idcategoria])
ON UPDATE CASCADE
ON DELETE CASCADE
ALTER TABLE [dbo].[Articulo] CHECK CONSTRAINT [fk_Categoria_Art]