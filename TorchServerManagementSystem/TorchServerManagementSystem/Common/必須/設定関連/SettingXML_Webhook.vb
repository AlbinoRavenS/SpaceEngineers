'''<summary>
'''XML形式設定データ(XMLに公開する設定項目)
'''</summary>
<Serializable()>
Public Class SettingXML_Webhook
    <NonSerialized>
    Private base As New SettingBase

    '※各型の初期値
    '初期値:String = Nothing
    '初期値:Integer = ０
    '初期値:Boolean = False

    'Note:XML出力するプロパティをPublic propertyで設定 

    Public Property Webhook_Global As String
    Public Property Webhook_Name As String


    ''' <summary>
    ''' 初期値の設定を行う。
    ''' </summary>
    Public Function Initialize() As Boolean
        Try
            Dim base As New SettingBase

            base.SetInitData(Webhook_Global, "")
            base.SetInitData(Webhook_Name, "")

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class