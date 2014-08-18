Namespace DomainObjects
    Public Class Glass
        Inherits GameObject

        Private Const _TYPE As String = "GLASS"
        Private _dices As Integer()

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Sub AddDice(resourceId As Integer)

        End Sub

        Public Function GetDices() As Integer()

        End Function

    End Class
End Namespace