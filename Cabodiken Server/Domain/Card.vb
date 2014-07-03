Namespace Domain
    Public Class Card
        Inherits GameObject

        Private Const _TYPE As String = "CARD"
        Private _isFaceUp As Boolean

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Function IsFaceUp() As Boolean

        End Function

        Public Sub SetFace(isUp As Boolean)

        End Sub

    End Class
End Namespace