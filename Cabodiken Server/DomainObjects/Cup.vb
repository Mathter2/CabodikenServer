Namespace DomainObjects
    Public Class Cup
        Inherits GameObject

        Private Const _TYPE As String = "CUP"
        Private _tokens As Integer()

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Sub AddToken(resourceId As Integer)

        End Sub

        Public Function GetToken() As Integer

        End Function

        Private Sub Shuffle()

        End Sub

    End Class
End Namespace