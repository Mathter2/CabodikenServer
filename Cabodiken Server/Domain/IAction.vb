Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.DomainObjects

Namespace Domain
    Public Interface IAction

        Function ExecuteAction(game As Game, owner As PlayerData, objectData As GameObject, action As String, _
                               parameters As String()) As List(Of ActionData)

    End Interface
End Namespace