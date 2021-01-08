'''<summary>
'''XML形式設定データ(XMLに公開する設定項目)
'''</summary>
<Serializable()>
Public Class SettingXML_Main
    <NonSerialized>
    Private base As New SettingBase

    '※各型の初期値
    '初期値:String = Nothing
    '初期値:Integer = ０
    '初期値:Boolean = False

    'Note:XML出力するプロパティをPublic propertyで設定
    'ログディレクトリパス
    Public Property LogDirectory As String

    'Torchフォルダ
    Public Property TorchFolder As String

    '自動シャットダウンを有効にするか
    Public Property IsAutoShutdown As Boolean

    '自動シャットダウンのタイマー
    Public Property AutoShutdownInterval As Integer

    ''' <summary>
    ''' 初期値の設定を行う。
    ''' </summary>
    Public Function Initialize() As Boolean
        Try
            Dim base As New SettingBase

            base.SetInitData(LogDirectory, "Application")
            base.SetInitData(TorchFolder, "C:\TorchSystem\Torch")

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class