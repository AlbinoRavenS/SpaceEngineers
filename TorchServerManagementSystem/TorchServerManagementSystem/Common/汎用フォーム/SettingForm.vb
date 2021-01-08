Public Class SettingForm

    Private settings As SettingMain

    Public Sub New(ByVal settings As SettingMain)

        ' この呼び出しはデザイナーで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。
        Me.settings = settings
    End Sub
#Region "イベント"
    ''' <summary>
    ''' 設定読み込み
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SettingForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Call ShowInitialize()
    End Sub

    ''' <summary>
    ''' 確定
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Call SaveSetting()
    End Sub

    ''' <summary>
    ''' 再読み込み
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Call ShowInitialize()
    End Sub
#End Region

    ''' <summary>
    ''' 設定読み込み
    ''' </summary>
    Private Sub ShowInitialize()
        TextBox_TorchFolderPath.Text = settings.TorchFolderPath
        TextBox_Webhook_Url.Text = settings.Webhook_Url
        TextBox_Webhook_Name.Text = settings.Webhook_Name
        CheckBox_isAutoShutdown.Checked = settings.IsAutoShutdown
        NumericUpDown_AutoShutodwnInterval.Value = settings.AutoShutdownInterval
    End Sub

    ''' <summary>
    ''' 設定の保存
    ''' </summary>
    Private Sub SaveSetting()
        Dim canSave As Boolean = True
        settings.TorchFolderPath = TextBox_TorchFolderPath.Text
        settings.Webhook_Url = TextBox_Webhook_Url.Text
        settings.Webhook_Name = TextBox_Webhook_Name.Text
        settings.IsAutoShutdown = CheckBox_isAutoShutdown.Checked
        If CInt(NumericUpDown_AutoShutodwnInterval.Value) = 0 Then
            Message.ShowMessage("停止時間(分)には1以上の数値を入力してください")
            canSave = False
        Else
            settings.AutoShutdownInterval = CInt(NumericUpDown_AutoShutodwnInterval.Value)
        End If


        If canSave Then
            settings.SaveSettings(False)
        Else
            Message.ShowMessage("設定は保存されませんでした。")
        End If

    End Sub

    ''' <summary>
    ''' 現設定と保存中の設定に違いが有るか
    ''' </summary>
    Private Function isChangeingSetting() As Boolean

        '違いが有ればTrueを返す
        If False OrElse
            TextBox_TorchFolderPath.Text <> settings.TorchFolderPath OrElse
            TextBox_Webhook_Url.Text <> settings.Webhook_Url OrElse
            TextBox_Webhook_Name.Text <> settings.Webhook_Name OrElse
            CheckBox_isAutoShutdown.Checked <> settings.IsAutoShutdown OrElse
            NumericUpDown_AutoShutodwnInterval.Value <> settings.AutoShutdownInterval Then

            Return True

        End If

        Return False
    End Function

End Class