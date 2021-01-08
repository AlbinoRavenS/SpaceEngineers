''' <summary>
''' コマンドプロンプト処理
''' </summary>
Public Class Cmd

    ''' <summary>
    ''' コマンドを実行する
    ''' </summary>
    ''' <param name="args">実行引数</param>
    ''' <returns>戻り値</returns>
    Public Shared Function runCmd(ByVal args As String) As String
        Try
            Message.WriteLog(args, Message.logLevel.DEBUG)
            'Processオブジェクトを作成
            Using p As New System.Diagnostics.Process()

                'ComSpec(cmd.exe)のパスを取得して、FileNameプロパティに指定
                p.StartInfo.FileName = System.Environment.GetEnvironmentVariable("ComSpec")
                '出力を読み取れるようにする
                p.StartInfo.UseShellExecute = False
                p.StartInfo.RedirectStandardOutput = True
                p.StartInfo.RedirectStandardInput = False
                'ウィンドウを表示しないようにする
                p.StartInfo.CreateNoWindow = True
                'コマンドラインを指定（"/c"は実行後閉じるために必要）
                p.StartInfo.Arguments = "/c " & args

                '起動
                p.Start()

                '出力を読み取る
                Dim results As String = p.StandardOutput.ReadToEnd()

                'プロセス終了まで待機する
                'WaitForExitはReadToEndの後である必要がある
                '(親プロセス、子プロセスでブロック防止のため)
                p.WaitForExit()
                p.Close()

                '出力を返す
                Message.WriteLog(results, Message.logLevel.DEBUG)
                Return results

            End Using

        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
