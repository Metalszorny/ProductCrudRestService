USE [ProductCrudRestServiceDatabase]
GO

/****** Object:  Table [dbo].[Products]    Script Date: 17/07/2022 21:38:12 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Products](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ean] [nvarchar](max) NOT NULL,
	[price] [nvarchar](max) NOT NULL,
	[is_deleted] [tinyint] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Products] ADD  CONSTRAINT [DF_Products_is_deleted]  DEFAULT ((0)) FOR [is_deleted]
GO

INSERT INTO [dbo].[Products] (ean, price, is_deleted) VALUES ("EAN1", "99.95", 0);
INSERT INTO [dbo].[Products] (ean, price, is_deleted) VALUES ("EAN2", "16.15", 0);
INSERT INTO [dbo].[Products] (ean, price, is_deleted) VALUES ("EAN3", "12", 0);
GO
