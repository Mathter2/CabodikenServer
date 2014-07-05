Namespace DataObjects

    <DataContract()>
    Public Class ObjectData

        Private _id As Integer
        Private _name As String
        Private _type As String

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

        <DataMember()>
        Public Property Type As String
            Get
                Return _type
            End Get
            Set(ByVal value As String)
                _type = value
            End Set
        End Property


        Public Sub New(id As Integer, name As String, type As String)
            _id = id
            _name = name
            _type = type
        End Sub

        Public Overrides Function Equals(obj As Object) As Boolean
            If TypeOf obj Is ObjectData Then
                Dim otherObjectData As ObjectData = CType(obj, ObjectData)
                Return Me.Id = otherObjectData.Id AndAlso Me.Type = otherObjectData.Type
            Else
                Return False
            End If
        End Function

    End Class

End Namespace