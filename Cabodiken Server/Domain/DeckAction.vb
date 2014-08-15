Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class DeckAction
        Implements IAction

        Public Function ExecuteAction(owner As DataObjects.PlayerData, objectData As GameObject, action As String, _
                                      parameters() As String) As List(Of ActionData) Implements IAction.ExecuteAction

            Dim deck As Deck = CType(objectData, Deck)

            Select Case action.ToUpper
                Case "MOVE"
                    If parameters.Count = 3 Then
                        Return ExecuteMove(owner, deck, parameters)
                    Else
                        Return ExecuteAreaMove(owner, deck, parameters)
                    End If
                Case "ROTATE"
                    Return ExecuteRotate(owner, deck, parameters)
                Case "LOCK"
                    Return ExecuteLock(owner, deck, parameters)
                Case "FLIP"

                Case "AGGREGATE"

                Case "SHUFFLE"
                    Return ExecuteShuffle(owner, deck, parameters)
                Case Else
                    Return Nothing
            End Select

        End Function

        Private Function ExecuteAreaMove(owner As PlayerData, deck As Deck, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim location As New Location(parameters(1))
            Dim area As Area = CType(parameters(2), Area)

            'If owner.IsPlayerArea(area) OrElse area = DataObjects.Area.Table OrElse _
            '    card.GetLocation.Area = area.Table OrElse owner.IsPlayerArea(card.GetLocation.Area) Then

            'Card.SetLocation(location)
            'actionList.Add(New ActionData("REMOVE", owner, deckId))
            'actionList.Add(New ActionData("CREATE_CARD", owner, Card.Id.ToString, Card.ResourceId.ToString, _
            '                             Card.GetRotation.ToString, Card.GetLocation().GetCoordinates, _
            '                              area.ToString, "False", Card.IsFaceUp.ToString))
            'End If

            Return actionList

        End Function

        Private Function ExecuteMove(owner As PlayerData, deck As Deck, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim location As Location = New Location(CInt(parameters(1)), CInt(parameters(2)),
                                                    1, deck.GetLocation.Area)

            If deck.GetLocation.Area = Area.Table Or owner.IsPlayerArea(deck.GetLocation.Area) Then
                deck.SetLocation(location)
                actionList.Add(New ActionData("MOVE", owner, deckId, location.GetCoordinates))
            End If

            Return actionList

        End Function

        Private Function ExecuteRotate(owner As PlayerData, deck As Deck, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim rotation As Integer = CType(parameters(1), Integer)

            'If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
            'Card.Rotate(rotation)
            'actionList.Add(New ActionData("ROTATE", owner, rotation.ToString))
            'End If

            Return actionList

        End Function

        Private Function ExecuteLock(owner As PlayerData, deck As Deck, parameters As String()) _
                                    As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim isLocked As Boolean = CType(parameters(1), Boolean)

            If deck.GetLocation.Area = Area.Table Or owner.IsPlayerArea(deck.GetLocation.Area) Then
                deck.SetLock(isLocked)
                actionList.Add(New ActionData("LOCK", owner, deckId, isLocked.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteShuffle(owner As PlayerData, deck As Deck, parameters As String()) _
                                        As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            If deck.GetLocation.Area = Area.Table Or owner.IsPlayerArea(deck.GetLocation.Area) Then
                deck.Shuffle(0)
                actionList.Add(New ActionData("REORDER", owner, deckId, deck.getCardsString))
            End If
            Return actionList

        End Function

    End Class
End Namespace
