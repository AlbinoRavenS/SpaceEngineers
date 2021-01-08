Imports System.IO
Imports System.Windows.Forms

''' <summary>
''' ログの出力やユーザーへのメッセージ等はここで統合管理する。
''' </summary>
Public Class Message

    'TODO:Note:Messageでの設定はソフト再起動まで変更されない(Logが分割されると見難くなる為)
    '          また、Logフォルダパス設定のみ使用し、それ以外の設定はメッセージ内では使用しない事

    ''' <summary>
    ''' クラス用Setting
    ''' </summary>
    Private Shared settings As New SettingMain

    ''' <summary>
    ''' ログ書き出しが初回か判断
    ''' </summary>
    Private Shared isWrited As Boolean = False

    ''' <summary>
    ''' ログ書き出し用のタイムスタンプ
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property timeStamp As String
        Get
            Return DateTime.Today.ToString("yyyy/MM/dd") & " " & Date.Now.ToString("HH:mm:ss")
        End Get
    End Property

    Private Const _DELIMITER As String = ""","""

    ''' <summary>
    ''' メッセージ表示の許可(デフォルトTrue)
    ''' </summary>
    Public Shared canShowMessage As Boolean = True

    ''' <summary>
    ''' ログ レベル
    ''' <para>INFO :処理情報</para>
    ''' <para>WARN :ユーザー操作による警告</para>
    ''' <para>ERR  :復帰可能なエラー</para>
    ''' <para>FATAL:復帰不可の重大エラー</para>
    ''' <para>DEBUG:デバッグ用ログ(最新のみ出力)</para>
    ''' </summary>
    Public Enum logLevel
        INFO
        WARN
        ERR
        FATAL
        DEBUG
    End Enum

    ''' <summary>
    ''' プロセスIDの取得
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property processID As String
        Get
            Return System.Diagnostics.Process.GetCurrentProcess.Id.ToString
        End Get
    End Property

#Region "パス設定"
    'logPath:標準ログ                                         '設定ログディレクトリ\Log\yyyyMMdd.log
    'errPath:エラーログ(Exception)                            '設定ログディレクトリ\ErrLog\yyyyMMdd.log
    'fullLogPath:フルログ(直前の起動のみ保持)                    'ソフトディレクトリ\アプリケーション名_full.log
    'crashLogPath:クラッシュログパス(ソフト終了時に消える)         'ソフトディレクトリ\アプリケーション名_crash.log
    'crashLogSavedPath:クラッシュログ保存パス                   'ソフトディレクトリ\crashLog\crash_yyyyMMdd_hhmmssfff.log
    'retryOverLogPath:リトライ超過時のログ出力先                 'ソフトディレクトリ\retryOverLog_yyyyMMdd_hhmmssfff.log


    ''' <summary>
    ''' 標準ログのパス
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property LogPath As String
        Get
            Return IO.Path.Combine(settings.logDir, $"SysLog\{DateTime.Today.ToString("yyyy")}\{DateTime.Today.ToString("yyyyMMdd")}.log")
        End Get
    End Property

    ''' <summary>
    ''' エラーログ専用のパス
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property ErrPath As String
        Get
            Return IO.Path.Combine(settings.logDir, $"ErrLog\{DateTime.Today.ToString("yyyy")}\{DateTime.Today.ToString("yyyyMMdd")}.log")
        End Get
    End Property

    ''' <summary>
    ''' フルログ出力用パス
    ''' </summary>
    ''' <returns></returns> 
    Private Shared ReadOnly Property FullLogPath As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, $"{My.Application.Info.AssemblyName}_Full.log")
        End Get
    End Property

    ''' <summary>
    ''' クラッシュログ出力用パス
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property CrashLogPath As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, $"{My.Application.Info.AssemblyName}_Crash.log")
        End Get
    End Property

    ''' <summary>
    ''' クラッシュログ保存先パス
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property CrashLogSavedPath As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, $"Log_OldCrash\Crash_{Now.ToString("yyyyMMdd_hhmmssfff")}.log")
        End Get
    End Property

    ''' <summary>
    ''' リトライ超過ログ出力用パス
    ''' </summary>
    ''' <returns></returns>
    Private Shared ReadOnly Property RetryOverLogPath As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, $"Log_RetryOver\RetryOver_{Now.ToString("yyyyMMdd_hhmmssfff")}.log")
        End Get
    End Property

#End Region

#Region "メッセージ表示関連"
    ''' <summary>
    ''' メッセージ表示が許可されている場合に限り表示(デフォルトTrue)
    ''' </summary>
    ''' <param name="messageStr"></param>
    Public Shared Sub ShowMessage(ByVal messageStr As String)

        'メッセージログを出力する
        WriteLog("MSG : " & messageStr, logLevel.DEBUG)

        'メッセージ表示が許可されている場合に限り表示
        If canShowMessage Then
            MessageBox.Show(messageStr, My.Application.Info.Title)
        End If

    End Sub
