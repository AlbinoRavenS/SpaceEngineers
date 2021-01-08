Imports Discord
Public Class CheckTorchLog

    Private settings As SettingMain

    Public Sub New(ByVal settings As SettingMain)

        Me.settings = settings

    End Sub

    ''' <summary>
    ''' Torchログフォルダパス
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property TorchLogFilePath As String
        Get
            Return IO.Path.Combine(settings.TorchLogFolder, settings.TorchLogFileName)
        End Get
    End Property

    ''' <summary>
    ''' Tempフォルダパス
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property TempFolderPath As String
        Get
            Return IO.Path.Combine(SettingMain.myDirectory, "Temp")
        End Get
    End Property

    ''' <summary>
    ''' Tempファイルパス
    ''' </summary>
    ''' <returns></returns>
    Private ReadOnly Property TempFilePath As String
        Get
            Return IO.Path.Combine(TempFolderPath, settings.TorchLogFileName)
        End Get
    End Property

    ''' <summary>
    ''' ログ確認処理メイン
    ''' ログで何かしらの処理があった場合Trueを返す
    ''' </summary>
    Public Function Main() As Boolean
        Dim refBool As Boolean
        'ログを比較し差分を取得
        Dim logDiffData As List(Of String) = GetDiffLog()

        '差分データで処理を実行
        refBool = LogDataControl(logDiffData)

        '差分がある場合、差分用Tempログファイルの更新
        If logDiffData.Any Then
            UpdateTempLog()
        End If

        Return refBool
    End Function


    ''' <summary>
    ''' TempファイルとTorchログを比較しTorchログに追加されたデータを取得する
    ''' </summary>
    ''' <returns></returns>
    Private Function GetDiffLog() As List(Of String)
        Dim newLogData As List(Of String) = FileControl.loadReadOnly(TorchLogFilePath)
        Dim tempLogData As List(Of String) = FileControl.loadReadOnly(TempFilePath)

        'Tempファイルに有る行をログファイルのデータから削除
        For Each tempLogRow As String In tempLogData
            newLogData.Remove(tempLogRow)
        Next

        Return newLogData
    End Function

    ''' <summary>
    ''' TorchログをTempファイルへ移動
    ''' </summary>
    Private Sub UpdateTempLog()
        Try
            'フォルダチェック/作成
            If Not IO.Directory.Exists(TempFolderPath) Then
                IO.Directory.CreateDirectory(TempFolderPath)
            End If

            'Tempファイルから対象以外を削除  
            For Each filePath As String In System.IO.Directory.GetFiles(TempFolderPath)
                If TempFilePath <> filePath Then
                    System.IO.File.Delete(filePath)
                End If
            Next

            'コピー
            If IO.File.Exists(TorchLogFilePath) Then
                IO.File.Copy(TorchLogFilePath, TempFilePath, True)
            End If

        Catch
            Throw
        End Try

    End Sub


    ''' <summary>
    ''' ログの行に応じた処理を行う
    ''' ログで何かしらの処理があった場合Trueを返す
    ''' </summary>
    ''' <param name="diffLog"></param>
    Private Function LogDataControl(ByVal diffLog As List(Of String)) As Boolean
        Dim refbool As Boolean = False

        For Each item As String In diffLog
            If RunLogControl(item) Then
                refbool = True
            End If
        Next

        Return refbool
    End Function
    ''' <summary>
    ''' ログデータに応じた処理を実行する。
    ''' 処理があればTrueを返す
    ''' </summary>
    ''' <param name="logRowData"></param>
    ''' <returns>isWorkLogData</returns>
    Private Function RunLogControl(ByVal logRowData As String) As Boolean
        Try

            Dim id As String = ""
            Dim strManageBase As String = "MultiplayerManagerBase:"

            Select Case True
                Case logRowData.Contains("Starting server watchdog.")
                    'サーバー起動
                    webhook.PostDiscord(settings.Webhook_Url, $"サーバーが起動しました。", settings.Webhook_Name)

                Case logRowData.Contains("Torch.Managers.MultiplayerManagerBase:") And logRowData.Contains("joined")
                    'プレイヤー接続
                    id = logRowData.Substring(logRowData.LastIndexOf(strManageBase) + Len(strManageBase))
                    id = id.Substring(0, id.LastIndexOf("joined"))
                    id = id.Replace("Player ", "")
                    webhook.PostDiscord(settings.Webhook_Url, $"{id}さんがログインしました。", settings.Webhook_Name)

                Case logRowData.Contains("Torch.Managers.MultiplayerManagerBase:") And logRowData.Contains("Disconnected")
                    'プレイヤー切断 
                    id = logRowData.Substring(logRowData.LastIndexOf(strManageBase) + Len(strManageBase))
                    id = id.Substring(0, id.LastIndexOf("("))
                    If Not id.Contains("ID:") Then
                        webhook.PostDiscord(settings.Webhook_Url, $"{id}さんがログアウトしました。", settings.Webhook_Name)
                    End If

                Case Else
                    Return False
            End Select
            Return True
        Catch
            Throw
        End Try
    End Function
End Class
