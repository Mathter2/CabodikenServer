Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class Location

        Private _area As Area
        Private _x As Integer
        Private _y As Integer
        Private _z As Integer

        Public ReadOnly Property X As Integer
            Get
                Return _x
            End Get
        End Property

        Public ReadOnly Property Y As Integer
            Get
                Return _y
            End Get
        End Property

        Public ReadOnly Property Z As Integer
            Get
                Return _z
            End Get
        End Property

        Public ReadOnly Property Area As Area
            Get
                Return _area
            End Get
        End Property

        Public Sub New(x As Integer, y As Integer, z As Integer, area As Area)

            SetCoordinates(x, y, z, area)

        End Sub

        Public Sub New(locationData As String)

            Dim locationDataParts As String() = locationData.Split(","c)
            Dim x As Integer = CType(locationDataParts(0), Integer)
            Dim y As Integer = CType(locationDataParts(1), Integer)
            Dim z As Integer = CType(locationDataParts(2), Integer)
            Dim area As Area = CType(locationDataParts(3), Area)

            SetCoordinates(x, y, z, area)

        End Sub

        Public Function GetCoordinates() As String

            Return _x & "," & _y & "," & _z

        End Function

        Public Sub SetCoordinates(x As Integer, y As Integer, z As Integer, area As Area)

            _x = x
            _y = y
            _z = z
            _area = area

        End Sub

    End Class
End Namespace