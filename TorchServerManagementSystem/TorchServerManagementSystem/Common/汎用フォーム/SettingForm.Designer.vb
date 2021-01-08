<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SettingForm
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Windows フォーム デザイナーで必要です。
    Private components As System.ComponentModel.IContainer

    'メモ: 以下のプロシージャは Windows フォーム デザイナーで必要です。
    'Windows フォーム デザイナーを使用して変更できます。  
    'コード エディターを使って変更しないでください。
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox_TorchFolderPath = New System.Windows.Forms.TextBox()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.TextBox_Webhook_Name = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.TextBox_Webhook_Url = New System.Windows.Forms.TextBox()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.設定ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.CheckBox_isAutoShutdown = New System.Windows.Forms.CheckBox()
        Me.NumericUpDown_AutoShutodwnInterval = New System.Windows.Forms.NumericUpDown()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.MenuStrip1.SuspendLayout()
        CType(Me.NumericUpDown_AutoShutodwnInterval, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'TabControl1
        '
        Me.TabControl1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Location = New System.Drawing.Point(12, 27)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(528, 189)
        Me.TabControl1.TabIndex = 2
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.NumericUpDown_AutoShutodwnInterval)
        Me.TabPage1.Controls.Add(Me.CheckBox_isAutoShutdown)
        Me.TabPage1.Controls.Add(Me.Label1)
        Me.TabPage1.Controls.Add(Me.Label3)
        Me.TabPage1.Controls.Add(Me.TextBox_TorchFolderPath)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(520, 163)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "基本設定"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(3, 16)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(88, 12)
        Me.Label3.TabIndex = 2
        Me.Label3.Text = "Torchフォルダパス"
        '
        'TextBox_TorchFolderPath
        '
        Me.TextBox_TorchFolderPath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_TorchFolderPath.Location = New System.Drawing.Point(5, 31)
        Me.TextBox_TorchFolderPath.Name = "TextBox_TorchFolderPath"
        Me.TextBox_TorchFolderPath.Size = New System.Drawing.Size(506, 19)
        Me.TextBox_TorchFolderPath.TabIndex = 3
        Me.TextBox_TorchFolderPath.Text = "empty"
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.Label4)
        Me.TabPage2.Controls.Add(Me.TextBox_Webhook_Name)
        Me.TabPage2.Controls.Add(Me.Label7)
        Me.TabPage2.Controls.Add(Me.TextBox_Webhook_Url)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(520, 163)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Webhook設定"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(6, 40)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(83, 12)
        Me.Label4.TabIndex = 10
        Me.Label4.Text = "Webhook_Name"
        '
        'TextBox_Webhook_Name
        '
        Me.TextBox_Webhook_Name.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Webhook_Name.Location = New System.Drawing.Point(8, 55)
        Me.TextBox_Webhook_Name.Name = "TextBox_Webhook_Name"
        Me.TextBox_Webhook_Name.Size = New System.Drawing.Size(506, 19)
        Me.TextBox_Webhook_Name.TabIndex = 11
        Me.TextBox_Webhook_Name.Text = "empty"
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(6, 3)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(69, 12)
        Me.Label7.TabIndex = 8
        Me.Label7.Text = "Webhook_Url"
        '
        'TextBox_Webhook_Url
        '
        Me.TextBox_Webhook_Url.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TextBox_Webhook_Url.Location = New System.Drawing.Point(8, 18)
        Me.TextBox_Webhook_Url.Name = "TextBox_Webhook_Url"
        Me.TextBox_Webhook_Url.Size = New System.Drawing.Size(506, 19)
        Me.TextBox_Webhook_Url.TabIndex = 9
        Me.TextBox_Webhook_Url.Text = "empty"
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.設定ToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(552, 24)
        Me.MenuStrip1.TabIndex = 3
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        '設定ToolStripMenuItem
        '
        Me.設定ToolStripMenuItem.Name = "設定ToolStripMenuItem"
        Me.設定ToolStripMenuItem.Size = New System.Drawing.Size(43, 20)
        Me.設定ToolStripMenuItem.Text = "設定"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Button1.Location = New System.Drawing.Point(384, 222)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 4
        Me.Button1.Text = "確定"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(465, 222)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(75, 23)
        Me.Button2.TabIndex = 5
        Me.Button2.Text = "クリア"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'CheckBox_isAutoShutdown
        '
        Me.CheckBox_isAutoShutdown.AutoSize = True
        Me.CheckBox_isAutoShutdown.Location = New System.Drawing.Point(5, 56)
        Me.CheckBox_isAutoShutdown.Name = "CheckBox_isAutoShutdown"
        Me.CheckBox_isAutoShutdown.Size = New System.Drawing.Size(314, 16)
        Me.CheckBox_isAutoShutdown.TabIndex = 4
        Me.CheckBox_isAutoShutdown.Text = "サーバーログに動きが無い場合に仮想PCをシャットダウンするか"
        Me.CheckBox_isAutoShutdown.UseVisualStyleBackColor = True
        '
        'NumericUpDown_AutoShutodwnInterval
        '
        Me.NumericUpDown_AutoShutodwnInterval.Location = New System.Drawing.Point(82, 75)
        Me.NumericUpDown_AutoShutodwnInterval.Name = "NumericUpDown_AutoShutodwnInterval"
        Me.NumericUpDown_AutoShutodwnInterval.Size = New System.Drawing.Size(120, 19)
        Me.NumericUpDown_AutoShutodwnInterval.TabIndex = 5
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(3, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(73, 12)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "停止時間(分)"
        '
        'SettingForm
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 257)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "SettingForm"
        Me.Text = "SettingForm"
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage2.PerformLayout()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        CType(Me.NumericUpDown_AutoShutodwnInterval, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents 設定ToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Button1 As Button
    Friend WithEvents Button2 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox_TorchFolderPath As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents TextBox_Webhook_Url As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents TextBox_Webhook_Name As TextBox
    Friend WithEvents NumericUpDown_AutoShutodwnInterval As NumericUpDown
    Friend WithEvents CheckBox_isAutoShutdown As CheckBox
    Friend WithEvents Label1 As Label
End Class
