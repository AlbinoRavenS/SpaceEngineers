''' <summary>
''' Exe実行処理
''' </summary>
Public Class EXE

    Private Enum defaultExitCode
        ExitNotFindExe = 999
    End Enum


    ''' <summary>
    ''' Exeファイルを実行しExitCodeを受け取る
    ''' </summary>
    ''' <param name="exePath">実行exeフルパス</param>
    ''' <param name="arg">引数</param>
    ''' <param name="hideWindow">ウィンドウ表示の有無</param>
    ''' <param name ="waitForExit">実行終了を待機するか</param>
    ''' <returns>ExitCode</returns>
    Public Overloads Function RunExe(ByVal exePath As String, ByVal arg As String, ByVal hideWindow As Boolean, ByVal waitForExit As Boolean) As Integer

        Message.WriteLog(MessageList.msg2, Message.logLevel.DEBUG)
        Message.WriteLog(exePath, Message.logLevel.DEBUG)
        Message.WriteLog(arg, Message.logLevel.DEBUG)

        '実行ファイル存在確認
        If Not IO.File.Exists(exePath) Then
            Message.ShowMessage(MessageList.msg1)
            Return defaultExitCode.ExitNotFindExe
        End If


        Dim hProcess As New System.Diagnostics.Process
        Try
            ' ProcessStartInfoプロパティの設定
            Dim hPsInfo As New System.Diagnostics.ProcessStartInfo
            hPsInfo.FileName = exePath

            If arg <> String.Empty Then
                hPsInfo.Arguments = arg
            End If

            If hideWindow Then
                hPsInfo.CreateNoWindow = hideWindow
                hPsInfo.WindowStyle = ProcessWindowStyle.Hidden
            End If


            ' Run exe
            hProcess = System.Diagnostics.Process.Start(hPsInfo)

            ' Wait for exit
            If waitForExit Then
                hProcess.WaitForExit()
            End If


            Message.WriteLog(hProcess.ExitCode.ToString, Message.logLevel.DEBUG)
            Return hProcess.ExitCode

        Catch ex As Exception
            Throw
        Finally
            ' Dispose    
            hProcess.Close()
            hProcess.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' Exeファイルを実行しExitCodeを受け取る(Arguments無し)
    ''' </summary>
    ''' <param name="exePath">実行exeフルパス</param> 
    ''' <param name="hideWindow">ウィンドウ表示の有無</param>
    ''' <param name ="waitForExit">実行終了を待機するか</param>
    ''' <returns>ExitCode</returns>
    Public Overloads Function RunExe(ByVal exePath As String, ByVal hideWindow As Boolean, ByVal waitForExit As Boolean) As Integer
        Try
            Return RunExe(exePath, String.Empty, hideWindow, waitForExit)
        Catch ex As Exception
            Throw
        End Try
    End Function


#Region "Zip圧縮"

    Private ReadOnly Property zipLibPath As String
        Get
            Return IO.Path.Combine(My.Application.Info.DirectoryPath, "Lib\7zip\7z.exe")
        End Get
    End Property

    ''' <summary>
    ''' ZIP圧縮する [入力パス.zip]で出力 ※拡張子小文字
    ''' </summary>
    ''' <param name="srcPath">入力[ファイル/フォルダ]パス</param>
    Public Overloads Sub zip(ByVal srcPath As String)
        Try
            Dim path As String = zipLibPath
            Dim args As String = $"a ""{srcPath}.zip"" ""{srcPath}"""

            RunExe(path, args, True, True)

        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' ZIP圧縮する [出力パス.zip]で出力 ※拡張子小文字
    ''' </summary>
    ''' <param name="srcPath">入力[ファイル/フォルダ]パス</param>
    ''' <param name="destPath">出力[ファイル/フォルダ]パス</param>
    Public Overloads Sub zip(ByVal srcPath As String, ByVal destPath As String)
        Try
            '圧縮(コマンドライン)  
            Dim path As String = zipLibPath
            Dim args As String = $"a ""{destPath}.zip"" ""{srcPath}"""
            RunExe(path, args, True, True)

        Catch ex As Exception
            Throw
        End Try
    End Sub
#End Region

End Class
