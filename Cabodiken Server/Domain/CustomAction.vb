Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class CustomAction
        Implements IAction

        Public Function ExecuteAction(owner As DataObjects.PlayerData, objectData As GameObject, action As String, parameters() As String) As List(Of DataObjects.ActionData) Implements IAction.ExecuteAction

        End Function
    End Class
End Namespace