Namespace DataObjects

    <DataContract()>
    Public Class ActionData

        Private _index As Integer
        Private _name As String
        Private _owner As UserData
        Private _parameters As String()

        <DataMember()>
        Public ReadOnly Property Index As Integer
            Get
                Return _index
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Owner As UserData
            Get
                Return _owner
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Parameters As String()
            Get
                Return _parameters
            End Get
        End Property

        Public Sub New(index As Integer, name As String, owner As UserData, parameters As String())
            _index = index
            _name = name
            _owner = owner
            _parameters = parameters
        End Sub

    End Class
End Namespace