Namespace DataObjects

    <DataContract()>
    Public Class UserData

        Private _id As Integer
        Private _name As String
        Private _host As Integer
        Private _message As String
        Private _isOnline As Boolean

        <DataMember()>
        Public Property Id() As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
            End Set
        End Property

        <DataMember()>
        Public Property Name() As String
            Get
                Return _name
            End Get
            Set(value As String)
                _name = value
            End Set
        End Property

        <DataMember()>
        Public Property Host() As Integer
            Get
                Return _host
            End Get
            Set(value As Integer)
                _host = value
            End Set
        End Property

        <DataMember()>
        Public Property Message() As String
            Get
                Return _message
            End Get
            Set(value As String)
                _message = value
            End Set
        End Property

        <DataMember()>
        Public Property IsOnline() As Boolean
            Get
                Return _isOnline
            End Get
            Set(value As Boolean)
                _isOnline = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, host As Integer, message As String, isOnline As Boolean)
            _id = id
            _name = name
            _host = host
            _message = message
            _isOnline = isOnline
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is UserData Then
                Dim otherUserData As UserData = CType(obj, UserData)
                Return Me.Name = otherUserData.Name AndAlso Me.Host = otherUserData.Host
            Else
                Return False
            End If
        End Function

    End Class

End Namespace
