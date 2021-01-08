Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting

<TestClass()> Public Class UnitTest_Main

    Dim settings As TorchServerManagementSystem.SettingMain

    Public Sub New()
        settings = New TorchServerManagementSystem.SettingMain
        settings.TorchFolderPath = "C:\Users\legom\Desktop\TorchServerManagementSystem\TorchServerManagementSystemTests\TestData\C\Torch"
        settings.Webhook_Url = "https://discordapp.com/api/webhooks/718489082686341241/Jdkb1v9typyL44FZUBhY6bP1hv1-ZRy73jiDTHSDu3GlZZUF0CbKUI7xVoa3IgzRR_X2"
        settings.Webhook_Name = "test"
        settings.SaveSettings(True)
    End Sub

    ''' <summary>
    ''' コマンドラインテスト
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_CommandLine()
        Dim argTest As TorchServerManagementSystem.Main

        Dim args As String() = {"path", "/t", "arg1", "arg7"}

        argTest = New TorchServerManagementSystem.Main(args)

    End Sub



    ''' <summary>
    ''' 常駐フォームテスト
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_ResidentSoftware()
        Dim argTest As TorchServerManagementSystem.Main

        Dim args As String() = {"path"}

        argTest = New TorchServerManagementSystem.Main(args)

    End Sub

    ''' <summary>
    ''' 差分チェックテスト
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_Diff()
        Dim CheckTorchLog As New TorchServerManagementSystem.CheckTorchLog(settings)
        Call CheckTorchLog.Main()
    End Sub

    ''' <summary>
    ''' DiscordWebhookテスト
    ''' </summary>
    <TestMethod()> Public Sub TestMethod_DiscordWebhook()
        Dim whUrl As String = "https://discordapp.com/api/webhooks/718489082686341241/Jdkb1v9typyL44FZUBhY6bP1hv1-ZRy73jiDTHSDu3GlZZUF0CbKUI7xVoa3IgzRR_X2"

        TorchServerManagementSystem.Webhook.PostDiscord(whUrl, "TestText", settings.Webhook_Name)

    End Sub


End Class