#End Region
#Region "ログ出力関連"
    ''' <summary>
    ''' ログを出力する
    ''' </summary>
    ''' <param name="logMsg"></param>
    ''' <param name="logLevel"></param>
    Public Shared Sub WriteLog(ByVal logMsg As String, ByVal logLevel As logLevel)
        'ログ出力内容
        Dim row As New List(Of String)

        'ログレベルの左詰め
        Dim strLogLevel As String
        strLogLevel = logLevel.ToString.PadRight(5)

        '出力行を作成
        row.Add(strLogLevel)  'ログレベルの出力
        row.Add(timeStamp)          'タイムスタンプ出力
        row.Add(processID)          'プロセスIDを出力
        row.Add(logMsg)             'ログメッセージ

        'デバッグ以外は通常ログに書き出す
        If Not logLevel = logLevel.DEBUG Then
            '通常ログに書き込みを行う
            Write(row, LogPath)
        End If


        Write(row, FullLogPath) 'フルログに書き込みを行う  
        Write(row, CrashLogPath) 'クラッシュログに書き込みを行う

    End Sub

    ''' <summary>
    ''' 例外処理エラーログの出力(同時に通常ログも出力)
    ''' </summary>
    ''' <param name="ex"></param>
    Public Overloads Shared Sub WriteExceptionErrLog(ByVal ex As Exception)
        'エラー出力内容
        Dim row As New List(Of String)

        '出力行を作成
        row.Add(timeStamp)
        row.Add(processID)
        row.Add(ex.ToString)

        '書き込み
        Write(row, ErrPath)

        '通常ログにも書き込みを行う
        WriteLog("重大エラー:エラーログを確認してください", logLevel.FATAL)
        'フルログにも書き込みを行う
        WriteLog(ex.ToString, logLevel.DEBUG)


    End Sub

    ''' <summary>
    ''' 例外処理エラーログの出力(同時に通常ログも出力)
    ''' </summary>
    ''' <param name="ex"></param>
    ''' <param name="isExceptionData">エラーデータの引数か(引数間違え対策)</param>
    Public Overloads Shared Sub WriteExceptionErrLog(ByVal ex As String, ByVal isExceptionData As Boolean)
        'エラー出力内容
        Dim row As New List(Of String)

        '出力行を作成
        row.Add(timeStamp)
        row.Add(processID)
        row.Add(ex)

        '書き込み
        Write(row, ErrPath)

        '通常ログにも書き込みを行う
        WriteLog("重大エラー:エラーログを確認してください", logLevel.FATAL)
        'フルログにも書き込みを行う
        WriteLog(ex.ToString, logLevel.DEBUG)

    End Sub

    ''' <summary>
    ''' ログに書き込みを行う
    ''' </summary>
    ''' <param name="strList">書き込む</param>
    ''' <param name="path"></param>
    Private Shared Sub Write(ByVal strList As List(Of String), ByVal path As String, Optional ByVal retryCount As Integer = 0)
        'ログ初回処理時はフルログを削除する
        If Not isWrited Then
                If IO.File.Exists(FullLogPath) Then
                    IO.File.Delete(FullLogPath)
                End If
                isWrited = True
            End If

            'リトライ回数
            If 10 <= retryCount Then
                RetryOverWrite(strList)
                Return
            End If

            'ディレクトリチェック
            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(path))
            End If

        Dim writeData As String = """" & String.Join(_DELIMITER, strList.ToArray) & """"

        Try
            Using writer As New IO.StreamWriter(path, True, System.Text.Encoding.GetEncoding("shift_jis"))
                'ファイル書き込み
                writer.WriteLine(writeData)
            End Using
        Catch ex As Exception
            'リトライ(100ミリ秒待って実行)
            Threading.Thread.Sleep(100)
            retryCount += 1
            Write(strList, path, retryCount)
        End Try
    End Sub

    ''' <summary>
    ''' ログ書き込み異常時、新規の時間ファイルに書き出す
    ''' </summary>
    ''' <param name="strList"></param>
    Private Shared Sub RetryOverWrite(ByVal strList As List(Of String))
        Dim writeData As String = """" & String.Join(_DELIMITER, strList.ToArray) & """"

        Try
            'ディレクトリ作成
            If Not IO.Directory.Exists(IO.Path.GetDirectoryName(RetryOverLogPath)) Then
                IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(RetryOverLogPath))
            End If

            Using writer As New IO.StreamWriter(RetryOverLogPath, True, System.Text.Encoding.GetEncoding("shift_jis"))
                'ファイル書き込み
                writer.WriteLine(writeData)
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region
#Region "クラッシュログ関連"
    ''' <summary>
    ''' クラッシュログが存在する場合、クラッシュログ保存フォルダへ移動
    ''' </summary>
    Public Shared Sub CheckStartupCrashLog()
        Try
            If IO.File.Exists(CrashLogPath) Then
                If Not IO.Directory.Exists(IO.Path.GetDirectoryName(CrashLogSavedPath)) Then
                    IO.Directory.CreateDirectory(IO.Path.GetDirectoryName(CrashLogSavedPath))
                End If
                IO.File.Move(CrashLogPath, CrashLogSavedPath)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub
    ''' <summary>
    ''' クラッシュログを削除する
    ''' </summary>
    Public Shared Sub deleteCrashLog()
        IO.File.Delete(CrashLogPath)
    End Sub
#End Region

End Class
