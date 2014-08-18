Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.DomainObjects

Namespace Domain
    Public Class CustomAction
        Implements IAction

        Public Function ExecuteAction(game As Game, owner As PlayerData, objectData As GameObject, action As String, parameters() As String) As List(Of ActionData) Implements IAction.ExecuteAction

        End Function
    End Class
End Namespace