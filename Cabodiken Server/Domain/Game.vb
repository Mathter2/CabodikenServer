Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class Game

        Private _actionFactory As ActionFactory
        Private _actions As List(Of ActionData)
        Private _gameSession As GameSessionData
        Private _isGameStarted As Boolean
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

        Public Sub AddUser(user As UserData)

        End Sub

        Public Sub ExecuteAction(owner As UserData, action As String, ParamArray parameters As String())

        End Sub

        Public Function GetActions(lastActionIndex As Integer) As ActionData()

        End Function

        Public Function GetResources() As ResourceLibrary

        End Function

        Public Sub Start()

        End Sub

    End Class
End Namespace