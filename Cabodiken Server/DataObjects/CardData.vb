Namespace DataObjects
    <DataContract()>
    Public Class CardData
        Inherits ObjectData

        Private _back As ImageData
        Private _front As ImageData

        <DataMember()>
        Public ReadOnly Property Back As ImageData
            Get
                Return _back
            End Get
        End Property

        <DataMember()>
        Public ReadOnly Property Front As ImageData
            Get
                Return _front
            End Get
        End Property

        Public Sub New(id As Integer, name As String, back As ImageData, front As ImageData)
            MyBase.New(id, name)
            _back = back
            _front = front
        End Sub

    End Class

End Namespace