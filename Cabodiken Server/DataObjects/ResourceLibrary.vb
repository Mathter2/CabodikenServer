Namespace DataObjects

    <DataContract()>
    Public Class ResourceLibrary

        Private _boards As List(Of BoardData)
        Private _decks As List(Of DeckData)
        Private _dices As List(Of DiceData)
        Private _tokens As List(Of TokenData)

        <DataMember()>
        Public Property Boards As BoardData()
            Get
                Return _boards.ToArray()
            End Get
            Set(value As BoardData())
                For Each board As BoardData In value
                    _boards.Add(board)
                Next
            End Set
        End Property

        <DataMember()>
        Public Property Decks As DeckData()
            Get
                Return _decks.ToArray()
            End Get
            Set(value As DeckData())
                For Each deck As DeckData In value
                    _decks.Add(deck)
                Next

            End Set
        End Property

        <DataMember()>
        Public Property Dices As DiceData()
            Get
                Return _dices.ToArray()
            End Get
            Set(value As DiceData())
                For Each dice As DiceData In value
                    _dices.Add(dice)
                Next

            End Set
        End Property

        <DataMember()>
        Public Property Tokens As TokenData()
            Get
                Return _tokens.ToArray()
            End Get
            Set(value As TokenData())
                For Each token As TokenData In value
                    _tokens.Add(token)
                Next

            End Set
        End Property

        Public Sub New()
            _boards = New List(Of BoardData)
            _decks = New List(Of DeckData)
            _dices = New List(Of DiceData)
            _tokens = New List(Of TokenData)
        End Sub

        Public Sub AddBoard(board As BoardData)
            _boards.Add(board)
        End Sub

        Public Sub AddDeck(deck As DeckData)
            _decks.Add(deck)
        End Sub

        Public Sub AddDice(dice As DiceData)
            _dices.Add(dice)
        End Sub

        Public Sub AddTokens(token As TokenData)
            _tokens.Add(token)
        End Sub

    End Class
End Namespace