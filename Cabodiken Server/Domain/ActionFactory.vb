Namespace Domain
    Public Class ActionFactory

        Public Function GetAction(objectType As String) As IAction

            Select Case objectType
                Case "CARD"
                    Return New CardAction()
                Case Else
                    Return New CustomAction()
            End Select

        End Function

    End Class
End Namespace