Namespace DataObjects

    <DataContract()>
    Public Class UserData

        Private _name As String
        Private _host As Host
        Private _isOnline As Boolean

        <DataMember()>
        Public ReadOnly Property Name() As String
            Get
                Return _name
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Host() As Host
            Get
                Return _host
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property IsOnline() As Boolean
            Get
                Return _isOnline
            End Get
        End Property

        Public Sub New(name As String, host As Host, isOnline As Boolean)
            _name = name
            _host = host
            _isOnline = isOnline
        End Sub

    End Class

End Namespace
