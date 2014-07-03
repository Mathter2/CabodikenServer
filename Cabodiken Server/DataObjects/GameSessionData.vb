Namespace DataObjects
    Public Class GameSessionData

        Private _game As ObjectData
        Private _gameSessionId As String
        Private _owner As PlayerData
        Private _players As PlayerData()

        Public ReadOnly Property Game As ObjectData
            Get
                Return _game
            End Get
        End Property

        Public ReadOnly Property GameSessionId As String
            Get
                Return _gameSessionId
            End Get
        End Property

        Public ReadOnly Property Owner As PlayerData
            Get
                Return _owner
            End Get
        End Property

        Public ReadOnly Property Players As PlayerData()
            Get
                Return _players
            End Get
        End Property

        Public Sub New(game As ObjectData, gameSessionId As String, owner As PlayerData, players As PlayerData())
            _game = game
            _gameSessionId = gameSessionId
            _owner = owner
            _players = players
        End Sub

    End Class
End Namespace