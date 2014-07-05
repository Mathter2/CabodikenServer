Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class Game

        Private _actionFactory As ActionFactory
        Private _actions As List(Of ActionData)
        Private _gameSession As GameSessionData
        Private _isGameStarted As Boolean
        Private _lastObjectId As Integer
        Private _placedObjects As Dictionary(Of Integer, GameObject)

        Public ReadOnly Property GameSession As GameSessionData
            Get
                Return _gameSession
            End Get
        End Property

        Public Sub AddCustomObject(customObject As ObjectData, objectType As String)

        End Sub

        Public Sub AddUser(user As UserData)

        End Sub

        Public Sub ExecuteAction(owner As UserData, action As String, ParamArray parameters As String())

        End Sub

        Public Function GetActions(lastActionIndex As Integer) As ActionData()

        End Function

        Public Function GetResources() As ResourceLibrary

        End Function

        Public Sub Start()

        End Sub

    End Class
End Namespace