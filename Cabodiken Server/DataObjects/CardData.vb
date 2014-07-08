Namespace DataObjects
    <DataContract()>
    Public Class CardData
        Inherits ObjectData

        Private _back As String
        Private _front As String

        <DataMember()>
        Public Property Back As String
            Get
                Return _back
            End Get
            Set(value As String)
                _back = value
            End Set
        End Property

        <DataMember()>
        Public Property Front As String
            Get
                Return _front
            End Get
            Set(value As String)
                _front = value
            End Set
        End Property

        Public Sub New(id As Integer, name As String, back As String, front As String)
            MyBase.New(id, name, "CARD")
            _back = back
            _front = front
        End Sub

    End Class

End Namespace