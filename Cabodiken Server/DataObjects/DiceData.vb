Namespace DataObjects

    <DataContract()>
    Public Class DiceData
        Inherits ObjectData

        Private _sides As String()

        <DataMember()>
        Public Property Sides As String()
            Get
                Return _sides
            End Get
            Set(value As String())
                _sides = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, sides As String())
            MyBase.New(id, name, "DICE")
            _sides = sides
        End Sub

    End Class

End Namespace