Namespace DataObjects
    Public Class Card
        Inherits GameObject

        Private Const _TYPE As String = "CARD"
        Private _isFaceDown As Boolean

        Public Property IsFaceDown() As Boolean
            Get
                Return _isFaceDown
            End Get
            Set(ByVal value As Boolean)
                _isFaceDown = value
            End Set
        End Property


        Public Sub New(id As Integer, resourceId As Integer, isFaceDown As Boolean)
            MyBase.New(id, resourceId)
            _isFaceDown = IsFaceDown
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

    End Class
End Namespace