Namespace DataObjects

    <DataContract()>
    Public Class ActionData

        Private _index As Integer
        Private _name As String
        Private _owner As UserData
        Private _parameters As String()

        <DataMember()>
        Public Property Index As Integer
            Get
                Return _index
            End Get
            Set(value As Integer)
                _index = value
            End Set
        End Property

        <DataMember()>
        Public Property Name As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        <DataMember()>
        Public Property Owner As UserData
            Get
                If _owner.GetType = GetType(PlayerData) Then
                    Throw New Exception("placed player data in user data again...")
                End If
                Return _owner
            End Get
            Set(value As UserData)
                _owner = value
            End Set
        End Property

        <DataMember()>
        Public Property Parameters As String()
            Get
                Return _parameters
            End Get
            Set(value As String())
                _parameters = value
            End Set
        End Property

        Public Sub New(index As Integer, name As String, owner As UserData, ParamArray parameters As String())

            If owner.GetType() = GetType(PlayerData) Then
                owner = CType(owner, PlayerData).GetUserData()
            End If

            _index = index
            _name = name
            _owner = owner
            _parameters = parameters

        End Sub

    End Class
End Namespace