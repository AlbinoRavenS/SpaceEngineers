Public Class FileControl

    ''' <summary>
    ''' ロック中ファイルを開けるが、読み取り専用かつパフォーマンスに影響あり)
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Shared Function loadReadOnly(ByVal path As String) As List(Of String)
        Try
            If Not IO.File.Exists(path) Then
                Message.WriteLog("ファイルが見つかりませんでした。", Message.logLevel.ERR)
                Return New List(Of String)
            End If
            Dim text As String


            Using fs As New IO.FileStream(path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.ReadWrite)
                Using sr As IO.TextReader = New IO.StreamReader(fs, System.Text.Encoding.Default)
                    text = sr.ReadToEnd()
                End Using
            End Using

            Dim dataRows As New List(Of String)

            For Each line As String In Split(text, vbCrLf)
                dataRows.Add(line)
            Next
            Return dataRows
        Catch
            Throw
        End Try
    End Function

End Class
