''' <summary>
''' 常駐ソフト型基本フォーム
''' </summary>
Public Class ResidentSoftware

    ' 設定ファイルの取得
    Private settings As SettingMain

    ' ログ管理クラス
    Private checkTorchLog As CheckTorchLog

    ' 終了待機
    Private _ExitFlag As Boolean
    ' 終了待機
    Public Property Exitflag() As Boolean
        Get
            Return _ExitFlag
        End Get
        Private Set(value As Boolean)
            _ExitFlag = value
        End Set
    End Property

#Region "Event"
    ''' <summary>
    ''' メインタイマー
    ''' メイン処理はここから呼び出す
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer_Main_Tick(sender As Object, e As EventArgs) Handles Timer_Main.Tick
        Try
            ' 処理中はタイマーを動作しない
            Me.Timer_Main.Stop()

            ' メイン処理を呼び出す
            Call ResidentTaskMain()

            ' 処理完了後タイマーを起動
            Me.Timer_Main.Start()
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 終了確認タイマー
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer_ExitCheck_Tick(sender As Object, e As EventArgs) Handles Timer_ExitCheck.Tick
        If Exitflag And Timer_Main.Enabled Then
            Me.Close()
        End If
    End Sub

    ''' <summary>
    ''' 自動シャットダウン処理
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Timer_AutoShutdown_Tick(sender As Object, e As EventArgs) Handles Timer_AutoShutdown.Tick
        If settings.IsAutoShutdown Then
            Dim cmd As New Cmd
            Webhook.PostDiscord(settings.Webhook_Url, $"(自動)サーバーを停止します。", settings.Webhook_Name)
            Cmd.runCmd("start shutdown -s -f -t 60")
        End If
    End Sub



    ''' <summary>
    ''' 終了処理(Exit ボタン)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ExitToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Me.NotifyIcon_Taskbar.Visible = False
        Me.Exitflag = True
    End Sub

    ''' <summary>
    ''' 設定ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SettingToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SettingToolStripMenuItem.Click
        Dim frmSetting As SettingForm
        frmSetting = New SettingForm(settings)
        frmSetting.ShowDialog()
    End Sub
#End Region

    Public Sub New(ByRef settings As SettingMain)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Me.settings = settings
        Me.checkTorchLog = New CheckTorchLog(settings)

        ' タスクトレイアイコンの設定
        'Me.NotifyIcon_Taskbar.Icon = New System.Drawing.Icon(IO.Path.Combine(SettingMain.myDirectory, "Resources\Icon_NotifyResident.ico"))
        Me.NotifyIcon_Taskbar.ContextMenuStrip = ContextMenuStrip_Taskbar

        ' タイマー設定
        Me.Timer_Main.Interval = 1000 'msec
        Me.Timer_Main.Start()
        Me.Timer_ExitCheck.Interval = 1000 'msec
        Me.Timer_ExitCheck.Start()
        Me.Timer_AutoShutdown.Interval = settings.AutoShutdownInterval * 60 * 1000 'msec
        Me.Timer_AutoShutdown.Start()

        '初回実行
        Call Me.Timer_Main_Tick(New Object, New EventArgs)

    End Sub

    ''' <summary>
    ''' 常駐処理
    ''' </summary>
    Private Sub ResidentTaskMain()
        Try
            'ここに常駐処理を記載
            If checkTorchLog.Main() Then
                Timer_AutoShutdown.Enabled = False
                Timer_AutoShutdown.Enabled = True
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

End Class