Namespace DataObjects

    <DataContract()>
    Public Class GameSessionData

        Private _game As ObjectData
        Private _gameSessionId As String
        Private _isGameStarted As Boolean
        Private _players As Dictionary(Of UserData, PlayerData)

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
        Public Property IsGameStarted As Boolean
            Get
                Return _isGameStarted
            End Get
            Set(value As Boolean)
                _isGameStarted = value
            End Set
        End Property

        <DataMember()>
        Public Property Players As PlayerData()
            Get
                Return _players.Values.ToArray
            End Get
            Set(value As PlayerData())
                For Each player As PlayerData In value
                    AddPlayer(player)
                Next
            End Set
        End Property

        Public Sub New(game As ObjectData, gameSessionId As String, owner As PlayerData, _
                       ParamArray players As PlayerData())
            _game = game
            _gameSessionId = gameSessionId
            _isGameStarted = False
            _players = New Dictionary(Of UserData, PlayerData)
            AddPlayer(owner)
            For Each player As PlayerData In players
                AddPlayer(player)
            Next
        End Sub

        Public Sub AddPlayer(player As PlayerData)

            Dim userData As New  _
                        UserData(player.Id, player.Name, player.Host, player.Message, player.IsOnline)
            _players.Add(userData, player)

        End Sub

        Public Function GetPlayer(user As UserData) As PlayerData

            Return _players(user)

        End Function

    End Class
End Namespace