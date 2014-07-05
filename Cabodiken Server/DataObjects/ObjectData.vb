Namespace DataObjects

    <DataContract()>
    Public Class ObjectData

        Private _id As Integer
        Private _name As String

        <DataMember()>
        Public Property Id As Integer
            Get
                Return _id
            End Get
            Set(value As Integer)
                _id = value
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

        Public Sub New(id As Integer, name As String)
            _id = id
            _name = name
        End Sub

    End Class

End Namespace