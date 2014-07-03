Namespace Domain
    Public Interface ILockable

        Function IsLocked() As Boolean

        Sub Lock()

        Function Switch() As Boolean

        Sub Unlock()

    End Interface
End Namespace