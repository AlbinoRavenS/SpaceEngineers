Imports System.Xml.Linq

''' <summary>
''' XML形式のファイルを処理する
''' </summary>
Public Class XMLCommon

    ''' <summary>
    ''' 処理するパス
    ''' </summary>
    ''' <returns></returns>
    Private Property path As String

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="path"></param>
    Public Sub New(ByVal path As String)
        Me.path = path
    End Sub

    ''' <summary>
    ''' 要素の値をリストで出力する
    ''' </summary>
    ''' <param name="elementList"></param>
    ''' <returns></returns>
    Public Function GetElementContent(ByVal elementList As List(Of String)) As List(Of String)
        Dim refList As New List(Of String)
        Try
            '読み込み
            Dim xmlDoc As New Xml.XmlDocument
            xmlDoc.Load(path)

            '対象ノードの取得
            Dim nodeStr As String = String.Join("/", elementList.ToArray)
            Dim nodes As Xml.XmlNodeList = xmlDoc.SelectNodes(nodeStr)

            '要素の値を取得
            For Each node As Xml.XmlNode In nodes
                refList.Add(node.InnerText)
            Next

            Return refList
        Catch ex As Exception
            Throw
        End Try
    End Function
End Class
