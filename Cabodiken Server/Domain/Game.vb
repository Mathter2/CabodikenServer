Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class Game

        Private _actionFactory As ActionFactory
        Private _actions As List(Of ActionData)
        Private _gameSession As GameSessionData
        Private _lastObjectId As Integer
        Private _placedObjects As Dictionary(Of Integer, GameObject)

        Public ReadOnly Property GameSession As GameSessionData
            Get
                Return _gameSession
            End Get
        End Property

        Public Sub New(gameId As Integer, gameName As String, gameSessionId As String, owner As PlayerData)
            Dim gameData As New ObjectData(gameId, gameName, "GAME")
            _gameSession = New GameSessionData(gameData, gameSessionId, owner)
            _actionFactory = New ActionFactory
            _actions = New List(Of ActionData)
            _lastObjectId = 0
            _placedObjects = New Dictionary(Of Integer, GameObject)
        End Sub

        Public Sub AddCustomObject(customObject As ObjectData, objectType As String)

        End Sub

        Public Function AddUser(user As UserData) As Boolean

            Dim playerPosition As Integer
            Dim playerNumber As Integer = _gameSession.Players.Count + 2
            If playerNumber > 8 Then
                Return False
            Else
                Select Case playerNumber
                    Case 2, 6
                        playerPosition = 3
                    Case 3, 7
                        playerPosition = 1
                    Case 4, 8
                        playerPosition = 2
                    Case 5
                        playerPosition = 0
                End Select
                _gameSession.AddPlayer(New PlayerData(user, playerPosition, playerNumber))
                Return True
            End If
            
        End Function

        Public Function ExecuteAction(owner As UserData, actionName As String, knownIndex As Integer, _
                                      parameters As String()) As Boolean

            Dim action As IAction
            Dim player As PlayerData = _gameSession.GetPlayer(owner)
            Dim gameObject As GameObject
            Dim gameObjectId As Integer
            Dim actions As List(Of ActionData)

            If parameters.Length < 1 AndAlso Not IsNumeric(parameters(0)) Then
                Return False
            Else
                gameObjectId = CType(parameters(0), Integer)
            End If

            If _placedObjects.ContainsKey(gameObjectId) Then
                gameObject = _placedObjects(gameObjectId)
            Else
                Return False
            End If

            If Not gameObject.validateAction(knownIndex) Then
                Return False
            End If

            action = _actionFactory.GetAction(gameObject.GetObjectType())

            actions = action.ExecuteAction(player, gameObject, actionName, parameters)

            If actions.Count = 0 Then
                Return False
            Else
                _actions.AddRange(actions)
                Return True
            End If

        End Function

        Public Function GetActions(lastActionIndex As Integer) As List(Of ActionData)

            Dim actions As New List(Of ActionData)

            For index As Integer = lastActionIndex To _actions.Count - 1

                actions.Add(_actions.Item(index))

            Next

            Return actions

        End Function

        Public Function GetResources() As ResourceLibrary

            Dim decks As List(Of ObjectData) = GetSessionObjects("DECK", _gameSession)
            Dim dices As List(Of ObjectData) = GetSessionObjects("DICE", _gameSession)
            Dim tokens As List(Of ObjectData) = GetSessionObjects("TOKEN", _gameSession)
            Dim boards As List(Of ObjectData) = GetSessionObjects("BOARD", _gameSession)
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

        Public Sub Start()

            _gameSession.IsGameStarted = True

        End Sub

        Private Function GetSessionObjects(objectType As String, session As GameSessionData) As List(Of ObjectData)

            Dim sessionObjects As New List(Of ObjectData)
            Dim players As New List(Of PlayerData)

            sessionObjects.AddRange(DataManager.Instance.GetGameObjects(session.Game.Id, objectType))
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

    End Class
End Namespace