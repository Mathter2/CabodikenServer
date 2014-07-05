Namespace DataObjects

    <DataContract()>
    Public Class GameSessionData

        Private _game As ObjectData
        Private _gameSessionId As String
        Private _owner As PlayerData
        Private _players As PlayerData()

        <DataMember()>
        Public Property Game As ObjectData
            Get
                Return _game
            End Get
            Set(value As ObjectData)
                _game = value
            End Set
        End Property

        <DataMember()>
        Public Property GameSessionId As String
            Get
                Return _gameSessionId
            End Get
            Set(value As String)
                _gameSessionId = value
            End Set
        End Property

        <DataMember()>
        Public Property Owner As PlayerData
            Get
                Return _owner
            End Get
            Set(value As PlayerData)
                _owner = value
            End Set
        End Property

        <DataMember()>
        Public Property Players As PlayerData()
            Get
                Return _players
            End Get
            Set(value As PlayerData())
                _players = value
            End Set
        End Property

        Public Sub New(game As ObjectData, gameSessionId As String, owner As PlayerData, players As PlayerData())
            _game = game
            _gameSessionId = gameSessionId
            _owner = owner
            _players = players
        End Sub

    End Class
End Namespace