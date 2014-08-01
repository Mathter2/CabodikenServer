Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class CardAction
        Implements IAction

        Public Function ExecuteAction(owner As DataObjects.PlayerData, objectData As GameObject, action As String, _
                                      parameters() As String) As List(Of ActionData) Implements IAction.ExecuteAction

            Dim card As Card = CType(objectData, Card)

            Select Case action.ToUpper
                Case "MOVE"
                    If parameters.Count = 2 Then
                        Return ExecuteMove(owner, card, parameters)
                    Else
                        Return ExecuteAreaMove(owner, card, parameters)
                    End If
                Case "ROTATE"
                    Return ExecuteRotate(owner, card, parameters)
                Case "LOCK"
                    Return ExecuteLock(owner, card, parameters)
                Case "FLIP"

                Case "AGGREGATE"

                Case Else
                    Return Nothing
            End Select

        End Function

        Private Function ExecuteAreaMove(owner As PlayerData, card As Card, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim location As New Location(parameters(1))
            Dim area As Area = CType(parameters(2), Area)

            If owner.IsPlayerArea(area) OrElse area = DataObjects.Area.Table OrElse _
                card.GetLocation.Area = area.Table OrElse owner.IsPlayerArea(card.GetLocation.Area) Then

                card.SetLocation(location)
                actionList.Add(New ActionData("REMOVE", owner, cardId))
                actionList.Add(New ActionData("CREATE_CARD", owner, card.Id.ToString, card.ResourceId.ToString, _
                                              card.GerRotation.ToString, card.GetLocation().GetCoordinates, _
                                              area.ToString, "False", card.IsFaceUp.ToString))
            End If

            Return actionList

        End Function

        Private Function ExecuteMove(owner As PlayerData, card As Card, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim cardId As String = card.Id.ToString
            Dim location As New Location(parameters(1))

            If card.GetLocation.Area = Area.Table Or owner.IsPlayerArea(card.GetLocation.Area) Then
                card.SetLocation(location)
                actionList.Add(New ActionData("MOVE", owner, cardId, location.GetCoordinates))
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
                actionList.Add(New ActionData("ROTATE", owner, rotation.ToString))
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
                actionList.Add(New ActionData("ROTATE", owner, isLocked.ToString))
            End If

            Return actionList

        End Function

    End Class
End Namespace
