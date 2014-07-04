Namespace DataObjects

    <DataContract()>
    Public Class PlayerData
        Inherits UserData

        Private _customDices As ObjectData()
        Private _customDecks As ObjectData()
        Private _customTokens As ObjectData()
        Private _place As Integer

        <DataMember()>
        Public ReadOnly Property CustomDices As ObjectData()
            Get
                Return _customDices
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property CustomDecks As ObjectData()
            Get
                Return _customDecks
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property CustomTokens As ObjectData()
            Get
                Return _customTokens
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Place As Integer
            Get
                Return _place
            End Get
        End Property

        Public Sub New(id As Integer, name As String, host As Integer, message As String, isOnline As Boolean, _
                       customDices As ObjectData(), customDecks As ObjectData(), _
                       customTokens As ObjectData(), place As Integer)
            MyBase.New(id, name, host, message, isOnline)
            _customDices = customDices
            _customDecks = customDecks
            _customTokens = customTokens
            _place = place
        End Sub

    End Class

End Namespace