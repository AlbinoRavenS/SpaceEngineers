'''<summary>
'''インスタンスとXMLファイルの変換
'''</summary>
Public Class Setting

    '''<summary>
    '''設定ファイル読込
    '''</summary>
    '''<typeparam name="T">データ型</typeparam>
    '''<param name="result">データ</param>
    '''<param name="path">ファイルパス</param>
    Public Shared Sub Load(Of T)(ByRef result As T, ByVal path As String)
        Try
            If IO.File.Exists(path) Then
                Dim text As String = IO.File.ReadAllText(path, XMLSerializer.DEFAULT_ENCODING)
                XMLSerializer.Deserialize(text, result)
            End If
        Catch ex As Exception
            Throw
        End Try
    End Sub

    '''<summary>
    '''設定ファイル書込
    '''</summary>
    '''<typeparam name="T">データ型</typeparam>
    '''<param name="data">データ</param>
    '''<param name="path">ファイルパス</param>
    Public Shared Sub Save(Of T)(ByVal data As T, ByVal path As String)
        Try
            Dim text As String = String.Empty
            XMLSerializer.Serialize(data, text)
            IO.File.WriteAllText(path, text, XMLSerializer.DEFAULT_ENCODING)
        Catch ex As Exception
            Throw
        End Try
    End Sub
End Class
