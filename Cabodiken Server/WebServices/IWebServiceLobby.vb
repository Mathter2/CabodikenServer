Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices

    <ServiceContract()>
    Public Interface IWebServiceLobby

        <OperationContract()>
        Sub AddCustomObjectV01(sessionTokenId As String, gameSessionId As String, objectId As Integer, objectType As String)

        <OperationContract()>
        Function CreateGameSessionV01(sessionTokenId As String, gameId As Integer, gameName As String) As String

        <OperationContract()>
        Function GetObjectsV01(sessionTokenId As String, objectType As String) As ObjectData()

        <OperationContract()>
        Function JoinGameSessionV01(sessionTokenId As String, gameSessionId As String) As Boolean

        <OperationContract()>
        Sub SendUserInvitationV01(sessionTokenId As String, gameSessionId As String, userId As Integer)

        <OperationContract()>
        Function UpdateGameSessionV01(sessionTokenId As String, gameSessionId As String) As GameSessionData

        <OperationContract()>
        Function UpdateUserInvitationsV01(sessionTokenId As String) As InvitationData()

    End Interface

End Namespace