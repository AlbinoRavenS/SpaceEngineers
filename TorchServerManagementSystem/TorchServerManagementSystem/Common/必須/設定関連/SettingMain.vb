''' <summary>
''' 内部で使用する設定項目
''' 
''' <para>設定方法</para>
''' <para>"設定ファイル取り込み設定"で設定クラスのインスタンス作成とパス指定を行う。</para>
''' <para>"コンストラクタ"で設定のロードを行う Ex:laodSetting(外部設定インスタンス,外部設定パス)</para>
''' <para>"SaveSettings"に保存の設定を行う Ex:SaveSetting(外部設定インスタンス, 外部設定パス, isInitializeSave)</para>
''' <para>"設定一覧"へ設定の追加を行う</para>
''' </summary>
Public Class SettingMain

    'TODO:Note:./による相対パス設定は原則禁止、My.Application.Info.DirectoryPathを使用。(タスクスケジュール実行時にエラーが発生する為)
    'TODO:Note:設定の追加は問題無いが、設定の削除はデシリアライズ時にエラー(全初期化される)

    '設定の読込を複数ファイル行う場合はインスタンス作成/パス設定/初期化処理/保存処理の追加を行う事

#Region "変更不要"
    ''' <summary>
    ''' 相対パス
    ''' </summary>
    ''' <returns></returns>
    Public Shared ReadOnly Property myDirectory As String = My.Application.Info.DirectoryPath

    ''' <summary>
    ''' 設定読み込み処理
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="settings"></param>
    ''' <param name="path"></param>
    Private Sub loadSetting(Of T)(ByRef settings As T, ByVal path As String)
        Try
            Setting.Load(settings, path)
        Catch ex As Exception
            Message.ShowMessage("設定の取得に失敗しました。初期値を読み込みます。")
            Message.WriteExceptionErrLog(ex)
        End Try
    End Sub

    ''' <summary>
    ''' 設定の保存
    ''' </summary>
    Public Sub SaveSetting(Of T)(ByRef settings As T, ByVal path As String, isInitializeSave As Boolean)
        Try
            '保存
            If isInitializeSave Then
                '初期化時の保存はファイルが無い場合のみ実行
                If Not IO.File.Exists(path) Then
                    Setting.Save(settings, path)
                End If
            Else
                Setting.Save(settings, path)
            End If

        Catch ex As Exception
            Throw
        End Try
    End Sub

#End Region

#Region "設定ファイル読み込み設定"
    ''' <summary>
    ''' 外部設定インスタンス(メイン)
    ''' </summary>
    Dim _settingXML_Main As New SettingXML_Main

    ''' <summary>
    ''' 外部設定インスタンス(サブ/不要なら削除)
    ''' </summary>
    Dim _settingXML_Webhook As New SettingXML_Webhook

    ''' <summary>
    ''' 外部設定パス_メイン(基本はExeと同ディレクトリ)
    ''' </summary>
    Public Shared ReadOnly Property settingPath_Main As String
        Get
            Return IO.Path.Combine(myDirectory, "Setting.xml")
        End Get
    End Property

    ''' <summary>
    ''' 外部設定パス_サブ(基本はExeと同ディレクトリ)
    ''' </summary>
    Public Shared ReadOnly Property settingPath_Sub As String
        Get
            Return IO.Path.Combine(myDirectory, "Setting_Webhooks.xml")
        End Get
    End Property

#End Region

#Region "基本/読込/保存処理"
    ''' <summary>
    ''' コンストラクタ 設定クラスの初期化を行う
    ''' </summary>
    Public Sub New()
        Try

            Call Me.LoadSettings()

            '外部設定の初期化
            _settingXML_Main.Initialize()
            _settingXML_Webhook.Initialize()

            Me.SaveSettings(True)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 設定の読み込み
    ''' </summary>
    Public Sub LoadSettings()
        Try
            '外部設定の取込み
            loadSetting(_settingXML_Main, settingPath_Main)
            loadSetting(_settingXML_Webhook, settingPath_Sub)
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' 設定の保存
    ''' </summary>
    Public Sub SaveSettings(ByVal isInitializeSave As Boolean)
        Try
            '保存
            SaveSetting(_settingXML_Main, settingPath_Main, isInitializeSave)
            SaveSetting(_settingXML_Webhook, settingPath_Sub, isInitializeSave)
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

