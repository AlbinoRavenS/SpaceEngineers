''' <summary>
''' CSV処理
''' </summary>
Public Class CSV
    Inherits List(Of List(Of String))

    ''' <summary>
    ''' 区切り文字
    ''' </summary>
    Public Enum DELIMITER
        wQuotationComma
        comma
        tab
    End Enum

    '''<summary>
    '''デフォルト文字エンコード
    '''</summary>
    Public Shared ReadOnly DEFAULT_ENCODING As Text.Encoding = Text.Encoding.GetEncoding("Shift_JIS")

    '''<summary>
    '''トリム対象
    '''</summary>
    Private Const _TRIMMER As String = """"

    ''' <summary>
    ''' 区切り文字
    ''' </summary>
    Private _DELIMITER As String

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="Delimiter">区切り文字</param>
    Public Sub New(Optional ByVal Delimiter As DELIMITER = DELIMITER.wQuotationComma)

        '区切り文字を設定
        Select Case Delimiter
            Case DELIMITER.wQuotationComma
                _DELIMITER = ""","""
            Case DELIMITER.comma
                _DELIMITER = ","
            Case DELIMITER.tab
                _DELIMITER = vbTab

        End Select

    End Sub

#Region "入出力処理"

    '''<summary>
    '''読込
    '''</summary>
    '''<param name="path">ファイルパス</param>
    '''<returns></returns>
    Public Function Load(ByVal path As String) As Boolean
        Try
            Dim text As String

            Me.Clear()
            text = IO.File.ReadAllText(path, System.Text.Encoding.Default)
            For Each line As String In Split(text, vbCrLf)
                If line = String.Empty Then Continue For

                Dim array As String()
                Dim list As New List(Of String)

                If line.StartsWith(_TRIMMER) Then line = line.Substring(1, line.Length - 1)
                If line.EndsWith(_TRIMMER) Then line = line.Substring(0, line.Length - 1)
                array = Split(line, _DELIMITER)
                list.AddRange(array)

                Me.Add(list)
            Next
            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <param name="path">ファイルパス</param>
    ''' <returns></returns>
    Public Function Save(ByVal path As String) As Boolean

        Try
            '行データ
            Dim rows As New List(Of String)
            '行を作成
            For Each row As List(Of String) In Me
                rows.Add(_TRIMMER & String.Join(_DELIMITER, row.ToArray) & _TRIMMER)
            Next

            '行を結合
            Dim writeStr As String = String.Join(vbCrLf, rows.ToArray)

            '書き出し
            IO.File.WriteAllText(path, writeStr, System.Text.Encoding.Default)
            Return True

        Catch ex As Exception
            Throw
        End Try
    End Function

    '''<summary>
    '''追記
    '''</summary>
    '''<param name="path">ファイルパス</param>
    '''<param name="data">データ</param>
    '''<returns></returns>
    Public Function Append(ByVal path As String, ByVal data As List(Of String)) As Boolean
        Try
            Dim text As String
            '文字列生成
            text = _TRIMMER
            For i As Integer = 0 To data.Count - 1
                text += data(i)
                If i < data.Count - 1 Then text += _DELIMITER
            Next
            text += _TRIMMER + vbCrLf

            '追記
            IO.File.AppendAllText(path, text, DEFAULT_ENCODING)

            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    '''<summary>
    '''セルデータ
    '''</summary>
    '''<param name="row">0から始まる行番号</param>
    '''<param name="column">0から始まる列番号</param>
    '''<returns></returns>
    Default Public Shadows ReadOnly Property item(ByVal row As Integer, ByVal column As Integer) As String
        Get
            Try
                '行列取得できない場合は空白を返す
                If row < MyBase.Count OrElse
                    column < MyBase.Item(row).Count Then
                    Return String.Empty
                End If

                Return MyBase.Item(row)(column)
            Catch ex As Exception
                Return String.Empty
            End Try
        End Get
    End Property

#End Region

#Region "検索処理"

    ''' <summary>
    ''' アイテムを完全一致で検索し、行を取得する
    ''' </summary>
    ''' <param name="searchStr"></param>
    ''' <param name="serachColumnIndex"></param>
    ''' <returns></returns>
    Public Overloads Function GetSearchRowFirst(ByVal searchStr As String, ByVal serachColumnIndex As Integer) As List(Of String)
        Try
            For Each item As List(Of String) In Me
                If item(serachColumnIndex) = searchStr Then
                    Return item
                End If
            Next
            Return New List(Of String)
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' アイテムを完全一致で検索し、指定列の値を取得する
    ''' </summary>
    ''' <param name="searchStr"></param>
    ''' <param name="serachColumnIndex"></param>
    ''' <param name="getRowIndex"></param>
    ''' <returns></returns>
    Public Overloads Function GetSearchRowFirst(ByVal searchStr As String, ByVal serachColumnIndex As Integer, ByVal getRowIndex As Integer) As String
        Try
            For Each item As List(Of String) In Me
                If item(serachColumnIndex) = searchStr Then
                    Return item(getRowIndex)
                End If
            Next

            Return String.Empty
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' アイテムを完全一致で検索し、全ての行を取得する
    ''' </summary>
    ''' <param name="searchStr"></param>
    ''' <param name="serachColumnIndex"></param>
    ''' <returns></returns>
    Public Function GetSearchRowAll(ByVal searchStr As String, ByVal serachColumnIndex As Integer) As List(Of List(Of String))
        Try
            Dim list As New List(Of List(Of String))
            For Each item As List(Of String) In Me
                If item(serachColumnIndex) = searchStr Then
                    list.Add(item)
                End If
            Next

            Return list
        Catch ex As Exception
            Throw
        End Try
    End Function

#End Region

End Class
