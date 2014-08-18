Namespace DomainObjects
    Public Class Dice
        Inherits GameObject

        Private Const _TYPE As String = "DICE"
        Private _selectedSide As Integer
        Private _sidesNumber As Integer

        Public Sub New(id As Integer, resourceId As Integer)
            MyBase.New(id, resourceId)
        End Sub

        Public Overrides Function GetObjectType() As String
            Return _TYPE
        End Function

        Public Function GetSide() As Integer

        End Function

        Public Sub SetSide(side As Integer)

        End Sub

        Public Function ThrowDice() As Integer

        End Function

    End Class
End Namespace