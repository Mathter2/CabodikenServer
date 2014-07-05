Imports MFierro.Cabodiken.DataObjects
Imports MySql.Data

Namespace Domain
    Public Class UserManager

#Region "Properties"

        Private Shared _instance As UserManager = New UserManager()
        Private _onlineUsers As List(Of UserData)
        Private _users As Dictionary(Of String, UserData)
        Private _allowedChars As Char()
        Private _random As Random

        Public Shared ReadOnly Property Instance As UserManager
            Get
                Return _instance
            End Get
        End Property

#End Region

#Region "Constructors"

        Private Sub New()
            _users = New Dictionary(Of String, UserData)
            _onlineUsers = New List(Of UserData)
            _allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLOMNOPQRSTUVWXYZ0123456789".ToCharArray()
            _random = New Random
        End Sub

#End Region

#Region "Methods"

        Public Function AddFriend(sessionTokenId As String, friendName As String, friendHost As Integer) As Boolean

            Dim userId As Integer = _users(sessionTokenId).Id
            Return DataManager.Instance.AddFriend(userId, friendName, friendHost)

        End Function

        Public Sub ChangeUserMessage(sessionTokenId As String, message As String)

            Dim userId As Integer = _users(sessionTokenId).Id
            DataManager.Instance.ModifyUserMessage(userId, message)

        End Sub

        Public Function GetFriendsList(sessionTokenId As String) As UserData()
            Dim friendsList As UserData()
            Dim userId As Integer
            userId = _users(sessionTokenId).Id
            friendsList = DataManager.Instance.GetFriends(userId)
            For Each user As UserData In friendsList
                user.IsOnline = _onlineUsers.Contains(user)
            Next
            Return friendsList
        End Function

        Public Function GetUser(sessionTokenId As String) As UserData
            Return _users(sessionTokenId)
        End Function

        Public Function LogIn(userName As String, host As Integer, password As String) As String
            Dim sessionTokenId As String = Nothing
            Dim userData As UserData
            userData = DataManager.Instance.ValidateUser(userName, host, password)
            If userData IsNot Nothing Then
                sessionTokenId = GenerateSessionTokenId(userName)
                _users.Add(sessionTokenId, userData)
                _onlineUsers.Add(userData)
            End If
            Return sessionTokenId
        End Function

        Public Sub LogOut(sessionTokenId As String)
            _onlineUsers.Remove(GetUser(sessionTokenId))
            _users.Remove(sessionTokenId)
        End Sub

        Public Function RegisterUser(userName As String, Host As Integer, password As String) As Boolean

            Return DataManager.Instance.RegisterUser(userName, Host, password)

        End Function

        Public Function ValidateSessionToken(sessionTokenId As String) As UserData

            If _users.ContainsKey(sessionTokenId) Then
                Return _users(sessionTokenId)
            Else
                Return Nothing
            End If

        End Function

        Private Function GenerateSessionTokenId(userName As String) As String
            Dim sessionTokenId As String = ""
            Dim ticks As String = Date.Now.Ticks.ToString
            Dim random As New Random()

            If userName.Length < 6 Then
                For i As Integer = userName.Length To 6
                    userName += GetRandomAlphaNumber()
                Next
            End If

            If ticks.Length < 7 Then
                For i As Integer = ticks.Length To 7
                    ticks += "0"
                Next
            End If

            For i As Integer = 1 To 64
                Select Case i
                    Case 4
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 1).ToString
                    Case 14
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 2).ToString
                    Case 24
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 3).ToString
                    Case 34
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 4).ToString
                    Case 44
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 5).ToString
                    Case 54
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 6).ToString
                    Case 64
                        sessionTokenId &= ticks.ToCharArray.GetValue(ticks.Length - 7).ToString
                    Case 10
                        sessionTokenId &= userName.ToCharArray.GetValue(3).ToString
                    Case 20
                        sessionTokenId &= userName.ToCharArray.GetValue(5).ToString
                    Case 30
                        sessionTokenId &= userName.ToCharArray.GetValue(4).ToString
                    Case 40
                        sessionTokenId &= userName.ToCharArray.GetValue(0).ToString
                    Case 50
                        sessionTokenId &= userName.ToCharArray.GetValue(2).ToString
                    Case 60
                        sessionTokenId &= userName.ToCharArray.GetValue(1).ToString
                    Case Else
                        sessionTokenId &= GetRandomAlphaNumber()
                End Select
            Next

            If _users.ContainsKey(sessionTokenId) Then
                sessionTokenId = GenerateSessionTokenId(userName)
            End If

            Return sessionTokenId

        End Function

        Private Function GetRandomAlphaNumber() As String
            Return _allowedChars(_random.Next(_allowedChars.Length - 1))
        End Function

#End Region

    End Class
End Namespace