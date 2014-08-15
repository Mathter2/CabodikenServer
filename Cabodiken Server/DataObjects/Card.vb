Namespace DataObjects
    Public Class Card
        Inherits GameObject

        Private Const _TYPE As String = "CARD"
        Private _isFaceUp As Boolean

        Public Property IsFaceUp() As Boolean
            Get
                Return _isFaceUp
            End Get
            Set(ByVal value As Boolean)
                _isFaceUp = value
            End Set
        End Property


        Public Sub New(id As Integer, resourceId As Integer, isFaceUp As Boolean)
            MyBase.New(id, resourceId)
            _isFaceUp = isFaceUp
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

    End Class
End Namespace