Namespace DataObjects

    <DataContract()>
    Public Class InvitationData

        Private _gameName As String
        Private _gameSessionId As String
        Private _sender As UserData

        <DataMember()>
        Public Property GameName As String
            Get
                Return _gameName
            End Get
            Set(value As String)
                _gameName = value
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
        Public Property Sender As UserData
            Get
                Return _sender
            End Get
            Set(value As UserData)
                _sender = value
            End Set
        End Property

        Public Sub New(gameName As String, gameSessionId As String, sender As UserData)
            _gameName = gameName
            _gameSessionId = gameSessionId
            _sender = sender
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is InvitationData Then
                Dim otherInvitationData As InvitationData = CType(obj, InvitationData)
                Return Me.GameSessionId = otherInvitationData.GameSessionId AndAlso _
                    Me.Sender.Equals(otherInvitationData.Sender)
            Else
                Return False
            End If
        End Function

    End Class
End Namespace