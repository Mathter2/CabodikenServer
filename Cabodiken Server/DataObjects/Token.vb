Namespace DataObjects
    Public Class Token
        Inherits GameObject

        Private Const _TYPE As String = "TOKEN"
        Private _sideX As Integer
        Private _sideY As Integer

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Function GetSideX() As Integer

        End Function

        Public Function GetSideY() As Integer

        End Function

        Public Sub SetSides(x As Integer, y As Integer)

        End Sub

    End Class
End Namespace