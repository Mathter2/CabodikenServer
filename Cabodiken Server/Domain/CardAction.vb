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
                    Return ExecuteMove(owner, card, parameters)
                Case "AREA_MOVE"
                    Return ExecuteAreaMove(owner, card, parameters)
                Case "ROTATE"
                    Return ExecuteRotate(owner, card, parameters)
                Case "LOCK"
                    Return ExecuteLock(owner, card, parameters)
                Case "FLIP"
                    Return ExecuteFlip(owner, card, parameters)
                Case "AGGREGATE"
                    Return ExecuteAggregate(game, owner, card, parameters)
                Case Else
                    Return Nothing
            End Select

        End Function

        Private Function ExecuteMove(owner As PlayerData, card As Card, parameters As String()) _
    As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim location As Location = New Location(CInt(parameters(1)), CInt(parameters(2)),
                                                    1, card.GetLocation.Area)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.SetLocation(location)
                actionList.Add(New ActionData("MOVE", owner, cardId, location.GetCoordinates))
            End If

            Return actionList

        End Function

        Private Function ExecuteAreaMove(owner As PlayerData, card As Card, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim moveArea As Area = CType(parameters(3), Area)
            Dim location As Location = New Location(CInt(parameters(1)), CInt(parameters(2)),
                                                    1, moveArea)
            Dim isFaceDown As Boolean = False

            If owner.IsPlayerArea(moveArea) OrElse moveArea = Area.Table OrElse _
                    card.GetLocation.Area = Area.Table OrElse owner.IsPlayerArea(card.GetLocation.Area) Then

                card.SetLocation(location)
                actionList.Add(New ActionData("REMOVE", owner, cardId))
                actionList.Add(New ActionData("CREATE_CARD", owner, cardId, CStr(card.ResourceId), _
                                              CStr(card.GetRotation), CStr(card.GetLocation.GetCoordinates), _
                                              CStr(card.IsLocked), CStr(isFaceDown)))
            End If

            Return actionList

        End Function

        Private Function ExecuteRotate(owner As PlayerData, card As Card, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim rotation As Integer = CType(parameters(1), Integer)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.Rotate(rotation)
                actionList.Add(New ActionData("ROTATE", owner, cardId, rotation.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteLock(owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim isLocked As Boolean = CType(parameters(1), Boolean)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.SetLock(isLocked)
                actionList.Add(New ActionData("LOCK", owner, cardId, isLocked.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteFlip(owner As PlayerData, card As Card, parameters As String()) _
                As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim isFaceDown As Boolean = CType(parameters(1), Boolean)

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.IsFaceDown = isFaceDown
                actionList.Add(New ActionData("FLIP", owner, cardId, card.IsFaceDown.ToString))
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
                actionList.Add(New ActionData("REMOVE", owner, cardId))
                actionList.Add(New ActionData("REORDER", owner, CStr(deck.Id), deck.getCardsString))
            End If

            Return actionList

        End Function

    End Class
End Namespace
