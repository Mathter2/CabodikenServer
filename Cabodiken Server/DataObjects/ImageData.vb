Namespace DataObjects
    Public Class ImageData

        Private _id As Integer
        Private _base64String As String

        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property Base64String As String
            Get
                Return _base64String
            End Get
        End Property

        Public Sub New(id As Integer, base64String As String)
            _id = id
            _base64String = base64String
        End Sub

    End Class
End Namespace