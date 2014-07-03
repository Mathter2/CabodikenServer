Namespace DataObjects

    Public Class BoardData
        Inherits ObjectData

        Private _image As ImageData

        Public ReadOnly Property Image As ImageData
            Get
                Return _image
            End Get
        End Property

        Public Sub New(id As Integer, name As String, image As ImageData)
            MyBase.New(id, name)
            _image = image
        End Sub

    End Class

End Namespace