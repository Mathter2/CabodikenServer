Namespace DataObjects

    <DataContract()>
    Public Class PlayerData
        Inherits UserData

        Private _customDices As List(Of ObjectData)
        Private _customDecks As List(Of ObjectData)
        Private _customTokens As List(Of ObjectData)
        Private _place As Integer

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

        Public Sub New(id As Integer, name As String, host As Integer, message As String, isOnline As Boolean, _
                       customDices As List(Of ObjectData), customDecks As List(Of ObjectData), _
                       customTokens As List(Of ObjectData), place As Integer)
            MyBase.New(id, name, host, message, isOnline)
            _customDices = customDices
            _customDecks = customDecks
            _customTokens = customTokens
            _place = place
        End Sub

        Public Sub New(userData As UserData, customDices As List(Of ObjectData), _
                       customDecks As List(Of ObjectData), customTokens As List(Of ObjectData), place As Integer)
            Me.New(userData.Id, userData.Name, userData.Host, userData.Message, userData.IsOnline, _
                   customDices, customDecks, CustomTokens, place)
        End Sub

        Public Sub New(userData As UserData, place As Integer)
            Me.New(userData, New List(Of ObjectData), New List(Of ObjectData), New List(Of ObjectData), place)
        End Sub

    End Class

End Namespace