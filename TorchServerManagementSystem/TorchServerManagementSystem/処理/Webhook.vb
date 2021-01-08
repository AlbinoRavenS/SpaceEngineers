Imports System.Net.Http
Imports System.Text.RegularExpressions
Imports System.Runtime.CompilerServices
Public Class Webhook

    ''' <summary>
    ''' Post文字コード
    ''' </summary>
    Private enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("shift_jis")

    ''' <summary>
    ''' 呼び出し
    ''' </summary>
    ''' <param name="WebhookURL"></param>
    ''' <param name="text"></param>
    Public Shared Sub PostDiscord(ByVal WebhookURL As String, ByVal text As String, ByVal notifyAccountName As String)
        _PostDiscord(WebhookURL, text, notifyAccountName)
    End Sub

    ''' <summary>
    ''' DiscordへPOSTする
    ''' </summary>
    ''' <param name="url"></param>
    ''' <param name="text"></param>
    Private Shared Sub _PostDiscord(ByVal url As String, ByVal text As String, ByVal notifyAccountName As String)
        'https://dobon.net/vb/dotnet/internet/webrequestpost.html

        Using _httpClient As HttpClient = New HttpClient
            Dim strs As Dictionary(Of String, String) = New Dictionary(Of String, String)

            strs.Add("content", text)
            strs.Add("username", notifyAccountName)
            strs.Add("avatar_url", "")

            Dim awaiter As TaskAwaiter(Of HttpResponseMessage) = _httpClient.PostAsync(url, New FormUrlEncodedContent(strs)).GetAwaiter()
            awaiter.GetResult()
        End Using
    End Sub


End Class
