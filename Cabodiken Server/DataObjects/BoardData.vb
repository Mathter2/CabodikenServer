Namespace DataObjects
    <DataContract()>
    Public Class BoardData
        Inherits ObjectData

        Private _image As String

        <DataMember()>
        Public Property Image As String
            Get
                Return _image
            End Get
            Set(value As String)
                _image = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, image As String)
            MyBase.New(id, name, "BOARD")
            _image = image
        End Sub

    End Class

End Namespace