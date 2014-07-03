Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices
    <ServiceContract()>
    Public Interface IWebServiceAuthentication

        <OperationContract()>
        Function UserAuthenticateV01(userName As String, host As Integer, password As String) As String

        <OperationContract()>
        Function GetFriendsListV01(sessionTokenId As String) As UserData()

        <OperationContract()>
        Function ChangeMessageV01(sessionTokenId As String, message As String) As String

        <OperationContract()>
        Function AddFriendV01(sessionTokenId As String, userName As String, host As Integer) As String

        <OperationContract()>
        Function RegisterUserV01(userName As String, host As Integer, password As String) As String

    End Interface
End Namespace