Namespace DataObjects

    <DataContract()>
    Public Class ResourceLibrary

        Private _boards As BoardData()
        Private _decks As DeckData()
        Private _dices As DiceData()
        Private _tokens As TokenData()

        <DataMember()>
        Public ReadOnly Property Boards As BoardData()
            Get
                Return _boards
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Decks As DeckData()
            Get
                Return _decks
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Dices As DiceData()
            Get
                Return _dices
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Tokens As TokenData()
            Get
                Return _tokens
            End Get
        End Property

        Public Sub New(boards As BoardData(), decks As DeckData(), dices As DiceData(), tokens As TokenData())
            _boards = boards
            _decks = decks
            _dices = dices
            _tokens = tokens
        End Sub

    End Class
End Namespace