﻿Imports MFierro.Cabodiken.DomainObjects

Namespace DataObjects

    <DataContract()>
    Public Class PlayerData
        Inherits UserData

        Private _customDices As List(Of ObjectData)
        Private _customDecks As List(Of ObjectData)
        Private _customTokens As List(Of ObjectData)
        Private _place As Integer
        Private _number As Integer
        Private _isHandLocked As Boolean = True
        Private _isAreaLocked As Boolean = True

        <DataMember()>
        Public Property CustomDices As ObjectData()
            Get
                Return _customDices.ToArray()
            End Get
            Set(value As ObjectData())
                For Each dice As ObjectData In value
                    _customDices.Add(dice)
                Next
            End Set
        End Property

        <DataMember()>
        Public Property CustomDecks As ObjectData()
            Get
                Return _customDecks.ToArray()
            End Get
            Set(value As ObjectData())
                For Each deck As ObjectData In value
                    _customDecks.Add(deck)
                Next
            End Set
        End Property

        <DataMember()>
        Public Property CustomTokens As ObjectData()
            Get
                Return _customTokens.ToArray()
            End Get
            Set(value As ObjectData())
                For Each token As ObjectData In value
                    _customTokens.Add(token)
                Next
            End Set
        End Property

        <DataMember()>
        Public Property Place As Integer
            Get
                Return _place
            End Get
            Set(value As Integer)
                _place = value
            End Set
        End Property

        <DataMember()>
        Public Property Number As Integer
            Get
                Return _number
            End Get
            Set(value As Integer)
                _number = value
            End Set
        End Property

        Public ReadOnly Property IsHandLocked As Boolean
            Get
                Return _isHandLocked
            End Get
        End Property

        Public ReadOnly Property IsAreaLocked As Boolean
            Get
                Return _isAreaLocked
            End Get
        End Property

        Public Sub New(id As Integer, name As String, host As Integer, message As String, isOnline As Boolean, _
                       customDices As List(Of ObjectData), customDecks As List(Of ObjectData), _
                       customTokens As List(Of ObjectData), place As Integer, number As Integer)
            MyBase.New(id, name, host, message, isOnline)
            _customDices = customDices
            _customDecks = customDecks
            _customTokens = customTokens
            _place = place
            _number = number
        End Sub

        Public Sub New(userData As UserData, customDices As List(Of ObjectData), customDecks As List(Of ObjectData) _
                       , customTokens As List(Of ObjectData), place As Integer, number As Integer)
            Me.New(userData.Id, userData.Name, userData.Host, userData.Message, userData.IsOnline, _
                   customDices, customDecks, customTokens, place, number)
        End Sub

        Public Sub New(userData As UserData, place As Integer, number As Integer)
            Me.New(userData, New List(Of ObjectData), New List(Of ObjectData), New List(Of ObjectData), place, number)
        End Sub

        Public Sub AddCustomObject(objectId As Integer, objectName As String, objectType As String)

            Dim customObject As New ObjectData(objectId, objectName, objectType)

            Select Case objectType
                Case "DECK"
                    _customDecks.Add(customObject)
                Case "DICE"
                    _customDices.Add(customObject)
                Case "TOKEN"
                    _customTokens.Add(customObject)
            End Select

        End Sub

        Public Function IsPlayerArea(playerArea As Area) As Boolean

            Select Case playerArea
                Case Area.Player1Area, Area.Player1Hand
                    Return Number = 1
                Case Area.Player2Area, Area.Player2Hand
                    Return Number = 2
                Case Area.Player3Area, Area.Player3Hand
                    Return Number = 3
                Case Area.Player4Area, Area.Player4Hand
                    Return Number = 4
                Case Area.Player5Area, Area.Player5Hand
                    Return Number = 5
                Case Area.Player6Area, Area.Player6Hand
                    Return Number = 6
                Case Area.Player7Area, Area.Player7Hand
                    Return Number = 7
                Case Area.Player8Area, Area.Player8Hand
                    Return Number = 8
                Case Else
                    Return False
            End Select

        End Function

        Public Sub LockHand(lock As Boolean)

            _isHandLocked = lock

        End Sub

        Public Sub LockArea(lock As Boolean)

            _isAreaLocked = lock

        End Sub

        Public Function GetUserData() As UserData
            Return New UserData(Id, Name, Host, Message, IsOnline)
        End Function

    End Class

End Namespace