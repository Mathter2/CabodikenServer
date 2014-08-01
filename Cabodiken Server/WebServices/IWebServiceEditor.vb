Imports System.ServiceModel
Imports MFierro.Cabodiken.DataObjects

Namespace WebServices

    <ServiceContract()>
    Public Interface IWebServiceEditor

        <OperationContract()>
        Function CreateDeckV01(newDeck As DeckData) As Boolean

    End Interface

End Namespace