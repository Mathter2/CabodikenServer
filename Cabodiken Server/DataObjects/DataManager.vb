Imports System.Data.Common
Imports MySql.Data.MySqlClient

Namespace DataObjects
    Public Class DataManager
        Implements IDisposable

#Region "Properties"

        Private Shared _instance As DataManager = New DataManager()
        Private _dbConnection As Data.Common.DbConnection

        Public Shared ReadOnly Property Instance As DataManager
            Get
                Return _instance
            End Get
        End Property
#End Region

#Region "Constructors"

        Private Sub New()
            _dbConnection = New MySqlConnection("server=127.0.0.1;" & _
                                                "uid=Cabodiken;" & _
                                                "pwd=cabodiken server;" & _
                                                "database=cabodiken;")
            _dbConnection.Open()
        End Sub

#End Region

#Region "Methods"

        Public Sub ModifyUserMessage(userId As Integer, message As String)
            Dim command As DbCommand
            command = GetDBCommand(GetCommandString("CALL", "modify_user_message", _
                                                    userId.ToString, Quote(message)))
            command.ExecuteNonQuery()
        End Sub

        Public Function GetFriends(userId As Integer) As List(Of UserData)
            Dim friendsList As New List(Of UserData)
            Dim command As DbCommand = GetDBCommand("CALL get_friends(" & userId & ");")
            Dim dataReader As DbDataReader
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

            command = GetDBCommand(GetCommandString("CALL", storeProcedure, userId.ToString))
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

            command = GetDBCommand(GetCommandString("CALL", storeProcedure, gameId.ToString))
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
                command.Dispose()
            End Try

            Return gameObjects

        End Function

        Public Function GetDeckData(deck As ObjectData) As DeckData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim cards As New List(Of CardData)

            command = GetDBCommand(GetCommandString("CALL", "get_deck_data", deck.Id.ToString))
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
                command.Dispose()
            End Try

            Return New DeckData(deck.Id, deck.Name, cards.ToArray())

        End Function

        Public Function GetBoardData(board As ObjectData) As BoardData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim boardData As BoardData = Nothing

            command = GetDBCommand(GetCommandString("CALL", "get_board_data", board.Id.ToString))
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
                command.Dispose()
            End Try

            Return boardData

        End Function

        Public Function GetDiceData(dice As ObjectData) As DiceData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim sides As New List(Of String)

            command = GetDBCommand(GetCommandString("CALL", "get_dice_data", dice.Id.ToString))
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
                command.Dispose()
            End Try

            Return New DiceData(dice.Id, dice.Name, sides.ToArray())

        End Function

        Public Function GetTokenData(token As ObjectData) As TokenData

            Dim command As DbCommand
            Dim dataReader As DbDataReader
            Dim sides As New List(Of List(Of String))
            Dim sidesMatrix As String()()

            command = GetDBCommand(GetCommandString("CALL", "get_deck_data", token.Id.ToString))
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
            command = GetDBCommand(GetCommandString("CALL", "get_user_by_name", _
                                                    Quote(username), host.ToString))
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
                command.Dispose()
            End Try
            Return user
        End Function

        Public Function ValidateUser(username As String, host As Integer, password As String) As UserData
            Dim command As DbCommand
            Dim isValid As Boolean
            command = GetDBCommand(GetCommandString("SELECT", "validate_user_password", _
                                                    Quote(username), host.ToString, Quote(password)))

            Try
                isValid = CType(command.ExecuteScalar(), Boolean)
            Finally
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

            command = GetDBCommand(GetCommandString("SELECT", "add_user", _
                                                    Quote(userName), host.ToString, Quote(password)))
            Try
                wasInserted = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Dispose()
            End Try

            Return wasInserted

        End Function

        Public Function AddFriend(userId As Integer, friendName As String, friendHost As Integer) As Boolean

            Dim command As DbCommand
            Dim wasAdded As Boolean = False
            Dim friendData As UserData = GetUser(friendName, friendHost)

            command = GetDBCommand(GetCommandString("SELECT", "add_friend", _
                                                    userId.ToString, friendData.Id.ToString))

            Try
                wasAdded = CType(command.ExecuteScalar, Boolean)
            Finally
                command.Dispose()
            End Try

            Return wasAdded

        End Function

        Private Function GetDBCommand(command As String) As Data.Common.DbCommand
            Return New MySqlCommand(command, CType(_dbConnection, MySqlConnection))
        End Function

        Private Function GetCommandString(commandPrefix As String, storeProcedureName As String, _
                                          ParamArray parameters As String()) As String
            Dim command As String = commandPrefix & " " & storeProcedureName & "("
            For Each parameter As String In parameters
                command &= parameter & ", "
            Next
            command = command.Substring(0, command.Length - 2) & ");"
            Return command
        End Function

        Private Function Quote(parameter As String) As String
            Return """" & parameter & """"
        End Function

        Private Function GetNullableString(dataReader As DbDataReader, columnId As Integer) As String

            If dataReader.IsDBNull(columnId) Then
                Return ""
            Else
                Return dataReader.GetString(columnId)
            End If

        End Function


#End Region

#Region "IDisposable Support"
        Private disposedValue As Boolean ' To detect redundant calls

        ' IDisposable
        Protected Overridable Sub Dispose(disposing As Boolean)
            If Not Me.disposedValue Then
                If disposing Then
                    _dbConnection.Close()
                End If

                ' TODO: free unmanaged resources (unmanaged objects) and override Finalize() below.
                ' TODO: set large fields to null.
            End If
            Me.disposedValue = True
        End Sub

        ' TODO: override Finalize() only if Dispose(ByVal disposing As Boolean) above has code to free unmanaged resources.
        'Protected Overrides Sub Finalize()
        '    ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        '    Dispose(False)
        '    MyBase.Finalize()
        'End Sub

        ' This code added by Visual Basic to correctly implement the disposable pattern.
        Public Sub Dispose() Implements IDisposable.Dispose
            ' Do not change this code.  Put cleanup code in Dispose(disposing As Boolean) above.
            Dispose(True)
            GC.SuppressFinalize(Me)
        End Sub
#End Region

    End Class
End Namespace