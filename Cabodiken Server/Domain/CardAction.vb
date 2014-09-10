Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.DomainObjects

Namespace Domain
    Public Class CardAction
        Implements IAction

        Public Function ExecuteAction(game As Game, owner As DataObjects.PlayerData, objectData As GameObject, action As String, _
                                      parameters() As String) As List(Of ActionData) Implements IAction.ExecuteAction

            Dim card As Card = CType(objectData, Card)

            Select Case action.ToUpper
                Case "MOVE"
                    Return ExecuteMove(game, owner, card, parameters)
                Case "AREA_MOVE"
                    Return ExecuteAreaMove(game, owner, card, parameters)
                Case "ROTATE"
                    Return ExecuteRotate(game, owner, card, parameters)
                Case "LOCK"
                    Return ExecuteLock(game, owner, card, parameters)
                Case "FLIP"
                    Return ExecuteFlip(game, owner, card, parameters)
                Case "AGGREGATE"
                    Return ExecuteAggregate(game, owner, card, parameters)
                Case Else
                    Return Nothing
            End Select

        End Function

        Private Function ExecuteMove(game As Game, owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim location As Location = New Location(CInt(parameters(1)), CInt(parameters(2)),
                                                    1, card.GetLocation.Area)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.SetLocation(location)
                actionList.Add(New ActionData(game.NextActionId, "MOVE", owner, cardId, location.GetCoordinates))
            End If

            Return actionList

        End Function

        Private Function ExecuteAreaMove(game As Game, owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim targetArea As Area = CType(parameters(3), Area)
            Dim location As Location = New Location(CInt(parameters(1)), CInt(parameters(2)),
                                                    1, targetArea)
            Dim isFaceDown As Boolean = False

            If (owner.IsPlayerArea(targetArea) OrElse targetArea = Area.Table) AndAlso _
                    (card.GetLocation.Area = Area.Table OrElse owner.IsPlayerArea(card.GetLocation.Area)) Then

                card.SetLocation(location)
                actionList.Add(New ActionData(game.NextActionId, "REMOVE", owner, cardId))
                actionList.Add(New ActionData(game.NextActionId + 1, "CREATE_CARD", owner, cardId, _
                                              CStr(card.ResourceId), CStr(card.GetRotation), _
                                              CStr(card.GetLocation.GetCoordinates), _
                                              CStr(card.IsLocked), CStr(isFaceDown)))
            End If

            Return actionList

        End Function

        Private Function ExecuteRotate(game As Game, owner As PlayerData, card As Card, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim rotation As Integer = CType(parameters(1), Integer)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.Rotate(rotation)
                actionList.Add(New ActionData(game.NextActionId, "ROTATE", owner, cardId, rotation.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteLock(game As Game, owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim isLocked As Boolean = CType(parameters(1), Boolean)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.SetLock(isLocked)
                actionList.Add(New ActionData(game.NextActionId, "LOCK", owner, cardId, isLocked.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteFlip(game As Game, owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim isFaceDown As Boolean = CType(parameters(1), Boolean)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.IsFaceDown = isFaceDown
                actionList.Add(New ActionData(game.NextActionId, "FLIP", owner, cardId, card.IsFaceDown.ToString))
            End If

            Return actionList
        End Function

        Private Function ExecuteAggregate(game As Game, owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim deckId As Integer = CInt(parameters(1))
            Dim deck As Deck = CType(game.GetGameObject(deckId), Deck)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                deck.AddCard(card.ResourceId)
                game.RemoveGameObject(card.Id)
                actionList.Add(New ActionData(game.NextActionId, "REMOVE", owner, cardId))
                actionList.Add(New ActionData(game.NextActionId + 1, "REORDER", owner, CStr(deck.Id), _
                                              deck.getCardsString))
            End If

            Return actionList

        End Function

    End Class
End Namespace
