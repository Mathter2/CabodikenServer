Namespace DataObjects

    <DataContract()>
    Public Class ObjectData

        Private _id As Integer
        Private _name As String

        <DataMember()>
        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Name As String
            Get
                Return _name
            End Get
        End Property

        Public Sub New(id As Integer, name As String)
            _id = id
            _name = name
        End Sub

    End Class

End Namespace