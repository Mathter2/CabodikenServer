Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class GameManager

#Region "Properties"

        Private Shared _instance As GameManager = New GameManager()
        Private _games As Dictionary(Of String, Game)
        Private _invitations As Dictionary(Of String, InvitationData)

        Public Shared ReadOnly Property Instance As GameManager
            Get
                Return _instance
            End Get
        End Property

#End Region

#Region "Constructors"

        Private Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Sub AddCustomObject(sessionTokenId As String, gameSessionId As String, objectId As Integer, objectType As String)

        End Sub

        Public Function CreateGameSession(sessionTokenId As String, gameId As Integer) As String

        End Function

        Public Sub CreateUserInvitation(sessionTokenId As String, gameSessionId As String, username As String, host As Host)

        End Sub

        Public Sub ExecuteAction(sessionTokenId As String, gameSessionId As String, action As String, parameters As String())

        End Sub

        Public Function GetActions(sessionTokenId As String, gameSessionId As String, lastActionIndex As Integer) As ActionData()

        End Function

        Public Function GetGameSession(sessionTokenId As String, gameSessionId As String) As GameSessionData

        End Function

        Public Function GetResources(sessionTokenId As String, gameSessionId As String) As ResourceLibrary

        End Function

        Public Function GetUserInvitations(sessionTokenId As String) As InvitationData()

        End Function

        Public Function JoinGameSession(sessionTokenId As String, gameSessionId As String) As String

        End Function

        Private Function GenerateGameSessionId() As String

        End Function

#End Region

    End Class
End Namespace