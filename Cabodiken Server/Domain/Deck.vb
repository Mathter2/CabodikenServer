Namespace Domain
    Public Class Deck
        Inherits GameObject

        Private Const _TYPE As String = "DECK"
        Private _addsFromTop As Boolean
        Private _removesFromTop As Boolean
        Private _cards As Integer()
        Private _isFaceUp As Boolean
        Private _layout As Integer

        Public Sub New(id As Integer, resourceId As Integer, addsFromTop As Boolean, removesFromTop As Boolean)
            MyBase.New(id, resourceId)
            _addsFromTop = addsFromTop
            _removesFromTop = removesFromTop
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Sub AddCard(resourceId As Integer)

        End Sub

        Public Sub AddDeck(resourceIds As Integer())

        End Sub

        Public Function Draw() As Integer

        End Function

        Public Function GetCards() As Integer()

        End Function

        Public Function GetLayout() As Integer

        End Function

        Public Sub SetFace(isUp As Boolean)

        End Sub

        Public Sub SetLayout(layout As Integer)

        End Sub

        Public Sub Shuffle(style As Integer)

        End Sub

        Public Function Split(splitStyle As Integer) As Integer()

        End Function

    End Class
End Namespace