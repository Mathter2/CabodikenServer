Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class EditorManager

#Region "Properties"

        Private Shared _instance As EditorManager = New EditorManager()

        Public Shared ReadOnly Property Instance As EditorManager
            Get
                Return _instance
            End Get
        End Property

#End Region

#Region "Constructors"

        Private Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Function CreateDeck(deck As DeckData, user As UserData, isPublic As Boolean) As String

            If DataManager.Instance.DeckExists(deck.Name) Then
                Return "The deck name already exists"
            End If

            Dim deckId As Integer = DataManager.Instance.AddDeck(deck.Name, user.Id, isPublic)

            For Each card As CardData In deck.Cards
                Dim errorMessage As String = CreateCard(card, deckId)
                If Not errorMessage = "" Then
                    Return "Could not add card " & card.Name & ": " & errorMessage
                End If
            Next

            Return "Deck added successfully"

        End Function

        Private Function CreateCard(card As CardData, deckId As Integer) As String

            If DataManager.Instance.CardExists(card.Name, deckId) Then
                Return "The card name already exists for the deckId: " & deckId
            End If

            Dim frontImageId As Integer = CreateImage("card_" & deckId & "_" & card.Name & "_front", card.Front)
            If frontImageId = -1 Then
                Return "Could not add front image as there is an image with the same name"
            End If

            Dim backImageId As Integer = CreateImage("card_" & deckId & "_" & card.Name & "_front", card.Back)
            If backImageId = -1 Then
                Return "Could not add back image as there is an image with the same name"
            End If

            DataManager.Instance.AddCard(card.Name, frontImageId, backImageId, deckId)

            Return ""

        End Function

        Private Function CreateImage(name As String, base64string As String) As Integer

            If DataManager.Instance.ImageExists(name) Then
                Return -1
            End If

            Return DataManager.Instance.AddImage(base64string, name)

        End Function

#End Region

    End Class
End Namespace