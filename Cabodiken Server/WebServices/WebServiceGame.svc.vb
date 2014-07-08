Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.Domain

Namespace WebServices
    Public Class WebServiceGame
        Implements IWebServiceGame

        Public Sub ExecuteActionV01(sessionTokenId As String, gameSessionId As String, actionName As String, _
                                    actionParameter() As String) Implements IWebServiceGame.ExecuteActionV01

        End Sub

        Public Function GetActionsV01(sessionTokenId As String, gameSessionId As String, _
                                      lastActionIndex As Integer) As DataObjects.ActionData() Implements IWebServiceGame.GetActionsV01

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