''' <summary>
''' マルチスレッドの実装を容易にするクラス
''' 実装例は単体テストのコード参照
''' </summary>
Public Class ThreadingClassTemplate
#Region "NoChange(変数 プロパティ イベント)"

    '実行中スレッド数
    Private _runningThreadCount As Integer
    '処理スレッド数(最終的に実行したい処理の数)
    Private _TotalThreadCount As Integer
    'スレッド実行数
    Private _ExecutedThreadCount As Integer
    '同時実行可能スレッド数
    Private _ThreadMax As Integer

    ''' <summary>
    ''' 実行中スレッド数
    ''' </summary>
    ''' <returns></returns>
    Public Property runningThreadCount() As Integer
        Get
            Return _runningThreadCount
        End Get
        Private Set(ByVal value As Integer)
            _runningThreadCount = value
        End Set
    End Property
    ''' <summary>
    ''' 処理スレッド数(最終的に実行したい処理の数)
    ''' </summary>
    ''' <returns></returns>
    Public Property TotalThreadCount() As Integer
        Get
            Return _TotalThreadCount
        End Get
        Private Set(ByVal value As Integer)
            _TotalThreadCount = value
        End Set
    End Property
    ''' <summary>
    ''' スレッド実行数
    ''' </summary>
    ''' <returns></returns>
    Public Property ExecutedThreadCount() As Integer
        Get
            Return _ExecutedThreadCount
        End Get
        Set(ByVal value As Integer)
            _ExecutedThreadCount = value
        End Set
    End Property
    ''' <summary>
    ''' 同時実行可能スレッド数
    ''' </summary>
    ''' <returns></returns>
    Public Property ThreadMax() As Integer
        Get
            Return _ThreadMax
        End Get
        Private Set(ByVal value As Integer)
            _ThreadMax = value
        End Set
    End Property

    'イベント

    ''' <summary>
    ''' 予期せぬエラーが発生した場合に発生するイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="ex"></param>
    Public Event ThreadException(ByVal sender As Object, ByVal ex As Exception)

    ''' <summary>
    ''' 一つのスレッド完了が完了した際に発生するイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="refData"></param>
    Public Event ThreadCompleatEvent(ByVal sender As Object, ByVal refData As List(Of String))

    ''' <summary>
    ''' TotalThreadCountで設定したスレッド数が完了した際に発生するイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="refData"></param>
    Public Event ThreadAllCompleatEvent(ByVal sender As Object, ByVal refData As List(Of String))

    'キャンセル処理用
    Private isCancel As Boolean

    '全スレッドの終了を待機中
    Private isWaitingAllThread As Boolean
#End Region

#Region "NoChange(メソッド)"
    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="totalThreadCount">完了とする最大実行スレッド数</param>
    ''' <param name="threadMax">同時実行を受け付ける数:推奨:最大4</param>
    Public Sub New(ByVal totalThreadCount As Integer, ByVal threadMax As Integer)
        Try
            Me.TotalThreadCount = totalThreadCount
            Me.ThreadMax = threadMax
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' スレッド実行処理（変更不要） 
    ''' </summary>
    Public Sub RunThread()
        Try
            '実行中が上限値の場合は待機
            Do While ThreadMax < runningThreadCount
                If isCancel Then Return
            Loop

            'スレッド作成
            Dim thread As New Threading.Thread(New Threading.ThreadStart(AddressOf ThreadingWorkCaller))
            thread.IsBackground = True

            '処理の実行を待機する
            Using wait As Threading.EventWaitHandle = New Threading.EventWaitHandle(False, Threading.EventResetMode.AutoReset, "waitThreadControl")
                thread.Start()
                wait.WaitOne()
            End Using
        Catch ex As Exception
            Throw
        End Try
    End Sub

    ''' <summary>
    ''' スレッドで実行する処理の基本部分
    ''' </summary>
    Private Sub ThreadingWorkCaller()
        '実行中カウントを加算する
        runningThreadCount += 1

        'イベント戻り値（必要に応じて型を変更）
        Dim refList As New List(Of String)

        Try
            '処理を実行
            refList = ThreadingWork()
        Catch ex As Exception
            'エラー時はイベントで返す
            RaiseEvent ThreadException(Me, ex)
        End Try

        '実行中カウントを減算
        runningThreadCount -= 1

        'スレッド完了イベントを送る
        RaiseEvent ThreadCompleatEvent(Me, refList)

        'スレッド完了済みカウントを加算
        ExecutedThreadCount += 1
        '全スレッド完了していた場合全スレッド完了イベントを送る
        If ExecutedThreadCount = TotalThreadCount Then
            RaiseEvent ThreadAllCompleatEvent(Me, refList)

            '全スレッド完了待ちの場合は完了待機を解除する
            If isWaitingAllThread Then
                '変数受け取り後、メインに処理を返す
                Using wait As Threading.EventWaitHandle = Threading.EventWaitHandle.OpenExisting("waitAllThread")
                    wait.Set()
                End Using
            End If
        End If

    End Sub

    ''' <summary>
    ''' キャンセル処理の実装
    ''' </summary>
    Public Sub Cancel()
        isCancel = True
    End Sub

    ''' <summary>
    ''' 全スレッドの完了を待機する。
    ''' </summary>
    Public Sub ThreadJoin()
        isWaitingAllThread = True
        '処理の実行を待機する
        Using wait As Threading.EventWaitHandle = New Threading.EventWaitHandle(False, Threading.EventResetMode.AutoReset, "waitAllThread")
            wait.WaitOne()
        End Using
    End Sub

#End Region

    '実行処理用変数
    Public Property hoge1 As String
    Public Property hoge2 As String

    Private Function ThreadingWork() As List(Of String)
        Try
            '適宜キャンセル処理を差し込む事
            'if isCancel then return

            '必ずスレッド内で変数の値を固定する(別スレッド実行時に変数が変わるため)
            Dim hoge1 As String = Me.hoge1
            Dim hoge2 As String = Me.hoge2

            '変数固定を待ってから次のスレッドの実行を許可する
            Using wait As Threading.EventWaitHandle = Threading.EventWaitHandle.OpenExisting("waitThreadControl")
                wait.Set()
            End Using

            '/////////////////////////////
            '/////////メイン処理//////////
            '/////////////////////////////
            Dim refList As New List(Of String)

            'テストでエラーを出す
            If hoge1 = "50" Then
                Throw New Exception
            End If

            'ランダムに処理を待機し結果を出力するテストコード
            Dim rnd As Integer = New Random().Next(1, 1000)
            Threading.Thread.Sleep(rnd)

            refList.Add(hoge1 & " : " & rnd & " : " & Now.ToString("HHmmss.fff"))
            Return refList
            '/////////////////////////////////
            '/////////メイン処理終了//////////
            '/////////////////////////////////
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
