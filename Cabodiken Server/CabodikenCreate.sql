USE [master]
GO
/****** Object:  Database [Cabodiken]    Script Date: 01/08/2014 16:34:46 ******/
CREATE DATABASE [Cabodiken] ON  PRIMARY 
( NAME = N'Cabodiken', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Cabodiken.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'Cabodiken_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.SQLEXPRESS\MSSQL\DATA\Cabodiken_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [Cabodiken] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Cabodiken].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Cabodiken] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Cabodiken] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Cabodiken] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Cabodiken] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Cabodiken] SET ARITHABORT OFF 
GO
ALTER DATABASE [Cabodiken] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Cabodiken] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [Cabodiken] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Cabodiken] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Cabodiken] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Cabodiken] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Cabodiken] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Cabodiken] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Cabodiken] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Cabodiken] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Cabodiken] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Cabodiken] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Cabodiken] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Cabodiken] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Cabodiken] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Cabodiken] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Cabodiken] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Cabodiken] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Cabodiken] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Cabodiken] SET  MULTI_USER 
GO
ALTER DATABASE [Cabodiken] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Cabodiken] SET DB_CHAINING OFF 
GO
USE [Cabodiken]
GO
/****** Object:  StoredProcedure [dbo].[add_card]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_card]
	@name varchar(50),
	@frontImageId int,
	@backImageId int,
	@deckId int
AS
BEGIN
	DECLARE @id int = -1;
	SELECT @id = [id] FROM [dbo].[cards] WHERE [name] = @name AND [deckId] = @deckId;

	IF @id != -1
		BEGIN
			SELECT @id AS 'id'
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[cards] ([name],[frontImageId],[backImageId],[deckId])
			OUTPUT inserted.id
			VALUES (@name, @frontImageId, @backImageId, @deckId);
		END
END

GO
/****** Object:  StoredProcedure [dbo].[add_deck]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_deck]
	-- Add the parameters for the stored procedure here
	@name varchar(50),
	@ownerId int,
	@isPublic bit
AS
BEGIN
	DECLARE @id int = -1;
	SELECT @id = [id] FROM [dbo].[decks] WHERE [name] = @name;

	IF @id != -1
		BEGIN
			SELECT @id AS 'id'
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[decks] ([name],[ownerId],[isPublic]) 
			OUTPUT inserted.id
			VALUES (@name, @ownerId, @isPublic);
		END
	
END

GO
/****** Object:  StoredProcedure [dbo].[add_friend]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_friend]
( @userId int, @friendId int)
AS
BEGIN
	IF dbo.friend_exists(@userId, @friendId) = 0
		BEGIN
			INSERT INTO [friends] ([userId], [friendId]) VALUES (@userId, @friendId)
			SELECT 1 AS 'Result'
		END
	ELSE
		SELECT 0 AS 'Result'
END

GO
/****** Object:  StoredProcedure [dbo].[add_image]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_image]
	-- Add the parameters for the stored procedure here
	@base64string varchar(MAX),
	@name varchar(50)
AS
BEGIN
	DECLARE @id int = -1;
	SELECT @id = [id] FROM [dbo].[images] WHERE [name] = @name;

	IF @id != -1
		BEGIN
			SELECT @id AS 'id';
		END
	ELSE
		BEGIN
			INSERT INTO [dbo].[images] ([base64string], [name])
			OUTPUT INSERTED.[id]
			VALUES (@base64string, @name);
			RETURN;
		END
END

GO
/****** Object:  StoredProcedure [dbo].[add_user]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[add_user]
	@name varchar(30), 
	@hostId int, 
	@password varchar(100),
	@message nvarchar(50) = ''
AS
BEGIN
	INSERT INTO [users]([name], [hostId], [password], [message]) VALUES (@name, @hostId, @password, @message);
	SELECT 1 AS 'Result'
END

GO
/****** Object:  StoredProcedure [dbo].[get_board_data]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[get_board_data]
	@boardId int
AS
BEGIN
	SELECT [boards].[id], [boards].[name], [images].[base64string] FROM [boards] INNER JOIN [images] ON [boards].[imageId] = [images].[id] WHERE [boards].[id] = @boardId;
END

GO
/****** Object:  StoredProcedure [dbo].[get_deck_data]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_deck_data]
	@deckId int
AS
BEGIN
	SELECT [cards].[id], [cards].[name], [frontImages].[base64string] AS 'FromtImage', [backImages].[base64string] AS 'BackImage' 
	FROM [cards] 
	INNER JOIN [images] AS [frontImages] ON [cards].[frontImageId] = [frontImages].[id] 
	INNER JOIN [images] AS [backImages] ON [cards].[backImageId] = [backImages].[id] 
	WHERE [cards].[deckId] = @deckId;
END

GO
/****** Object:  StoredProcedure [dbo].[get_dice_data]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_dice_data]
	@diceId int
AS
BEGIN
	SELECT [dice_sides].[diceFace], [images].[base64string]
	FROM [dice_sides] 
	INNER JOIN [images] ON [dice_sides].[imageId] = [images].[id] 
	WHERE [dice_sides].[diceId] = @diceId
	ORDER BY [dice_sides].[diceFace] ASC;
END

GO
/****** Object:  StoredProcedure [dbo].[get_friends]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_friends]
	@userId int
AS
BEGIN
	SELECT [id], [name], [hostId], [message] FROM [users] WHERE [id] IN (SELECT [friendId] FROM [friends] WHERE [friends].[userId] = @userId);
END

GO
/****** Object:  StoredProcedure [dbo].[get_game_boards]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_game_boards]
	@gameId int
AS
BEGIN
	SELECT [id], [name] FROM [boards] WHERE [id] IN (SELECT [id] FROM [game_boards] WHERE [gameId] = @gameId);
END

GO
/****** Object:  StoredProcedure [dbo].[get_game_decks]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_game_decks]
	@gameId int
AS
BEGIN
	SELECT [id], [name] FROM [decks] WHERE [id] IN (SELECT [id] FROM [game_decks] WHERE [gameId] = @gameId);
END

GO
/****** Object:  StoredProcedure [dbo].[get_game_dices]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_game_dices]
	@gameId int
AS
BEGIN
	SELECT [id], [name] FROM [dices] WHERE [id] IN (SELECT [id] FROM [game_dices] WHERE [gameId] = @gameId);
END

GO
/****** Object:  StoredProcedure [dbo].[get_game_tokens]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_game_tokens]
	@gameId int
AS
BEGIN
	SELECT [id], [name] FROM [tokens] WHERE [id] IN (SELECT [id] FROM [game_tokens] WHERE [gameId] = @gameId);
END

GO
/****** Object:  StoredProcedure [dbo].[get_token_data]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_token_data]
	@tokenId int
AS
BEGIN
	SELECT [token_sides].[horizontalIndex], [token_sides].[verticalIndex], [images].[base64string]
	FROM [token_sides] 
	INNER JOIN [images] ON [token_sides].[imageId] = [images].[id] 
	WHERE [token_sides].[tokenId] = @tokenId;
END

GO
/****** Object:  StoredProcedure [dbo].[get_user_by_name]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_by_name] 
	-- Add the parameters for the stored procedure here
	@name varchar(30),
	@hostId int
AS
BEGIN
	SET NOCOUNT ON;
	SELECT [id], [name], [hostId], [message] FROM users WHERE [name] = @name AND [hostId] = @hostId;
END

GO
/****** Object:  StoredProcedure [dbo].[get_user_decks]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_decks]
	@userId int
AS
BEGIN
	SELECT [id], [name] FROM [decks] WHERE [ownerId] = @userId OR [isPublic] = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[get_user_dices]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_dices]
	@userId int
AS
BEGIN
	SELECT [id], [name] FROM [dices] WHERE [ownerId] = @userId OR [isPublic] = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[get_user_games]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_games]
	@userId int
AS
BEGIN
	SELECT [id], [name] FROM [games] WHERE [ownerId] = @userId OR [isPublic] = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[get_user_tokens]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_tokens]
	@userId int
AS
BEGIN
	SELECT [id], [name] FROM [tokens] WHERE [ownerId] = @userId OR [isPublic] = 1;
END

GO
/****** Object:  StoredProcedure [dbo].[modify_user_message]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[modify_user_message]
	@userId int,
	@message nvarchar(50)
AS
BEGIN
	UPDATE [users] SET [message] = @message WHERE [users].[id] = @userId;
END

GO
/****** Object:  UserDefinedFunction [dbo].[card_exists]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[card_exists]
(
	@name VARCHAR(50),
	@deckId INT
)
RETURNS BIT
AS
BEGIN

	IF EXISTS (SELECT * FROM [dbo].[cards] WHERE [name] = @name AND [deckId] = @deckId)
		RETURN 1
	
	RETURN 0

END

GO
/****** Object:  UserDefinedFunction [dbo].[deck_exists]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[deck_exists]
(
	@name VARCHAR(50)
)
RETURNS BIT
AS
BEGIN
	IF EXISTS (SELECT * FROM [dbo].[decks] WHERE [name] = @name)
		RETURN 1
	
	RETURN 0
END

GO
/****** Object:  UserDefinedFunction [dbo].[friend_exists]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[friend_exists] (@userId INT, @friendId INT) 
RETURNS BIT
BEGIN
	IF EXISTS (SELECT * FROM friends WHERE friends.userId = @userId AND friends.friendId = @friendId)
		RETURN 1
	
	RETURN 0
END
GO
/****** Object:  UserDefinedFunction [dbo].[image_exists]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[image_exists]
(
	@name VARCHAR(50)
)
RETURNS BIT
AS
BEGIN

	IF EXISTS (SELECT * FROM [dbo].[images] WHERE [name] = @name)
		RETURN 1
	
	RETURN 0
END

GO
/****** Object:  UserDefinedFunction [dbo].[user_exists]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[user_exists]
(
	@name varchar(30),
	@hostId int
)
RETURNS bit
AS
BEGIN
	IF EXISTS(SELECT * FROM [users] WHERE [users].[name] = @name AND [users].[hostId] = @hostId)
		RETURN 1

	RETURN 0
END

GO
/****** Object:  UserDefinedFunction [dbo].[validate_user_password]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE FUNCTION [dbo].[validate_user_password]
(
	@name varchar(30),
	@hostId int,
	@password varchar(100)
)
RETURNS bit
AS
BEGIN
	IF EXISTS(SELECT * FROM [users] WHERE [users].[name] = @name AND [users].[hostId] = @hostId AND [users].[password] = @password)
		RETURN 1

	RETURN 0
END

GO
/****** Object:  Table [dbo].[boards]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[boards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ownerId] [int] NOT NULL,
	[imageId] [int] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_boards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[cards]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[cards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[frontImageId] [int] NOT NULL,
	[backImageId] [int] NOT NULL,
	[deckId] [int] NOT NULL,
 CONSTRAINT [PK_cards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[decks]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[decks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ownerId] [int] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_decks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[dice_sides]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[dice_sides](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[diceFace] [int] NOT NULL,
	[diceId] [int] NOT NULL,
	[imageId] [int] NOT NULL,
 CONSTRAINT [PK_dice_sides] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[dices]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[dices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ownerId] [int] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_dices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[friends]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[friends](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[userId] [int] NOT NULL,
	[friendId] [int] NOT NULL,
 CONSTRAINT [PK_friends] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[game_boards]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[game_boards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gameId] [int] NOT NULL,
	[boardId] [int] NOT NULL,
 CONSTRAINT [PK_game_boards] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[game_decks]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[game_decks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gameId] [int] NOT NULL,
	[deckId] [int] NOT NULL,
 CONSTRAINT [PK_game_decks] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[game_dices]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[game_dices](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gameId] [int] NOT NULL,
	[diceId] [int] NOT NULL,
 CONSTRAINT [PK_game_dices] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[game_tokens]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[game_tokens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[gameId] [int] NOT NULL,
	[tokenId] [int] NOT NULL,
 CONSTRAINT [PK_game_tokens] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[games]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[games](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ownerId] [int] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_games] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[hosts]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[hosts](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_hosts] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[images]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[images](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[base64string] [varchar](max) NOT NULL,
	[name] [varchar](50) NOT NULL,
 CONSTRAINT [PK_images] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[token_sides]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[token_sides](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[horizontalIndex] [int] NOT NULL,
	[verticalIndex] [int] NOT NULL,
	[imageId] [int] NOT NULL,
	[tokenId] [int] NOT NULL,
 CONSTRAINT [PK_token_sides] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[tokens]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tokens](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[ownerId] [int] NOT NULL,
	[isPublic] [bit] NOT NULL,
 CONSTRAINT [PK_tokens] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[users]    Script Date: 01/08/2014 16:34:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[users](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](30) NOT NULL,
	[hostId] [int] NOT NULL,
	[password] [varchar](100) NOT NULL,
	[message] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_users] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[boards]  WITH CHECK ADD  CONSTRAINT [board_image] FOREIGN KEY([imageId])
REFERENCES [dbo].[images] ([id])
GO
ALTER TABLE [dbo].[boards] CHECK CONSTRAINT [board_image]
GO
ALTER TABLE [dbo].[boards]  WITH CHECK ADD  CONSTRAINT [board_owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[boards] CHECK CONSTRAINT [board_owner]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD  CONSTRAINT [card_back_image] FOREIGN KEY([backImageId])
REFERENCES [dbo].[images] ([id])
GO
ALTER TABLE [dbo].[cards] CHECK CONSTRAINT [card_back_image]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD  CONSTRAINT [card_deck] FOREIGN KEY([deckId])
REFERENCES [dbo].[decks] ([id])
GO
ALTER TABLE [dbo].[cards] CHECK CONSTRAINT [card_deck]
GO
ALTER TABLE [dbo].[cards]  WITH CHECK ADD  CONSTRAINT [card_front_image] FOREIGN KEY([frontImageId])
REFERENCES [dbo].[images] ([id])
GO
ALTER TABLE [dbo].[cards] CHECK CONSTRAINT [card_front_image]
GO
ALTER TABLE [dbo].[decks]  WITH CHECK ADD  CONSTRAINT [deck_owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[decks] CHECK CONSTRAINT [deck_owner]
GO
ALTER TABLE [dbo].[dice_sides]  WITH CHECK ADD  CONSTRAINT [dice_side_dice] FOREIGN KEY([diceId])
REFERENCES [dbo].[dices] ([id])
GO
ALTER TABLE [dbo].[dice_sides] CHECK CONSTRAINT [dice_side_dice]
GO
ALTER TABLE [dbo].[dice_sides]  WITH CHECK ADD  CONSTRAINT [dice_side_image] FOREIGN KEY([imageId])
REFERENCES [dbo].[images] ([id])
GO
ALTER TABLE [dbo].[dice_sides] CHECK CONSTRAINT [dice_side_image]
GO
ALTER TABLE [dbo].[dices]  WITH CHECK ADD  CONSTRAINT [dice_owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[dices] CHECK CONSTRAINT [dice_owner]
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD  CONSTRAINT [friend_friend] FOREIGN KEY([friendId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[friends] CHECK CONSTRAINT [friend_friend]
GO
ALTER TABLE [dbo].[friends]  WITH CHECK ADD  CONSTRAINT [friend_user] FOREIGN KEY([userId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[friends] CHECK CONSTRAINT [friend_user]
GO
ALTER TABLE [dbo].[game_boards]  WITH CHECK ADD  CONSTRAINT [game_board_board] FOREIGN KEY([boardId])
REFERENCES [dbo].[boards] ([id])
GO
ALTER TABLE [dbo].[game_boards] CHECK CONSTRAINT [game_board_board]
GO
ALTER TABLE [dbo].[game_boards]  WITH CHECK ADD  CONSTRAINT [game_board_game] FOREIGN KEY([gameId])
REFERENCES [dbo].[games] ([id])
GO
ALTER TABLE [dbo].[game_boards] CHECK CONSTRAINT [game_board_game]
GO
ALTER TABLE [dbo].[game_decks]  WITH CHECK ADD  CONSTRAINT [game_deck_deck] FOREIGN KEY([deckId])
REFERENCES [dbo].[decks] ([id])
GO
ALTER TABLE [dbo].[game_decks] CHECK CONSTRAINT [game_deck_deck]
GO
ALTER TABLE [dbo].[game_decks]  WITH CHECK ADD  CONSTRAINT [game_deck_game] FOREIGN KEY([gameId])
REFERENCES [dbo].[games] ([id])
GO
ALTER TABLE [dbo].[game_decks] CHECK CONSTRAINT [game_deck_game]
GO
ALTER TABLE [dbo].[game_dices]  WITH CHECK ADD  CONSTRAINT [game_dice_dice] FOREIGN KEY([diceId])
REFERENCES [dbo].[dices] ([id])
GO
ALTER TABLE [dbo].[game_dices] CHECK CONSTRAINT [game_dice_dice]
GO
ALTER TABLE [dbo].[game_dices]  WITH CHECK ADD  CONSTRAINT [game_dice_game] FOREIGN KEY([gameId])
REFERENCES [dbo].[games] ([id])
GO
ALTER TABLE [dbo].[game_dices] CHECK CONSTRAINT [game_dice_game]
GO
ALTER TABLE [dbo].[game_tokens]  WITH CHECK ADD  CONSTRAINT [game_token_game] FOREIGN KEY([gameId])
REFERENCES [dbo].[games] ([id])
GO
ALTER TABLE [dbo].[game_tokens] CHECK CONSTRAINT [game_token_game]
GO
ALTER TABLE [dbo].[game_tokens]  WITH CHECK ADD  CONSTRAINT [game_token_token] FOREIGN KEY([tokenId])
REFERENCES [dbo].[tokens] ([id])
GO
ALTER TABLE [dbo].[game_tokens] CHECK CONSTRAINT [game_token_token]
GO
ALTER TABLE [dbo].[games]  WITH CHECK ADD  CONSTRAINT [game_owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[games] CHECK CONSTRAINT [game_owner]
GO
ALTER TABLE [dbo].[token_sides]  WITH CHECK ADD  CONSTRAINT [token_side_image] FOREIGN KEY([imageId])
REFERENCES [dbo].[images] ([id])
GO
ALTER TABLE [dbo].[token_sides] CHECK CONSTRAINT [token_side_image]
GO
ALTER TABLE [dbo].[token_sides]  WITH CHECK ADD  CONSTRAINT [token_side_token] FOREIGN KEY([tokenId])
REFERENCES [dbo].[tokens] ([id])
GO
ALTER TABLE [dbo].[token_sides] CHECK CONSTRAINT [token_side_token]
GO
ALTER TABLE [dbo].[tokens]  WITH CHECK ADD  CONSTRAINT [token_owner] FOREIGN KEY([ownerId])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[tokens] CHECK CONSTRAINT [token_owner]
GO
ALTER TABLE [dbo].[users]  WITH CHECK ADD  CONSTRAINT [user_hosts] FOREIGN KEY([hostId])
REFERENCES [dbo].[hosts] ([id])
GO
ALTER TABLE [dbo].[users] CHECK CONSTRAINT [user_hosts]
GO
USE [master]
GO
ALTER DATABASE [Cabodiken] SET  READ_WRITE 
GO
