Namespace DataObjects

    <DataContract()>
    Public Class PlayerData
        Inherits UserData

        Private _customDices As ObjectData()
        Private _customDecks As ObjectData()
        Private _customTokens As ObjectData()
        Private _place As Place

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
        Public ReadOnly Property Place As Place
            Get
                Return _place
            End Get
        End Property

        Public Sub New(name As String, host As Host, isOnline As Boolean, customDices As ObjectData(), _
                       customDecks As ObjectData(), customTokens As ObjectData(), place As Place)
            MyBase.New(name, host, isOnline)
            _customDices = customDices
            _customDecks = customDecks
            _customTokens = customTokens
            _place = place
        End Sub

    End Class

End Namespace