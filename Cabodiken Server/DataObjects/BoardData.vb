Namespace DataObjects
    <DataContract()>
    Public Class BoardData
        Inherits ObjectData

        Private _image As ImageData

        <DataMember()>
        Public Property Image As ImageData
            Get
                Return _image
            End Get
            Set(value As ImageData)
                _image = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, image As ImageData)
            MyBase.New(id, name, "BOARD")
            _image = image
        End Sub

    End Class

End Namespace