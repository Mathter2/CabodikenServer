Namespace DataObjects
    Public Class DataManager

#Region "Properties"

        Private _instance As DataManager = New DataManager()

        Public ReadOnly Property Instance As DataManager
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

        Public Sub ChangeUserMessage(userId As Integer, message As String)

        End Sub

        Public Function GetFriends(userId As Integer) As UserData()

        End Function

        Public Function GetObject(objectId As Integer, objectType As String) As ObjectData()

        End Function

        Public Function GetUser(username As String, Host As String) As UserData

        End Function

#End Region

    End Class
End Namespace