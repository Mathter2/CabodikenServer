Namespace DataObjects

    <DataContract()>
    Public Class ResourceLibrary

        Private _boards As BoardData()
        Private _decks As DeckData()
        Private _dices As DiceData()
        Private _tokens As TokenData()

        <DataMember()>
        Public Property Boards As BoardData()
            Get
                Return _boards
            End Get
            Set(value As BoardData())
                _boards = value
            End Set
        End Property

        <DataMember()>
        Public Property Decks As DeckData()
            Get
                Return _decks
            End Get
            Set(value As DeckData())
                _decks = value
            End Set
        End Property

        <DataMember()>
        Public Property Dices As DiceData()
            Get
                Return _dices
            End Get
            Set(value As DiceData())
                _dices = value
            End Set
        End Property

        <DataMember()>
        Public Property Tokens As TokenData()
            Get
                Return _tokens
            End Get
            Set(value As TokenData())
                _tokens = value
            End Set
        End Property

        Public Sub New(boards As BoardData(), decks As DeckData(), dices As DiceData(), tokens As TokenData())
            _boards = boards
            _decks = decks
            _dices = dices
            _tokens = tokens
        End Sub

    End Class
End Namespace