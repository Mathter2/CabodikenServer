Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices
    <ServiceContract()>
    Public Interface IWebServiceGame

        <OperationContract()>
        Function ExecuteActionV01(sessionTokenId As String, gameSessionId As String, lastIndex As Integer, _
                                  actionName As String, actionParameters As String()) As Boolean

        <OperationContract()>
        Function GetActionsV01(sessionTokenId As String, gameSessionId As String, _
                               lastActionIndex As Integer) As ActionData()

        <OperationContract()>
        Function LoadGameResourcesV01(sessionTokenId As String, gameSessionId As String) As ResourceLibrary

    End Interface
End Namespace