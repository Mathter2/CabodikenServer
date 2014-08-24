Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.DomainObjects

Namespace Domain
    Public Class DeckAction
        Implements IAction

        Public Function ExecuteAction(game As Game, owner As DataObjects.PlayerData, objectData As GameObject, action As String, _
                                      parameters() As String) As List(Of ActionData) Implements IAction.ExecuteAction

            Dim deck As Deck = CType(objectData, Deck)

            Select Case action.ToUpper
                Case "MOVE"
                    Return ExecuteMove(owner, deck, parameters)
                Case "AREA_MOVE"
                    Return ExecuteAreaMove(owner, deck, parameters)
                Case "ROTATE"
                    Return ExecuteRotate(owner, deck, parameters)
                Case "LOCK"
                    Return ExecuteLock(owner, deck, parameters)
                Case "FLIP"
                    Return ExecuteFlip(owner, deck, parameters)
                Case "AGGREGATE"
                    Return Nothing 'Placeholder for aggregate function
                Case "SHUFFLE"
                    Return ExecuteShuffle(owner, deck, parameters)
                Case "DRAW"
                    Return ExecuteDraw(game, owner, deck, parameters)
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

        Private Function ExecuteFlip(owner As PlayerData, deck As Deck, parameters As String()) _
                                     As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim isFaceDown As Boolean = CType(parameters(1), Boolean)
            If deck.GetLocation.Area = Area.Table Or owner.IsPlayerArea(deck.GetLocation.Area) Then
                deck.IsFaceDown = isFaceDown
                actionList.Add(New ActionData("FLIP", owner, deckId, deck.IsFaceDown.ToString))
            End If
            Return actionList

        End Function

        Private Function ExecuteDraw(game As Game, owner As PlayerData, deck As Deck, parameters As String()) _
                                     As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim deckId As String = deck.Id.ToString
            Dim x As Integer = CType(parameters(1), Integer)
            Dim y As Integer = CType(parameters(2), Integer)
            Dim targetArea As Area = CType(parameters(3), Area)

            If (deck.GetLocation.Area = Area.Table Or owner.IsPlayerArea(deck.GetLocation.Area)) AndAlso _
                    (targetArea = Area.Table Or owner.IsPlayerArea(targetArea)) Then

                Dim newCardResourceId As Integer = deck.Draw()
                Dim card As Card = New Card(0, newCardResourceId, deck.IsFaceDown)
                If Not targetArea = Area.Table Then
                    card.IsFaceDown = False
                Else
                    card.IsFaceDown = deck.IsFaceDown
                End If
                card.Rotate(deck.GetRotation)
                card.SetLocation(New Location(x, y, 0, targetArea))
                card = CType(game.AddGameObject(card), Card)
                actionList.Add(New ActionData("REORDER", owner, deckId, deck.getCardsString))
                actionList.Add(New ActionData("CREATE_CARD", owner, CStr(card.Id), CStr(card.ResourceId), _
                                              CStr(card.GetRotation), CStr(card.GetLocation.GetCoordinates), _
                                              CStr(card.IsLocked), CStr(card.IsFaceDown)))
            End If

            Return actionList

        End Function
    End Class
End Namespace
