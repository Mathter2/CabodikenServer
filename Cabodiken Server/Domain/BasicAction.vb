Imports MFierro.Cabodiken.DataObjects
Imports MFierro.Cabodiken.DomainObjects

Namespace Domain
    Public Class BasicAction
        Implements IAction

        Public Function ExecuteAction(game As Game, owner As PlayerData, gameObject As GameObject, action As String, _
                                      parameters() As String) As List(Of ActionData) _
            Implements IAction.ExecuteAction

            Select Case action.ToUpper
                Case "MOVE"
                    If parameters.Count = 2 Then
                        Return ExecuteMove(owner, gameObject, parameters)
                    Else
                        Return ExecuteAreaMove(owner, gameObject, parameters)
                    End If
                Case "ROTATE"
                    Return ExecuteRotate(owner, gameObject, parameters)
                Case "LOCK"
                    Return ExecuteLock(owner, gameObject, parameters)
                Case Else
                    Return Nothing
            End Select

        End Function

        Private Function ExecuteAreaMove(owner As PlayerData, gameObject As GameObject, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim objectId As String = parameters(0)
            Dim location As Location = GetLocation(parameters(1))
            Dim moveArea As Area = CType(parameters(2), Area)

            If owner.IsPlayerArea(moveArea) OrElse moveArea = Area.Table OrElse _
                gameObject.GetLocation.Area = Area.Table OrElse owner.IsPlayerArea(gameObject.GetLocation.Area) Then

                gameObject.SetLocation(location)
                actionList.Add(New ActionData("REMOVE", owner, objectId))

            End If

        End Function

        Private Function ExecuteMove(owner As PlayerData, gameObject As GameObject, parameters As String()) _
            As List(Of ActionData)

            Dim actionList As New List(Of ActionData)
            Dim objectId As String = parameters(0)
            Dim location As Location = GetLocation(parameters(1))

            If gameObject.GetLocation.Area = Area.Table Or owner.IsPlayerArea(gameObject.GetLocation.Area) Then
                gameObject.SetLocation(location)
                actionList.Add(New ActionData("MOVE", owner, objectId, location.GetCoordinates))
            End If

            Return actionList

        End Function

        Private Function ExecuteRotate(owner As PlayerData, gameObject As GameObject, parameters As String()) _
            As List(Of ActionData)



        End Function

        Private Function ExecuteLock(owner As PlayerData, gameObject As GameObject, parameters As String()) _
            As List(Of ActionData)



        End Function

        Private Function GetLocation(locationData As String) As Location

            Dim locationDataParts As String() = locationData.Split(","c)
            Dim x As Integer = CType(locationDataParts(0), Integer)
            Dim y As Integer = CType(locationDataParts(1), Integer)
            Dim z As Integer = CType(locationDataParts(2), Integer)
            Dim area As Area = CType(locationDataParts(3), Area)

            Return New Location(x, y, z, area)

        End Function

    End Class
End Namespace