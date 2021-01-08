Public Class MessageList

    Public Const msg0 As String = "msg連番にログの内容を追記していく ※インテリセンスで確認する為、複雑な変数名を使用しないこと"
    Public Const msg1 As String = "実行ファイルが見つかりませんでした。"
    Public Const msg2 As String = "EXEファイルを実行します。"


    '''<summary>
    '''起動ログの出力
    '''</summary>   
    Public Shared Sub OutputStartUpLog()
        Message.WriteLog("StartUp...", Message.logLevel.INFO)
        Message.WriteLog($"Arguments{vbTab}:" & String.Join(vbTab, System.Environment.GetCommandLineArgs()), Message.logLevel.INFO)
        Message.WriteLog($"Version {vbTab}:" & Reflection.Assembly.GetExecutingAssembly.GetName.Version.ToString(), Message.logLevel.INFO)
        Message.WriteLog($"Tarminal{vbTab}:" & Environment.MachineName, Message.logLevel.INFO)
        Message.WriteLog($"ProcessCode{vbTab}:" & System.Diagnostics.Process.GetCurrentProcess.Id.ToString, Message.logLevel.INFO)
    End Sub


End Class
