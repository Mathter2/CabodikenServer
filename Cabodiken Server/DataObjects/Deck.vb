Namespace DataObjects
    Public Class Deck
        Inherits GameObject

        Private Const _TYPE As String = "DECK"
        Private _addsFromTop As Boolean
        Private _removesFromTop As Boolean
        Private _cards As New List(Of Integer)
        Private _isFaceDown As Boolean
        Private _layout As Integer

        Public Property IsFaceDown() As Boolean
            Get
                Return _isFaceDown
            End Get
            Set(ByVal value As Boolean)
                _isFaceDown = value
            End Set
        End Property

        Public Property Layout() As Integer
            Get
                Return _layout
            End Get
            Set(ByVal value As Integer)
                _layout = value
            End Set
        End Property


        Public Sub New(id As Integer, resourceId As Integer, addsFromTop As Boolean, removesFromTop As Boolean)
            MyBase.New(id, resourceId)
            _addsFromTop = addsFromTop
            _removesFromTop = removesFromTop
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Function getCardsString() As String

            Dim cardStrings As New List(Of String)
            For Each card As Integer In _cards
                cardStrings.Add(card.ToString)
            Next
            Return Join(cardStrings.ToArray(), ",")

        End Function

        Public Sub AddCard(resourceId As Integer)

            _cards.Add(resourceId)

        End Sub

        Public Sub AddDeck(resourceIds As Integer())

            _cards.AddRange(resourceIds)

        End Sub

        Public Function Draw() As Integer

        End Function

        Public Function GetCards() As Integer()

            Return _cards.ToArray

        End Function

        Public Function GetLayout() As Integer

        End Function

        Public Sub SetLayout(layout As Integer)

        End Sub

        Public Function Split(splitStyle As Integer) As Integer()

        End Function

        Public Sub Shuffle(style As Integer)

            Select Case style
                Case 0
                    ShuffleTotal()
                Case Else
                    ShuffleTotal()
            End Select


        End Sub

        Private Sub ShuffleTotal()

            Dim shuffled As New List(Of Integer)
            For index As Integer = 0 To _cards.Count - 1
                Dim random As Integer = GenerateRandomNumber(0, _cards.Count - 1)
                shuffled.Add(_cards(random))
                _cards.RemoveAt(random)
            Next
            _cards = shuffled

        End Sub

        Private Function GenerateRandomNumber(min As Integer, max As Integer) As Integer
            Static staticRandomGenerator As New System.Random
            Return staticRandomGenerator.Next(min, max + 1)
        End Function

    End Class
End Namespace