''' <summary>
''' コマンドプロンプト処理
''' </summary>
Public Class Cmd

    ''' <summary>
    ''' robocopyを実行する(/MIR /NP /R:0 /W:0) Return:変更のあったファイルパス一覧
    ''' </summary>
    ''' <param name="srcFolder">ミラー元</param>
    ''' <param name="destFolder">ミラー先</param>
    ''' <returns>変更のあったファイルパス一覧</returns> 
    Public Function robocopy(ByVal srcFolder As String, ByVal destFolder As String) As List(Of String)

        Dim refList As New List(Of String)
        Try

            '実行コマンドライン設定
            Dim args As String = $"robocopy {srcFolder} {destFolder} /MIR /NP /R:0 /W:0"

            'コマンド実行
            Dim cmdResult As String = runCmd(args)

            '戻り値を処理 
            Dim a As New List(Of String())
            For Each n As String In Split(cmdResult, vbCrLf)
                a.Add(Split(n, vbTab))
            Next

            Dim dirPath As String = String.Empty
            For Each lineData As String In Split(cmdResult, vbCrLf)

                Dim splData As String() = Split(lineData, vbTab)

                If splData Is Nothing Then Continue For
                If splData.Count < 3 Then Continue For

                If splData.Count = 3 Then
                    'コピー先ディレクトリを設定(コピー元→コピー先で置換)
                    dirPath = Replace(splData(2), srcFolder, destFolder)

                ElseIf splData.Count = 5 Then

                    If splData(1).Contains("新しい") OrElse splData(1).Contains("古い") Then
                        'ファイル名を設定

                        Dim fileName As String = splData(4)

                        'ファイルパスをセット
                        refList.Add(IO.Path.Combine(dirPath, fileName))
                    End If
                End If

            Next

            Return refList
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' コマンドを実行する
    ''' </summary>
    ''' <param name="args">実行引数</param>
    ''' <returns>戻り値</returns>
    Private Function runCmd(ByVal args As String) As String
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
