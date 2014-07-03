Namespace Domain
    Public MustInherit Class GameObject
        Implements IMovable, IRotable, ILockable

#Region "Properties"

        Private _id As Integer
        Private _resourceId As Integer
        Private _isLocked As Boolean
        Private _location As Location
        Private _rotation As Integer

        Public ReadOnly Property Id As Integer
            Get
                Return _id
            End Get
        End Property

        Public ReadOnly Property ResourceId As Integer
            Get
                Return _resourceId
            End Get
        End Property

#End Region

#Region "Constructors"

        Public Sub New(id As Integer, resourceId As Integer)
            _id = id
            _resourceId = resourceId
        End Sub

#End Region

#Region "Methods"

        Public Function IsLocked() As Boolean Implements ILockable.IsLocked

        End Function

        Public Sub Lock() Implements ILockable.Lock

        End Sub

        Public Function Switch() As Boolean Implements ILockable.Switch

        End Function

        Public Sub Unlock() Implements ILockable.Unlock

        End Sub

        Public Function GetLocation() As Location Implements IMovable.GetLocation

        End Function

        Public Sub SetArea(areaName As String) Implements IMovable.SetArea

        End Sub

        Public Sub SetLocation(x As Integer, y As Integer, z As Integer) Implements IMovable.SetLocation

        End Sub

        Public Function GerRotation() As Integer Implements IRotable.GerRotation

        End Function

        Public Sub Rotate(degrees As Integer) Implements IRotable.Rotate

        End Sub

        MustOverride Function GetObjectType() As String

#End Region

    End Class
End Namespace