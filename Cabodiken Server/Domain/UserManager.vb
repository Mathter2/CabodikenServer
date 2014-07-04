Imports MFierro.Cabodiken.DataObjects

Namespace Domain
    Public Class UserManager

#Region "Properties"

        Private Shared _instance As UserManager = New UserManager()
        Private _onlineUsers As String()
        Private _users As Dictionary(Of String, UserData)

        Public Shared ReadOnly Property Instance As UserManager
            Get
                Return _instance
            End Get
        End Property

#End Region

#Region "Constructors"

        Private Sub New()

        End Sub

#End Region

#Region "Methods"

        Public Sub AddFriend(sessionTokenId As String, friendName As String, friendHost As Host)

        End Sub

        Public Sub ChangeUserMessage(sessionTokenId As String, message As String)

        End Sub

        Public Function GetFriendsList(sessionTokenId As String) As UserData()

        End Function

        Public Function GetSessionToken(username As String, host As Host) As String

        End Function

        Public Function GetUser(sessionTokenId As String) As UserData
            Return _users(sessionTokenId)
        End Function

        Public Function LogIn(userName As String, host As Host, password As String) As String
            'Validate login
            Return GenerateSessionTokenId(userName)
        End Function

        Public Sub RegisterUser(userName As String, Host As Integer, password As String)

        End Sub

        Private Function GenerateSessionTokenId(userName As String) As String
            Dim sessionTokenId As String = ""
            Dim ticks As String = Date.Today.Ticks.ToString
            Dim random As New Random()

            If userName.Length < 6 Then
                For i As Integer = userName.Length To 6
                    userName += ChrW(random.Next(32, 126))
                Next
            End If

            If ticks.Length < 7 Then
                For i As Integer = ticks.Length To 7
                    ticks = "0" + ticks
                Next
            End If

            For i As Integer = 1 To 64
                Select Case i
                    Case 4
                        sessionTokenId += ticks.ToCharArray.GetValue(0)
                    Case 14
                        sessionTokenId += ticks.ToCharArray.GetValue(1)
                    Case 24
                        sessionTokenId += ticks.ToCharArray.GetValue(2)
                    Case 34
                        sessionTokenId += ticks.ToCharArray.GetValue(3)
                    Case 44
                        sessionTokenId += ticks.ToCharArray.GetValue(4)
                    Case 54
                        sessionTokenId += ticks.ToCharArray.GetValue(5)
                    Case 64
                        sessionTokenId += ticks.ToCharArray.GetValue(6)
                    Case 10
                        sessionTokenId += userName.ToCharArray.GetValue(3)
                    Case 20
                        sessionTokenId += userName.ToCharArray.GetValue(5)
                    Case 30
                        sessionTokenId += userName.ToCharArray.GetValue(4)
                    Case 40
                        sessionTokenId += userName.ToCharArray.GetValue(0)
                    Case 50
                        sessionTokenId += userName.ToCharArray.GetValue(2)
                    Case 60
                        sessionTokenId += userName.ToCharArray.GetValue(1)
                    Case Else
                        sessionTokenId += ChrW(random.Next(32, 126))
                End Select
            Next

            If _users.ContainsKey(sessionTokenId) Then
                sessionTokenId = GenerateSessionTokenId(userName)
            End If

            Return sessionTokenId

        End Function

#End Region

    End Class
End Namespace