Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.Domain

Namespace WebServices

    Public Class WebServiceAuthentication
        Implements IWebServiceAuthentication

        Public Function AddFriendV01(sessionTokenId As String, userName As String, host As Integer) As String _
            Implements IWebServiceAuthentication.AddFriendV01

        End Function

        Public Function ChangeMessageV01(sessionTokenId As String, message As String) As String _
            Implements IWebServiceAuthentication.ChangeMessageV01

        End Function

        Public Function GetFriendsListV01(sessionTokenId As String) As DataObjects.UserData() _
            Implements IWebServiceAuthentication.GetFriendsListV01

        End Function

        Public Function UserAuthenticateV01(userName As String, host As Integer, password As String) As String _
            Implements IWebServiceAuthentication.UserAuthenticateV01

        End Function

        Public Function RegisterUserV01(userName As String, host As Integer, password As String) As String _
            Implements IWebServiceAuthentication.RegisterUserV01

            UserManager.Instance.

        End Function
    End Class

End Namespace