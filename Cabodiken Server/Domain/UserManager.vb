Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class UserManager

#Region "Properties"

        Private Shared _instance As UserManager = New UserManager()
        Private _onlineUsers As String()
        Private _users As Dictionary(Of String, UserData)

        Public Shared ReadOnly Property Instance As UserManager
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

        Public Sub AddFriend(sessionTokenId As String, friendName As String, friendHost As Host)

        End Sub

        Public Sub ChangeUserMessage(sessionTokenId As String, message As String)

        End Sub

        Public Function GetFriendsList(sessionTokenId As String) As UserData()

        End Function

        Public Function GetSessionToken(username As String, host As Host) As String

        End Function

        Public Function GetUser(sessionTokenId As String) As UserData

        End Function

        Public Function LogIn(username As String, password As String) As String

        End Function

        Public Sub RegisterUser(userName As String, Host As Integer, password As String)

        End Sub

        Private Function GenerateSessionTokenId() As String

        End Function

#End Region

    End Class
End Namespace