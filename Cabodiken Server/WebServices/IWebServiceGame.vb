Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices
    <ServiceContract()>
    Public Interface IWebServiceGame

        <OperationContract()>
        Sub ExecuteActionV01(sessionTokenId As String, gameSessionId As String, actionName As String, actionParameter As String())

        <OperationContract()>
        Function GetActionsV01(sessionTokenId As String, gameSessionId As String, lastActionIndex As Integer) As ActionData()

        <OperationContract()>
        Function LoadGameResourcesV01(sessionTokenId As String, gameSessionId As String) As ResourceLibrary

    End Interface
End Namespace