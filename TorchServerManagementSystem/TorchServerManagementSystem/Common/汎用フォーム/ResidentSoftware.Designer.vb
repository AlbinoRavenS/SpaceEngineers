<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class ResidentSoftware
    Inherits System.Windows.Forms.Form

    'フォームがコンポーネントの一覧をクリーンアップするために dispose をオーバーライドします。
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ResidentSoftware))
        Me.Timer_Main = New System.Windows.Forms.Timer(Me.components)
        Me.ContextMenuStrip_Taskbar = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.ExitToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.SettingToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem()
        Me.NotifyIcon_Taskbar = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.Timer_ExitCheck = New System.Windows.Forms.Timer(Me.components)
        Me.Timer_AutoShutdown = New System.Windows.Forms.Timer(Me.components)
        Me.Label1 = New System.Windows.Forms.Label()
        Me.ContextMenuStrip_Taskbar.SuspendLayout()
        Me.SuspendLayout()
        '
        'Timer_Main
        '
        '
        'ContextMenuStrip_Taskbar
        '
        Me.ContextMenuStrip_Taskbar.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ExitToolStripMenuItem, Me.SettingToolStripMenuItem})
        Me.ContextMenuStrip_Taskbar.Name = "ContextMenuStrip_Taskbar"
        Me.ContextMenuStrip_Taskbar.Size = New System.Drawing.Size(112, 48)
        '
        'ExitToolStripMenuItem
        '
        Me.ExitToolStripMenuItem.Name = "ExitToolStripMenuItem"
        Me.ExitToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.ExitToolStripMenuItem.Text = "Exit"
        '
        'SettingToolStripMenuItem
        '
        Me.SettingToolStripMenuItem.Name = "SettingToolStripMenuItem"
        Me.SettingToolStripMenuItem.Size = New System.Drawing.Size(111, 22)
        Me.SettingToolStripMenuItem.Text = "Setting"
        '
        'NotifyIcon_Taskbar
        '
        Me.NotifyIcon_Taskbar.Icon = CType(resources.GetObject("NotifyIcon_Taskbar.Icon"), System.Drawing.Icon)
        Me.NotifyIcon_Taskbar.Text = "NotifyIcon1"
        Me.NotifyIcon_Taskbar.Visible = True
        '
        'Timer_ExitCheck
        '
        '
        'Timer_AutoShutdown
        '
        Me.Timer_AutoShutdown.Interval = 1000
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(38, 12)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Label1"
        '
        'ResidentSoftware
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 12.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(284, 261)
        Me.Controls.Add(Me.Label1)
        Me.Name = "ResidentSoftware"
        Me.Text = "ResidentSoftware"
        Me.ContextMenuStrip_Taskbar.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Timer_Main As Windows.Forms.Timer
    Friend WithEvents ContextMenuStrip_Taskbar As Windows.Forms.ContextMenuStrip
    Friend WithEvents NotifyIcon_Taskbar As Windows.Forms.NotifyIcon
    Friend WithEvents ExitToolStripMenuItem As Windows.Forms.ToolStripMenuItem
    Friend WithEvents Timer_ExitCheck As Windows.Forms.Timer
    Friend WithEvents SettingToolStripMenuItem As ToolStripMenuItem
    Friend WithEvents Timer_AutoShutdown As Timer
    Friend WithEvents Label1 As Label
End Class
