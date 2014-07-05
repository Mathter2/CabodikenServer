Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.Domain

Namespace WebServices

    Public Class WebServiceLobby
        Implements IWebServiceLobby

        Public Sub AddCustomObjectV01(sessionTokenId As String, gameSessionId As String, objectId As Integer, objectType As String) Implements IWebServiceLobby.AddCustomObjectV01

        End Sub

        Public Function CreateGameSessionV01(sessionTokenId As String, gameId As Integer, gameName As String) _
            As String Implements IWebServiceLobby.CreateGameSessionV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)
            If userData Is Nothing Then
                Return Nothing
            Else
                Return GameManager.Instance.CreateGameSession(userData, gameId, gameName)
            End If

        End Function

        Public Function GetObjectsV01(sessionTokenId As String, objectType As String) As DataObjects.ObjectData() Implements IWebServiceLobby.GetObjectsV01

        End Function

        Public Function JoinGameSessionV01(sessionTokenId As String, gameSessionId As String) As Boolean _
            Implements IWebServiceLobby.JoinGameSessionV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)
            If userData Is Nothing Then
                Return Nothing
            Else
                Return GameManager.Instance.JoinGameSession(userData, gameSessionId)
            End If

        End Function

        Public Sub SendUserInvitationV01(sessionTokenId As String, gameSessionId As String, userId As Integer) Implements IWebServiceLobby.SendUserInvitationV01

        End Sub

        Public Function UpdateUserInvitationsV01(sessionTokenId As String) As DataObjects.InvitationData() Implements IWebServiceLobby.UpdateUserInvitationsV01

        End Function

        Public Function UpdateGameSessionV01(sessionTokenId As String, gameSessionId As String) As DataObjects.GameSessionData Implements IWebServiceLobby.UpdateGameSessionV01

        End Function
    End Class

End Namespace