Imports System.Data.Common
Imports MySql.Data.MySqlClient
Imports System.Data.SqlClient
Imports MFierro.Cabodiken.DomainObjects

Namespace DataObjects
    Public Class DataManager

#Region "Properties"

        Private Shared _instance As DataManager = New DataManager()
        Private _quoteChar As Char
        Private _databaseType As DatabaseType = DatabaseType.MSSQL

        Public Shared ReadOnly Property Instance As DataManager
            Get
                Return _instance
            End Get
        End Property
#End Region

#Region "Constructors"

        Private Sub New()
            If _databaseType = DatabaseType.MySQL Then
                _quoteChar = """"c
            ElseIf _databaseType = DatabaseType.MSSQL Then
                _quoteChar = "'"c
            End If
        End Sub

#End Region

#Region "Methods"

        Public Sub ModifyUserMessage(userId As Integer, message As String)
            Dim command As DbCommand
            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "modify_user_message", _
                                                    userId.ToString, Quote(message)))

            command.Connection.Open()

            Try
                command.ExecuteNonQuery()
            Finally

                command.Connection.Close()
                command.Dispose()
            End Try

        End Sub

        Public Function GetFriends(userId As Integer) As List(Of UserData)
            Dim friendsList As New List(Of UserData)
            Dim command As DbCommand = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_friends", _
                                                                     userId.ToString))
            Dim dataReader As DbDataReader

            command.Connection.Open()
            dataReader = command.ExecuteReader()
            Try
                
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim id As Integer = dataReader.GetInt32(0)
                        Dim userName As String = dataReader.GetString(1)
                        Dim hostId As Integer = dataReader.GetInt32(2)
                        Dim message As String = GetNullableString(dataReader, 3)
                        friendsList.Add(New UserData(id, userName, CType(hostId, Host), message, False))
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try
            Return friendsList
        End Function

        Public Function GetUserObjects(userId As Integer, objectType As String) As List(Of ObjectData)

            Dim customObjects As New List(Of ObjectData)
            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim storeProcedure As String = ""

            Select Case objectType
                Case "DECK"
                    storeProcedure = "get_user_decks"
                Case "DICE"
                    storeProcedure = "get_user_dices"
                Case "TOKEN"
                    storeProcedure = "get_user_tokens"
                Case "GAME"
                    storeProcedure = "get_user_games"
            End Select

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, storeProcedure, userId.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim id As Integer = dataReader.GetInt32(0)
                        Dim name As String = dataReader.GetString(1)
                        customObjects.Add(New ObjectData(id, name, objectType))
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            Return customObjects

        End Function

        Public Function GetGameObjects(gameId As Integer, objectType As String) As List(Of ObjectData)

            Dim gameObjects As New List(Of ObjectData)
            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim storeProcedure As String = ""

            Select Case objectType
                Case "DECK"
                    storeProcedure = "get_game_decks"
                Case "DICE"
                    storeProcedure = "get_game_dices"
                Case "TOKEN"
                    storeProcedure = "get_game_tokens"
                Case "BOARD"
                    storeProcedure = "get_game_boards"
            End Select

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, storeProcedure, gameId.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim id As Integer = dataReader.GetInt32(0)
                        Dim name As String = dataReader.GetString(1)
                        gameObjects.Add(New ObjectData(id, name, objectType))
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            Return gameObjects

        End Function

        Public Function GetDeckData(deck As ObjectData) As DeckData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim cards As New List(Of CardData)

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_deck_data", deck.Id.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim id As Integer = dataReader.GetInt32(0)
                        Dim name As String = dataReader.GetString(1)
                        Dim frontImage As String = dataReader.GetString(2)
                        Dim backImage As String = dataReader.GetString(3)

                        cards.Add(New CardData(id, name, backImage, frontImage))
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            Return New DeckData(deck.Id, deck.Name, cards.ToArray())

        End Function

        Public Function GetBoardData(board As ObjectData) As BoardData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim boardData As BoardData = Nothing

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_board_data", board.Id.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim id As Integer = dataReader.GetInt32(0)
                        Dim name As String = dataReader.GetString(1)
                        Dim image As String = dataReader.GetString(2)

                        boardData = New BoardData(id, name, image)
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            Return boardData

        End Function

        Public Function GetDiceData(dice As ObjectData) As DiceData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim sides As New List(Of String)

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_dice_data", dice.Id.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim image As String = dataReader.GetString(1)

                        sides.Add(image)
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            Return New DiceData(dice.Id, dice.Name, sides.ToArray())

        End Function

        Public Function GetTokenData(token As ObjectData) As TokenData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim sides As New List(Of List(Of String))
            Dim sidesMatrix As String()()

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_deck_data", token.Id.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()

            Try
                If dataReader.HasRows Then
                    Dim lastIndex As Integer = -1
                    Do While dataReader.Read()
                        Dim newIndex As Integer = dataReader.GetInt32(0)
                        Dim image As String = dataReader.GetString(2)

                        If Not lastIndex = newIndex Then
                            lastIndex = newIndex
                            sides.Add(New List(Of String))
                        End If

                        sides(sides.Count - 1).Add(image)
                    Loop
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try

            sidesMatrix = New String(sides.Count)() {}

            For horizontalIndex As Integer = 0 To sides.Count - 1
                sidesMatrix(horizontalIndex) = New String(sides(horizontalIndex).Count) {}
                For verticalIndex As Integer = 0 To sides(horizontalIndex).Count - 1
                    sidesMatrix(horizontalIndex)(verticalIndex) = sides(verticalIndex)(horizontalIndex)
                Next
            Next

            Return New TokenData(token.Id, token.Name, sidesMatrix)

        End Function

        Public Function GetUser(username As String, host As Integer) As UserData
            Dim user As UserData = Nothing
            Dim command As DbCommand
            Dim dataReader As DbDataReader
            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_user_by_name", _
                                                    Quote(username), host.ToString))
            command.Connection.Open()
            dataReader = command.ExecuteReader()
            Try
                If dataReader.HasRows Then
                    dataReader.Read()
                    Dim id As Integer = dataReader.GetInt32(0)
                    Dim message As String = GetNullableString(dataReader, 3)
                    user = New UserData(id, username, CType(host, DataObjects.Host), message, False)
                End If
            Finally
                dataReader.Close()
                command.Connection.Close()
                command.Dispose()
            End Try
            Return user
        End Function

        Public Function ValidateUser(username As String, host As Integer, password As String) As UserData
            Dim command As DbCommand
            Dim isValid As Boolean
            command = GetDBCommand(GetCommandString(CommandType.SqlFunction, "validate_user_password", _
                                                    Quote(username), host.ToString, Quote(password)))

            command.Connection.Open()
            Try
                isValid = CType(command.ExecuteScalar(), Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            If isValid Then
                Return GetUser(username, host)
            Else
                Return Nothing
            End If
        End Function

        Public Function RegisterUser(userName As String, host As Integer, password As String) As Boolean

            Dim command As DbCommand
            Dim wasInserted As Boolean = False

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "add_user", _
                                                    Quote(userName), host.ToString, Quote(password)))

            command.Connection.Open()
            Try
                wasInserted = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return wasInserted

        End Function

        Public Function AddFriend(userId As Integer, friendName As String, friendHost As Integer) As Boolean

            Dim command As DbCommand
            Dim wasAdded As Boolean = False
            Dim friendData As UserData = GetUser(friendName, friendHost)

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "add_friend", _
                                                    userId.ToString, friendData.Id.ToString))

            command.Connection.Open()
            Try
                wasAdded = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return wasAdded

        End Function

        Public Function AddImage(base64string As String, name As String) As Integer

            Dim command As DbCommand
            Dim id As Integer

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "add_image", _
                                                    base64string, name))

            command.Connection.Open()
            Try
                id = CType(command.ExecuteScalar, Integer)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return id

        End Function

        Public Function AddCard(name As String, frontImageId As Integer, backImageId As Integer, _
                                deckId As Integer) As Integer

            Dim command As DbCommand
            Dim id As Integer

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "add_card", _
                                                    name, CStr(frontImageId), CStr(backImageId), CStr(deckId)))

            command.Connection.Open()
            Try
                id = CType(command.ExecuteScalar, Integer)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return id

        End Function

        Public Function AddDeck(name As String, ownerId As Integer, isPublic As Boolean) As Integer

            Dim command As DbCommand
            Dim id As Integer

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "add_deck", _
                                                    name, CStr(ownerId), CStr(CInt(isPublic))))

            command.Connection.Open()
            Try
                id = CType(command.ExecuteScalar, Integer)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return id

        End Function

        Public Function DeckExists(name As String) As Boolean

            Dim command As DbCommand
            Dim exists As Boolean

            command = GetDBCommand(GetCommandString(CommandType.SqlFunction, "deck_exists", name))

            command.Connection.Open()
            Try
                exists = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return exists

        End Function

        Public Function CardExists(name As String, deckId As Integer) As Boolean

            Dim command As DbCommand
            Dim exists As Boolean

            command = GetDBCommand(GetCommandString(CommandType.SqlFunction, "card_exists", name, CStr(deckId)))

            command.Connection.Open()
            Try
                exists = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return exists

        End Function

        Public Function ImageExists(name As String) As Boolean

            Dim command As DbCommand
            Dim exists As Boolean

            command = GetDBCommand(GetCommandString(CommandType.SqlFunction, "image_exists", name))

            command.Connection.Open()
            Try
                exists = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return exists

        End Function

        Public Function GetPlacedDecks(gameId As Integer) As List(Of Deck)

            Dim command As DbCommand
            Dim decks As New List(Of Deck)
            Dim dataReader As DbDataReader

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_placed_decks", CStr(gameId)))

            command.Connection.Open()
            dataReader = command.ExecuteReader()
            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()
                        Dim deck As Deck
                        Dim deckId As Integer = dataReader.GetInt32(2)
                        Dim name As String = dataReader.GetString(4)
                        Dim isLocked As Boolean = dataReader.GetBoolean(5)
                        Dim addsFromTop As Boolean = dataReader.GetBoolean(6)
                        Dim removesFromTop As Boolean = dataReader.GetBoolean(7)
                        Dim isFaceDown As Boolean = dataReader.GetBoolean(8)
                        Dim x As Integer = dataReader.GetInt32(9)
                        Dim y As Integer = dataReader.GetInt32(10)
                        Dim z As Integer = dataReader.GetInt32(11)
                        Dim rotation As Integer = dataReader.GetInt32(12)

                        deck = New Deck(0, deckId, addsFromTop, removesFromTop)
                        deck.SetLock(isLocked)
                        deck.IsFaceDown = isFaceDown
                        deck.SetLocation(New Location(x, y, z, Area.Table))
                        deck.AddDeck(GetDeckCards(deckId))

                        decks.Add(deck)
                    Loop
                End If
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return decks

        End Function

        Public Function GetDeckCards(deckId As Integer) As Integer()

            Dim command As DbCommand
            Dim cards As New List(Of Integer)
            Dim dataReader As DbDataReader

            command = GetDBCommand(GetCommandString(CommandType.SqlProcedure, "get_deck_cards", CStr(deckId)))

            command.Connection.Open()
            dataReader = command.ExecuteReader()
            Try
                If dataReader.HasRows Then
                    Do While dataReader.Read()

                        Dim cardId As Integer = dataReader.GetInt32(0)
                        cards.Add(cardId)

                    Loop
                End If
            Finally
                command.Connection.Close()
                command.Dispose()
            End Try

            Return cards.ToArray()

        End Function

        Private Function GetConnection() As DbConnection

            If _databaseType = DatabaseType.MySQL Then
                Return New MySqlConnection("server=127.0.0.1;" & _
                                           "uid=Cabodiken;" & _
                                           "pwd=cabodiken server;" & _
                                           "database=cabodiken;")
            Else
                Return New SqlConnection("Server=MATHTERPC\SQLEXPRESS;" & _
                                         "Database=Cabodiken;" & _
                                         "Trusted_Connection=Yes;")
            End If


        End Function

        Private Function GetDBCommand(command As String) As Data.Common.DbCommand
            If _databaseType = DatabaseType.MySQL Then
                Return New MySqlCommand(command, CType(GetConnection(), MySqlConnection))
            Else
                Return New SqlCommand(command, CType(GetConnection(), SqlConnection))
            End If

        End Function

        Private Function GetCommandString(commandType As CommandType, storeProcedureName As String, _
                                          ParamArray parameters As String()) As String
            If _databaseType = DatabaseType.MySQL Then
                Return GetMySQLCommandString(commandType, storeProcedureName, parameters)
            Else
                Return GetMSSQLCommandString(commandType, storeProcedureName, parameters)
            End If
        End Function

        Private Function GetMSSQLCommandString(commandType As CommandType, storeProcedureName As String, _
                                               parameters As String()) As String

            Dim commandPrefix As String
            Dim command As String
            Dim lftp As String = "("
            Dim rgtp As String = ")"

            storeProcedureName = "dbo." & storeProcedureName

            If commandType = DataObjects.CommandType.SqlFunction Then
                commandPrefix = "SELECT"
            Else
                commandPrefix = "EXEC"
                lftp = " "
                rgtp = ""
            End If

            command = commandPrefix & " " & storeProcedureName & lftp

            For Each parameter As String In parameters
                command &= parameter & ", "
            Next
            command = command.Substring(0, command.Length - 2) & rgtp

            Return command

        End Function

        Private Function GetMySQLCommandString(commandType As CommandType, storeProcedureName As String, _
                                               parameters As String()) As String
            Dim commandPrefix As String
            Dim command As String

            If commandType = DataObjects.CommandType.SqlFunction Then
                commandPrefix = "CALL"
            Else
                commandPrefix = "SELECT"
            End If

            command = commandPrefix & " " & storeProcedureName & "("

            For Each parameter As String In parameters
                command &= parameter & ", "
            Next

            command = command.Substring(0, command.Length - 2) & ");"

            Return command
        End Function

        Private Function Quote(parameter As String) As String
            Return _quoteChar & parameter & _quoteChar
        End Function

        Private Function GetNullableString(dataReader As DbDataReader, columnId As Integer) As String

            If dataReader.IsDBNull(columnId) Then
                Return ""
            Else
                Return dataReader.GetString(columnId)
            End If

        End Function


#End Region

    End Class

#Region "Enums"

    Friend Enum DatabaseType
        MySQL = 1
        MSSQL = 2
    End Enum

    Friend Enum CommandType
        SqlFunction = 1
        SqlProcedure = 2
    End Enum

#End Region

End Namespace