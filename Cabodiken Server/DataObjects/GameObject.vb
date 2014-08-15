Namespace DataObjects
    Public MustInherit Class GameObject
        Implements IMovable, IRotable, ILockable

#Region "Properties"

        Private _id As Integer
        Private _resourceId As Integer
        Private _isLocked As Boolean
        Private _location As Location
        Private _rotation As Integer
        Private _lastUsedIndex As Integer

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

        Public ReadOnly Property LastUsedIndex As Integer
            Get
                Return _lastUsedIndex
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

            Return _isLocked

        End Function

        Public Sub SetLock(lockStatus As Boolean) Implements ILockable.SetLock

            _isLocked = lockStatus

        End Sub

        Public Function GetLocation() As Location Implements IMovable.GetLocation

            Return _location

        End Function

        Public Sub SetArea(areaName As String) Implements IMovable.SetArea

        End Sub

        Public Sub SetLocation(location As Location) Implements IMovable.SetLocation

            _location = location

        End Sub

        Public Function GetRotation() As Integer Implements IRotable.GetRotation

        End Function

        Public Sub Rotate(degrees As Integer) Implements IRotable.Rotate

        End Sub

        Public Function validateAction(currentIndex As Integer) As Boolean

            If currentIndex > _lastUsedIndex Then
                _lastUsedIndex = currentIndex
                Return True
            Else
                Return False
            End If

        End Function

        MustOverride Function GetObjectType() As String

#End Region

    End Class
End Namespace