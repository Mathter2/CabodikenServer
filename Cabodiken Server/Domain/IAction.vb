Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Interface IAction

        Function ExecuteAction(action As String, ParamArray parameters As String()) As ActionData()

    End Interface
End Namespace