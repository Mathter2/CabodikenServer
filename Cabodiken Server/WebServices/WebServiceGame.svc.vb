Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.Domain

Namespace WebServices
    Public Class WebServiceGame
        Implements IWebServiceGame

        Public Function ExecuteActionV01(sessionTokenId As String, gameSessionId As String, _
                                         actionName As String, actionParameters() As String) As Boolean _
                                         Implements IWebServiceGame.ExecuteActionV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)

            If userData Is Nothing Then
                Return False
            Else
                Return GameManager.Instance.ExecuteAction(userData, gameSessionId, actionName, actionParameters)
            End If

        End Function

        Public Function GetActionsV01(sessionTokenId As String, gameSessionId As String, _
                                      lastActionIndex As Integer) As DataObjects.ActionData() _
                                      Implements IWebServiceGame.GetActionsV01



        End Function

        Public Function LoadGameResourcesV01(sessionTokenId As String, gameSessionId As String) _
            As DataObjects.ResourceLibrary Implements IWebServiceGame.LoadGameResourcesV01

            Dim userData As UserData = UserManager.Instance.ValidateSessionToken(sessionTokenId)

            If userData Is Nothing Then
                Return Nothing
            Else
                Return GameManager.Instance.GetResources(gameSessionId)
            End If

        End Function
    End Class
End Namespace