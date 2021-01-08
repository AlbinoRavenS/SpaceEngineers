''' <summary>
''' シリアライズ関連処理クラス
''' </summary>
''' <remarks></remarks>
Friend Class XMLSerializer

    ''' <summary>
    ''' 文字コード　MemoryStream仕様による標準
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly DEFAULT_ENCODING As System.Text.Encoding = System.Text.Encoding.UTF8

    ''' <summary>
    ''' シリアライズ
    ''' </summary>
    ''' <typeparam name="T">オブジェクトの型</typeparam>
    ''' <param name="obj">オブジェクト</param>
    ''' <param name="result">シリアル化テキスト</param>
    ''' <returns>実行結果</returns>
    ''' <remarks></remarks>
    Public Shared Function Serialize(Of T)(ByVal obj As T, ByRef result As String) As Boolean
        Try

            Dim formatter As System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            Dim buffer As Byte()

            'Nothingシリアル化対応
            If obj Is Nothing Then
                result = String.Empty
                Return True
            End If

            'カスタムフォーマッタ取得
            formatter = Nothing
            If Not GetCustomFormatter(formatter) Then Return False

            'バイト配列にシリアライズ
            Using stream As New System.IO.MemoryStream()

                formatter.Serialize(stream, obj) 'メモリ上にシリアライズ
                buffer = stream.ToArray 'バイト配列に格納

            End Using

            'テキスト取得
            result = System.Text.Encoding.GetEncoding(DEFAULT_ENCODING.CodePage).GetString(buffer)

            Return True

        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''デシリアライズ
    ''' </summary>
    ''' <typeparam name="T">オブジェクトの型</typeparam>
    ''' <param name="text">シリアル化テキスト</param>
    ''' <param name="result">オブジェクト</param>
    ''' <returns>実行結果</returns>
    ''' <remarks></remarks>
    Public Shared Function Deserialize(Of T)(ByVal text As String, ByRef result As T) As Boolean
        Try
            Dim formatter As System.Runtime.Serialization.Formatters.Soap.SoapFormatter
            Dim buffer As Byte()

            'Nothingシリアル化対応
            If text = String.Empty Then
                result = Nothing
                Return True
            End If

            'テキストをバイト配列に変換
            buffer = System.Text.Encoding.GetEncoding(DEFAULT_ENCODING.CodePage).GetBytes(text.ToCharArray)

            'カスタムフォーマッタ取得
            formatter = Nothing
            If Not GetCustomFormatter(formatter) Then Return False

            'バイト配列からデシリアライズ
            Using stream As New System.IO.MemoryStream(buffer)
                result = CType(formatter.Deserialize(stream), T)
            End Using


            Return True
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    '''シリアル化カスタムフォーマッタ取得
    ''' </summary>
    ''' <returns>フォーマッタ</returns>
    ''' <remarks></remarks>
    Private Shared Function GetCustomFormatter(ByRef result As System.Runtime.Serialization.Formatters.Soap.SoapFormatter) As Boolean
        result = New System.Runtime.Serialization.Formatters.Soap.SoapFormatter
        result.Binder = New XMLDeserializationBinder()
        result.FilterLevel = System.Runtime.Serialization.Formatters.TypeFilterLevel.Full 'すべてのメンバをシリアライズ対象とする
        result.AssemblyFormat = System.Runtime.Serialization.Formatters.FormatterAssemblyStyle.Simple 'デシリアライズ時にアセンブリバージョンの一致を必須としない

        Return True
    End Function

End Class
