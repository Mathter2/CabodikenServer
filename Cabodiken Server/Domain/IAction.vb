Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Interface IAction

        Function ExecuteAction(owner As PlayerData, objectData As GameObject, action As String, _
                               parameters As String()) As List(Of ActionData)

    End Interface
End Namespace