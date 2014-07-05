Namespace DataObjects

    <DataContract()>
    Public Class DeckData
        Inherits ObjectData

        Private _cards As CardData()

        <DataMember()>
        Public Property Cards As CardData()
            Get
                Return _cards
            End Get
            Set(value As CardData())
                _cards = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, cards As CardData())
            MyBase.New(id, name, "DECK")
            _cards = cards
        End Sub

    End Class

End Namespace