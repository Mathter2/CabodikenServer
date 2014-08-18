Namespace DomainObjects
    Public Class Box
        Inherits GameObject

        Private Const _TYPE As String = "BOX"
        Private _tokenResourceId As Integer
        Private _quantity As Integer

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Sub AddToken(tokenResourceId As Integer, quantity As Integer)

        End Sub

        Public Function GetQuantity() As Integer

        End Function

        Public Function GetTokenResourceId() As Integer

        End Function

    End Class
End Namespace