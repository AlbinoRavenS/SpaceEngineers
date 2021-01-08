Public Class Main

    Private settings As SettingMain
#Region "変更不要"
    ''' <summary>
    ''' 起動時必須機能
    ''' </summary>
    Private Sub StartUp()

        'クラッシュログの確認
        Message.CheckStartupCrashLog()

        '設定読み込み
        settings = New SettingMain


        '起動ログの書き出し
        MessageList.OutputStartUpLog()


    End Sub

    ''' <summary>
    ''' 終了時必須機能
    ''' </summary>
    Private Sub Shutdown()
        Message.WriteLog("ExitApplication", Message.logLevel.INFO)

        'クラッシュログの削除
        Message.deleteCrashLog()
    End Sub
#End Region
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="args"></param>
    Sub New(ByRef args As String())
        Try

            'スタートアップ処理
            Call StartUp()

            If settings.isEmpty Then
                args = Nothing
                args = New String() {"", "/setting"}
                MsgBox("空白の設定が有ります。設定完了後再起動してください")
            End If

            'フォーム呼出を記載(呼出方法は各フォームへ記載)
            Select Case True
                Case args.Count = 1
                    '引数無し(通常常駐)
                    Dim frmResidentSoftware As New ResidentSoftware(settings)
                    frmResidentSoftware.ShowInTaskbar = False 'タスクバーに非表示にする
                    frmResidentSoftware.ShowDialog()

                Case args(1).ToLower = "/setting"
                    '設定画面を表示
                    Dim frmSetting As New SettingForm(settings)
                    frmSetting.ShowInTaskbar = False 'タスクバーに非表示にする
                    frmSetting.ShowDialog()


                Case Else
                    '引数異常

            End Select

            '終了処理
            Call Shutdown()
        Catch ex As Exception
            Message.WriteExceptionErrLog(ex)
            Message.ShowMessage(ex.ToString)
        Finally
        End Try
    End Sub
End Class
