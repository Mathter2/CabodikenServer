Namespace Domain
    Public Class ActionFactory

        Public Function GetAction(actionName As String) As IAction

            Select Case actionName.ToUpper
                Case "MOVE", "ROTATE", "LOCK"
                    Return New BasicAction()
                Case "SHUFFLE", "AGGREGATE", "SPLIT", "DRAW"
                    Return New ContainerAction()
                Case "FLIP", "SPIN", "THROW"
                    Return New SpecialAction()
                Case Else
                    Return New CustomAction()
            End Select

        End Function

    End Class
End Namespace