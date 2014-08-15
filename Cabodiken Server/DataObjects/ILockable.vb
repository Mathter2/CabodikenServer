Namespace DataObjects
    Public Interface ILockable

        Function IsLocked() As Boolean

        Sub SetLock(lockStatus As Boolean)

    End Interface
End Namespace