Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices

    <ServiceContract()>
    Public Interface IWebServiceLobby

        <OperationContract()>
        Function AddCustomObjectV01(sessionTokenId As String, gameSessionId As String, objectId As Integer, _
                               objectName As String, objectType As String) As Boolean

        <OperationContract()>
        Function CreateGameSessionV01(sessionTokenId As String, gameId As Integer, gameName As String) As String

        <OperationContract()>
        Function GetObjectsV01(sessionTokenId As String, objectType As String) As ObjectData()

        <OperationContract()>
        Function JoinGameSessionV01(sessionTokenId As String, gameSessionId As String) As Boolean

        <OperationContract()>
        Sub SendUserInvitationV01(sessionTokenId As String, gameSessionId As String, friendName As String, friendHost As Integer)

        <OperationContract()>
        Function UpdateGameSessionV01(sessionTokenId As String, gameSessionId As String) As GameSessionData

        <OperationContract()>
        Function UpdateUserInvitationsV01(sessionTokenId As String) As InvitationData()

    End Interface

End Namespace