Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class GameManager

#Region "Properties"

        Private Shared _instance As GameManager = New GameManager()
        Private _games As Dictionary(Of String, Game)
        Private _invitations As Dictionary(Of UserData, List(Of InvitationData))
        Private _allowedChars As Char()
        Private _random As Random

        Public Shared ReadOnly Property Instance As GameManager
            Get
                Return _instance
            End Get
        End Property

#End Region

#Region "Constructors"

        Private Sub New()
            _games = New Dictionary(Of String, Game)
            _invitations = New Dictionary(Of UserData, List(Of InvitationData))
            _allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
            _random = New Random
        End Sub

#End Region

#Region "Methods"

        Public Sub AddCustomObject(user As UserData, gameSessionId As String, objectId As Integer, _
                                   objectName As String, objectType As String)

            Dim player As PlayerData = _games(gameSessionId).GameSession.GetPlayer(user)

            player.AddCustomObject(objectId, objectName, objectType)

        End Sub

        Public Function BeginGame(user As UserData, gameSessionId As String) As Boolean

            Dim game As Game = _games(gameSessionId)

            If game.GameSession.Owner.Equals(user) Then
                game.Start()
                Return True
            Else
                Return False
            End If

        End Function

        Public Function CreateGameSession(ownerData As UserData, gameId As Integer, _
                                          gameName As String) As String
            Dim newGame As Game
            Dim newGameSessionId As String = GenerateGameSessionId(gameName)
            Dim owner As PlayerData = New PlayerData(ownerData, 0)

            newGame = New Game(gameId, gameName, newGameSessionId, owner)
            _games.Add(newGameSessionId, newGame)

            Return newGameSessionId

        End Function

        Public Sub CreateUserInvitation(gameSessionId As String, user As UserData, friendData As UserData)

            Dim gameName As String = _games(gameSessionId).GameSession.Game.Name
            Dim invitation As New InvitationData(gameName, gameSessionId, user)
            Dim friendInvitations As List(Of InvitationData)

            If _invitations.ContainsKey(friendData) Then
                friendInvitations = _invitations(friendData)
                If Not friendInvitations.Contains(invitation) Then
                    friendInvitations.Add(invitation)
                End If
            Else
                friendInvitations = New List(Of InvitationData)
                friendInvitations.Add(invitation)
                _invitations.Add(friendData, friendInvitations)
            End If

        End Sub

        Public Sub ExecuteAction(sessionTokenId As String, gameSessionId As String, action As String, parameters As String())

        End Sub

        Public Function GetActions(sessionTokenId As String, gameSessionId As String, lastActionIndex As Integer) As ActionData()

        End Function

        Public Function GetGameSession(gameSessionId As String) As GameSessionData

            Return _games(gameSessionId).GameSession

        End Function

        Public Function GetResources(gameSessionId As String) As ResourceLibrary

            Dim gameSession As Game = _games(gameSessionId)
            Dim decks As List(Of ObjectData) = GetSessionObjects("DECK", gameSession.GameSession)
            Dim dices As List(Of ObjectData) = GetSessionObjects("DICE", gameSession.GameSession)
            Dim tokens As List(Of ObjectData) = GetSessionObjects("TOKEN", gameSession.GameSession)
            Dim boards As List(Of ObjectData) = GetSessionObjects("BOARD", gameSession.GameSession)
            Dim resources As New ResourceLibrary()

            For Each deck As ObjectData In decks
                resources.AddDeck(DataManager.Instance.GetDeckData(deck))
            Next

            For Each dice As ObjectData In dices
                resources.AddDice(DataManager.Instance.GetDiceData(dice))
            Next

            For Each token As ObjectData In tokens
                resources.AddTokens(DataManager.Instance.GetTokenData(token))
            Next

            For Each board As ObjectData In boards
                resources.AddBoard(DataManager.Instance.GetBoardData(board))
            Next

            Return resources

        End Function

        Public Function GetUserCustomObjects(user As UserData, objectType As String) As ObjectData()

            Return DataManager.Instance.GetUserObjects(user.Id, objectType).ToArray()

        End Function

        Public Function GetUserInvitations(user As UserData) As InvitationData()

            If _invitations.ContainsKey(user) Then
                Return _invitations(user).ToArray
            Else
                Return Nothing
            End If

        End Function

        Public Function JoinGameSession(user As UserData, gameSessionId As String) As Boolean

            Dim game As Game = _games(gameSessionId)
            Return game.AddUser(user)

        End Function

        Private Function GetSessionObjects(objectType As String, session As GameSessionData) As List(Of ObjectData)

            Dim sessionObjects As New List(Of ObjectData)
            Dim players As New List(Of PlayerData)

            sessionObjects.AddRange(DataManager.Instance.GetGameObjects(session.Game.Id, objectType))
            players.Add(session.Owner)
            players.AddRange(session.Players)

            Select Case objectType
                Case "DECK"
                    For Each player As PlayerData In players
                        sessionObjects.AddRange(player.CustomDecks)
                    Next
                Case "DICE"
                    For Each player As PlayerData In players
                        sessionObjects.AddRange(player.CustomDices)
                    Next
                Case "TOKEN"
                    For Each player As PlayerData In players
                        sessionObjects.AddRange(player.CustomTokens)
                    Next
            End Select

            Return sessionObjects

        End Function

        Private Function GenerateGameSessionId(gameName As String) As String
            Dim gameSessionId As String = ""
            Dim ticks As String = Date.Now.Ticks.ToString
            Dim random As New Random()

            If gameName.Length < 6 Then
                For i As Integer = gameName.Length To 6
                    gameName += GetRandomAlphaNumber()
                Next
            End If

            If ticks.Length < 7 Then
                For i As Integer = ticks.Length To 7
                    ticks += "0"
                Next
            End If

            For i As Integer = 1 To 64
                Select Case i
                    Case 4
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 1).ToString
                    Case 14
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 2).ToString
                    Case 24
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 3).ToString
                    Case 34
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 4).ToString
                    Case 44
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 5).ToString
                    Case 54
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 6).ToString
                    Case 64
                        gameSessionId &= ticks.ToCharArray.GetValue(ticks.Length - 7).ToString
                    Case 10
                        gameSessionId &= gameName.ToCharArray.GetValue(3).ToString
                    Case 20
                        gameSessionId &= gameName.ToCharArray.GetValue(5).ToString
                    Case 30
                        gameSessionId &= gameName.ToCharArray.GetValue(4).ToString
                    Case 40
                        gameSessionId &= gameName.ToCharArray.GetValue(0).ToString
                    Case 50
                        gameSessionId &= gameName.ToCharArray.GetValue(2).ToString
                    Case 60
                        gameSessionId &= gameName.ToCharArray.GetValue(1).ToString
                    Case Else
                        gameSessionId &= GetRandomAlphaNumber()
                End Select
            Next

            If _games.ContainsKey(gameSessionId) Then
                gameSessionId = GenerateGameSessionId(gameName)
            End If

            Return gameSessionId

        End Function

        Private Function GetRandomAlphaNumber() As String
            Return _allowedChars(_random.Next(_allowedChars.Length - 1))
        End Function

#End Region

    End Class
End Namespace