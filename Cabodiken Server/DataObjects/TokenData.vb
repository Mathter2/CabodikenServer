Namespace DataObjects

    Public Class TokenData
        Inherits ObjectData

        Private _sides As ImageData()()

        Public ReadOnly Property Sides As ImageData()()
            Get
                Return _sides
            End Get
        End Property

        Public Sub New(id As Integer, name As String, sides As ImageData()())
            MyBase.New(id, name)
            _sides = sides
        End Sub

    End Class

End Namespace