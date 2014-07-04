Namespace DataObjects

    <DataContract()>
    Public Class ImageData

        Private _id As Integer
        Private _base64String As String

        <DataMember()>
        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        <DataMember()>
        Public Property Base64String As String
            Get
                Return _base64String
            End Get
            Set(value As String)
                _base64String = value
            End Set
        End Property

        Public Sub New(id As Integer, base64String As String)
            _id = id
            _base64String = base64String
        End Sub

    End Class
End Namespace