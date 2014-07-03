Namespace DataObjects
    Public Class InvitationData

        Private _gameName As String
        Private _gameSessionId As String
        Private _sender As UserData

        Public ReadOnly Property GameName As String
            Get
                Return _gameName
            End Get
        End Property

        Public ReadOnly Property GameSessionId As String
            Get
                Return _gameSessionId
            End Get
        End Property

        Public ReadOnly Property Sender As UserData
            Get
                Return _sender
            End Get
        End Property

        Public Sub New(gameName As String, gameSessionId As String, sender As UserData)
            _gameName = gameName
            _gameSessionId = gameSessionId
            _sender = sender
        End Sub

    End Class
End Namespace