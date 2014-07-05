Namespace DataObjects

    <DataContract()>
    Public Class DiceData
        Inherits ObjectData

        Private _sides As ImageData()

        <DataMember()>
        Public Property Sides As ImageData()
            Get
                Return _sides
            End Get
            Set(value As ImageData())
                _sides = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, sides As ImageData())
            MyBase.New(id, name, "DICE")
            _sides = sides
        End Sub

    End Class

End Namespace