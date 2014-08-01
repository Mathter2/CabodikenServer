Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.Domain

Namespace WebServices

    Public Class WebServiceLobby
        Implements IWebServiceLobby

        Public Function AddCustomObjectV01(sessionTokenId As String, gameSessionId As String, objectId As Integer, _
                                      objectName As String, objectType As String) As Boolean _
                                  Implements IWebServiceLobby.AddCustomObjectV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)

            If userData Is Nothing Then
                Return False
            Else
                GameManager.Instance.AddCustomObject(userData, gameSessionId, objectId, objectName, objectType)
                Return True
            End If

        End Function

        Public Function BeginGameV01(sessionTokenId As String, gameSessionId As String) As Boolean _
            Implements IWebServiceLobby.BeginGameV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)

            If userData Is Nothing Then
                Return False
            Else
                GameManager.Instance.BeginGame(userData, gameSessionId)
                Return True
            End If

        End Function

        Public Function CreateGameSessionV01(sessionTokenId As String, gameId As Integer, gameName As String) _
            As String Implements IWebServiceLobby.CreateGameSessionV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)

            If userData Is Nothing Then
                Return Nothing
            Else
                Return GameManager.Instance.CreateGameSession(userData, gameId, gameName)
            End If

        End Function

        Public Function GetObjectsV01(sessionTokenId As String, objectType As String) As ObjectData() _
            Implements IWebServiceLobby.GetObjectsV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)
            If userData Is Nothing Then
                Return Nothing
            Else
                Return GameManager.Instance.GetUserCustomObjects(userData, objectType)
            End If

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

        Public Sub SendUserInvitationV01(sessionTokenId As String, gameSessionId As String, friendName As String, _
                                         friendHost As Integer) Implements IWebServiceLobby.SendUserInvitationV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)
            Dim friendData As UserData = UserManager.Instance.GetUser(friendName, friendHost)
            GameManager.Instance.CreateUserInvitation(gameSessionId, userData, friendData)

        End Sub

        Public Function UpdateUserInvitationsV01(sessionTokenId As String) As InvitationData() _
            Implements IWebServiceLobby.UpdateUserInvitationsV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)
            Return GameManager.Instance.GetUserInvitations(userData)

        End Function

        Public Function UpdateGameSessionV01(sessionTokenId As String, gameSessionId As String) As GameSessionData _
            Implements IWebServiceLobby.UpdateGameSessionV01

            If UserManager.Instance.ValidateSessionToken(sessionTokenId) IsNot Nothing Then
                Return GameManager.Instance.GetGameSession(gameSessionId)
            Else
                Return Nothing
            End If

        End Function
    End Class

End Namespace