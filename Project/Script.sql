
 CREATE TABLE [dbo].[ImmovableType](
   [ImmovableTypeId] [int] IDENTITY(1,1) NOT NULL,
   [ImmovableName] [varchar](50) NOT NULL,
   CONSTRAINT [PK_ImmovableType] PRIMARY KEY CLUSTERED ([ImmovableTypeId] ASC)
) ON [PRIMARY]

CREATE TABLE [dbo].[Immovable](
   [ImmovableId] [int] IDENTITY(1,1) NOT NULL,
   [ImmovableTypeId] [int] NOT NULL,
   [Address] [varchar] NOT NULL,
   [OwnerId] [int] NOT NULL,
   [Cost] [decimal] NOT NULL,
   CONSTRAINT [PK_Immovable] PRIMARY KEY CLUSTERED ([ImmovableId] ASC)
 ) ON [PRIMARY]

 CREATE TABLE [dbo].[Cadastr](
   [CadastrId] [int] IDENTITY(1,1) NOT NULL,
   [CadastrTypeId] [int] NOT NULL,  
   CONSTRAINT [PK_Cadastr] PRIMARY KEY CLUSTERED ([CadastrId] ASC)
 ) ON [PRIMARY]

  CREATE TABLE [dbo].[Land](
   [LandId] [int] IDENTITY(1,1) NOT NULL,
   [LandTypeId] [int] NOT NULL,
   [OwnerId] [int] NOT NULL,
   [Cost] [decimal] NOT NULL,
   [Area] [decimal] NOT NULL,
   [Address] [varchar] (200) NOT NULL,   
   CONSTRAINT [FK_Land] PRIMARY KEY CLUSTERED ([LandId] ASC)
 ) ON [PRIMARY]

  CREATE TABLE [dbo].[LandType](
   [LandTypeId] [int] IDENTITY(1,1) NOT NULL,
   [Name] [int] NOT NULL  
   CONSTRAINT [FK_LandType] PRIMARY KEY CLUSTERED ([LandTypeId] ASC)
 ) ON [PRIMARY]

   CREATE TABLE [dbo].[LicenseRequest](
   [LicenseId] [int] IDENTITY(1,1) NOT NULL,
   [CadastrId] [int] NOT NULL,  
   CONSTRAINT [FK_TransportType] PRIMARY KEY CLUSTERED ([LicenseId] ASC)
 ) ON [PRIMARY]

CREATE TABLE [dbo].[Owner](
   [OwnerId] [int] IDENTITY(1,1) NOT NULL,
   [Name] [varchar](50) NOT NULL,
   CONSTRAINT [PK_Owner] PRIMARY KEY CLUSTERED ([OwnerId] ASC)
 ) ON [PRIMARY]