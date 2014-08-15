Namespace DataObjects
    Public Class Board
        Inherits GameObject

        Private Const _TYPE As String = "BOARD"

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function
    End Class
End Namespace