#Region "設定値"
#Region "MAIN"
    ''' <summary>
    ''' ログディレクトリ設定(読み替え処理後)
    ''' </summary> 
    ''' <returns></returns>
    Public ReadOnly Property logDir As String
        Get
            Return logDirectory()
        End Get
    End Property

    ''' <summary>
    ''' ログディレクトリのベース(処理で使用するログ/エラーログディレクトリは別途設定を読み込むこと。)
    ''' </summary>
    ''' <returns></returns>
    Public Property logDirBase As String
        Get
            Return _settingXML_Main.LogDirectory
        End Get
        Set(value As String)
            _settingXML_Main.LogDirectory = value
        End Set
    End Property


    ''' <summary>
    ''' Torchフォルダ
    ''' </summary>
    ''' <returns></returns>
    Public Property TorchFolderPath As String
        Get
            Return _settingXML_Main.TorchFolder
        End Get
        Set(value As String)
            _settingXML_Main.TorchFolder = value
        End Set
    End Property

    Public ReadOnly Property TorchLogFolder As String
        Get
            Return IO.Path.Combine(TorchFolderPath, "Logs")
        End Get
    End Property
    ''' <summary>
    ''' Torchログファイル名
    ''' </summary>
    ''' <returns></returns>
    Public ReadOnly Property TorchLogFileName As String
        Get
            Return $"Torch-{Today().ToString("yyyy-MM-dd")}.log"
        End Get
    End Property

    ''' <summary>
    ''' 自動シャットダウン機能
    ''' </summary>
    ''' <returns></returns>
    Public Property IsAutoShutdown As Boolean
        Get
            Return _settingXML_Main.IsAutoShutdown
        End Get
        Set(value As Boolean)
            _settingXML_Main.IsAutoShutdown = value
        End Set
    End Property

    ''' <summary>
    ''' 自動シャットダウンタイマー
    ''' </summary>
    ''' <returns></returns>
    Public Property AutoShutdownInterval As Integer
        Get
            Return _settingXML_Main.AutoShutdownInterval
        End Get
        Set(value As Integer)
            _settingXML_Main.AutoShutdownInterval = value
        End Set
    End Property
#End Region
#Region "Webhook"

    ''' <summary>
    ''' Webhook
    ''' </summary>
    ''' <returns></returns>
    Public Property Webhook_Url As String
        Get
            Return _settingXML_Webhook.Webhook_Global
        End Get
        Set(value As String)
            _settingXML_Webhook.Webhook_Global = value
        End Set
    End Property

    ''' <summary>
    ''' Webhook
    ''' </summary>
    ''' <returns></returns>
    Public Property Webhook_Name As String
        Get
            Return _settingXML_Webhook.Webhook_Name
        End Get
        Set(value As String)
            _settingXML_Webhook.Webhook_Name = value
        End Set
    End Property
#End Region
#End Region


#Region "設定読み替え処理"
    ' 設定を読み込んだ際に違う値で使用したい場合、基本ここで処理する。

    ''' <summary>
    ''' ログディレクトリを初回で固定する処理(USBがDドライブになった場合の対策)
    ''' </summary>
    ''' <returns></returns>
    Private Property logDirectoryPath As String

    ''' <summary>
    ''' <para>ログディレクトリ</para>
    ''' <para>ログディレクトリが"Default"の場合は[S:\G-PRONET\Log\アセンブリ名]又は[D:\Share\~]を返す。</para>
    ''' <para>ログディレクトリが"ApplicationFolder"の場合と、</para>
    ''' <para>ログディレクトリが異常(存在しないドライブを設定等)正常に生成されない場合は相対パス+Logを返す。</para>
    ''' </summary>
    ''' <returns></returns>
    Private Function logDirectory() As String
        Try
            If logDirectoryPath Is Nothing Then
                Dim refPath As String = String.Empty

                '特定の文字列の場合、処理を変える
                Select Case _settingXML_Main.LogDirectory.ToLower
                    Case "applicationfolder"
                        refPath = IO.Path.Combine(myDirectory, "Log")
                    Case Else
                        refPath = _settingXML_Main.LogDirectory
                End Select

                'ディレクトリチェック
                If Not IO.Directory.Exists(refPath) Then
                    Try
                        '設定値でディレクトリ作成
                        IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(refPath))

                    Catch ex As Exception

                        '設定値で生成失敗した場合
                        '相対パス+Logで生成しなおす
                        refPath = IO.Path.Combine(myDirectory, "Log")
                        '再生成した場合の処理
                        If Not IO.Directory.Exists(refPath) Then
                            IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(refPath))
                        End If
                    End Try
                End If
                logDirectoryPath = refPath
            End If

            Return logDirectoryPath
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

#Region "その他処理"
    ''' <summary>
    ''' 設定に空が有るかチェックする
    ''' </summary>
    ''' <returns></returns>
    Public Function IsEmpty() As Boolean
        If Me.TorchFolderPath = "" OrElse
            Me.Webhook_Url = "" OrElse
            Me.Webhook_Name = "" Then

            Return True

        End If

        Return False
    End Function
#End Region

End Class
