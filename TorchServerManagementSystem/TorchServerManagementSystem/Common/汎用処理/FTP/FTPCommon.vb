Imports System
Public Class FTPCommon
    'FTPサーバーから送信されたステータスを表示
    '参考 https://docs.microsoft.com/ja-jp/dotnet/api/system.net.ftpstatuscode?view=netframework-4.7.2


    'CommonSettings
    Private ftpUserName As String = "stec_p01w"
    Private ftpPassword As String = "pr9wzxkj"
    Private ftpAddress As String = "ftp://202.250.41.219"

    Public ftpFileInfo As New ftpFileInfo

    ''' <summary>
    ''' FTP形式のパスから親フォルダのパスを抽出する
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetFtpFtpParentsFolderPath(ByVal path As String) As String
        Get
            Return path.Substring(0, path.LastIndexOf("/"))
        End Get
    End Property

    ''' <summary>
    ''' FTP形式のパスからファイル名を取得する
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetFtpFileName(ByVal path As String) As String
        Get
            Return path.Substring(path.LastIndexOf("/") + 1)
        End Get
    End Property

    ''' <summary>
    ''' FTP形式のパスを結合する
    ''' </summary>
    ''' <param name="folderPath"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    Private ReadOnly Property GetCombineFtpFilePath(ByVal folderPath As String, ByVal fileName As String) As String
        Get
            If Right(folderPath, 1) <> "/" Then
                folderPath = folderPath & "/"
            End If

            Return folderPath & fileName
        End Get
    End Property

    ''' <summary>
    ''' 指定パスに接続要求を行う
    ''' </summary>
    ''' <param name="uriPath">指定パス(フォルダ/ファイル)</param>
    ''' <param name="webRequestMethodsFtp">要求処理コード</param>
    ''' <param name="refFtpWebRequest">戻り値：FtpWebRequest</param>
    ''' <param name="refErrMessage">戻り値：Exception</param>
    ''' <returns></returns>
    Private Function GetFtpWebRequest(ByVal uriPath As String, ByVal webRequestMethodsFtp As String, ByRef refFtpWebRequest As Net.FtpWebRequest, ByRef refErrMessage As String) As Boolean
        Try
            'FTP接続先の設定
            Dim uri As New Uri(uriPath)
            'FTP接続リクエストの作成
            Dim ftpReq As Net.FtpWebRequest = CType(Net.WebRequest.Create(uri), Net.FtpWebRequest)
            'FTP接続ログインユーザー名とパスワードを設定
            ftpReq.Credentials = New Net.NetworkCredential(ftpUserName, ftpPassword)

            ftpReq.KeepAlive = False    '要求完了後に接続を閉じる
            ftpReq.UsePassive = True    'PASVモードを無効にする
            If webRequestMethodsFtp = Net.WebRequestMethods.Ftp.AppendFile OrElse
                webRequestMethodsFtp = Net.WebRequestMethods.Ftp.UploadFile OrElse
                webRequestMethodsFtp = Net.WebRequestMethods.Ftp.DownloadFile Then
                ftpReq.UseBinary = False     'ASCIIモードで転送する
            End If


            ftpReq.Method = webRequestMethodsFtp

            refFtpWebRequest = ftpReq
            Return True
        Catch ex As Exception
            refErrMessage = ex.ToString
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FTPへのアップロード処理を行う    
    ''' </summary>
    ''' <param name="ftpFolderPath">アップロード先のFTPフォルダパス</param>
    ''' <param name="uploadFilePath">アップロード対象のFTPファイルパス</param>
    ''' <param name="append">追記モード/上書きモード</param>
    ''' <param name="refReturnState">戻り値：FTP処理結果ステータス</param>
    ''' <param name="refErrMessage">戻り値：Exception</param>
    ''' <returns></returns>
    Public Function upload(ByVal ftpFolderPath As String, ByVal uploadFilePath As String, ByVal append As Boolean, ByRef refReturnState As String, ByRef refErrMessage As String) As Boolean
        Try
            'ファイルパス等の設定
            ftpFolderPath = ftpAddress & ftpFolderPath                                  'FTP上のファイルパス
            Dim uploadFileName As String = IO.Path.GetFileName(uploadFilePath)          'ファイル名
            Dim ftpFilePath As String = IO.Path.Combine(ftpFolderPath, uploadFileName)  'FTP上1ファイルパス

            'FTP処理を設定
            Dim ftpReq As Net.FtpWebRequest = Nothing
            Dim webRequestMethod As String = String.Empty
            If append Then
                webRequestMethod = Net.WebRequestMethods.Ftp.AppendFile
            Else
                webRequestMethod = Net.WebRequestMethods.Ftp.UploadFile
            End If
            If GetFtpWebRequest(ftpFilePath, webRequestMethod, ftpReq, refErrMessage) Then
                Return False
            End If

            '書き込み処理
            Using reqStream As IO.Stream = ftpReq.GetRequestStream
                Using fileStream As New IO.FileStream(uploadFilePath, IO.FileMode.Open, IO.FileAccess.Read)
                    Dim buffer(1023) As Byte
                    While True
                        Dim readSize As Integer = fileStream.Read(buffer, 0, buffer.Length)
                        If readSize = 0 Then Exit While
                        reqStream.Write(buffer, 0, readSize)
                    End While
                    fileStream.Close()
                    reqStream.Close()
                End Using
            End Using

            'FtpWebResponseを取得
            Using ftpres As Net.FtpWebResponse = CType(ftpReq.GetResponse, Net.FtpWebResponse)
                refReturnState = Replace(ftpres.StatusDescription, vbCrLf, "")
            End Using

            Return True
        Catch ex As Exception
            refErrMessage = ex.ToString
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FTPからダウンロード処理を行う
    ''' </summary>
    ''' <param name="ftpFilePath">ダウンロードするFTPファイルのパス</param>
    ''' <param name="downloadFolderPath">出力するフォルダのパス</param>
    ''' <param name="refReturnState">戻り値：FTP処理ステータス</param>
    ''' <param name="refErrMessage">戻り値：Exception</param>
    ''' <returns></returns>
    Public Function download(ByVal ftpFilePath As String, ByVal downloadFolderPath As String, ByRef refReturnState As String, ByRef refErrMessage As String) As Boolean
        Try
            'ファイルパスの設定
            ftpFilePath = ftpAddress & ftpFilePath
            Dim downloadFilePath As String = downloadFolderPath & GetFtpFileName(ftpFilePath)

            'FTP処理を設定
            Dim ftpReq As Net.FtpWebRequest = Nothing
            Dim webRequestMethod As String = Net.WebRequestMethods.Ftp.DownloadFile
            If GetFtpWebRequest(ftpFilePath, webRequestMethod, ftpReq, refErrMessage) Then
                Return False
            End If

            '読み取り処理
            Using ftpRes As Net.FtpWebResponse = CType(ftpReq.GetResponse, Net.FtpWebResponse)
                Using resStream As IO.Stream = ftpRes.GetResponseStream
                    Using fileStream As New IO.FileStream(downloadFilePath, IO.FileMode.Create)
                        While True
                            Dim buffer(1023) As Byte
                            Dim readSize As Integer = resStream.Read(buffer, 0, buffer.Length)
                            If readSize = 0 Then Exit While
                            fileStream.Write(buffer, 0, readSize)
                        End While
                        fileStream.Close()
                        resStream.Close()
                    End Using
                End Using

                refReturnState = Replace(ftpRes.StatusDescription, vbCrLf, "")
                ftpRes.Close()
            End Using
            Return True
        Catch ex As Exception
            refErrMessage = ex.ToString
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FTP上のファイルを削除する
    ''' </summary>
    ''' <param name="ftpFilePath"></param>
    ''' <param name="refErrMessage"></param>
    ''' <returns></returns>
    Public Function DeleteFile(ByVal ftpFilePath As String, ByRef refReturnState As String, ByVal refErrMessage As String) As Boolean
        Try
            'ファイルパスの設定
            ftpFilePath = ftpAddress & ftpFilePath

            'FTP処理を設定
            Dim ftpReq As Net.FtpWebRequest = Nothing
            Dim webRequestMethod As String = Net.WebRequestMethods.Ftp.DeleteFile
            If GetFtpWebRequest(ftpFilePath, webRequestMethod, ftpReq, refErrMessage) Then
                Return False
            End If

            '削除処理
            Using ftpRes As Net.FtpWebResponse = CType(ftpReq.GetResponse, Net.FtpWebResponse)
                Dim resCord As String = ftpRes.StatusCode.ToString
                Dim statusDescription As String = ftpRes.StatusDescription
                refReturnState = Replace(statusDescription, vbCrLf, "")
            End Using
            Return True
        Catch ex As Exception
            refErrMessage = ex.ToString
            Return False
        End Try
    End Function

    ''' <summary>
    ''' FTP上のフォルダに有るファイル情報を取得(加工はFtpInfoにて一行ずつおこなう)
    ''' </summary>
    ''' <param name="ftpFolder"></param>
    ''' <param name="refFilesInfoList"></param>
    ''' <param name="refErrMessage"></param>
    ''' <returns></returns>
    Public Function GetFtpDirectoryFilesInfo(ByVal ftpFolder As String, ByRef refFilesInfoList As List(Of String), ByRef refErrMessage As String) As Boolean
        Try
            ftpFolder = ftpAddress & ftpFolder

            'FTP処理を設定
            Dim ftpReq As Net.FtpWebRequest = Nothing
            Dim webRequestMethod As String = Net.WebRequestMethods.Ftp.ListDirectoryDetails
            If GetFtpWebRequest(ftpFolder, webRequestMethod, ftpReq, refErrMessage) Then
                Return False
            End If

            Dim ftpRes As Net.FtpWebResponse = CType(ftpReq.GetResponse, Net.FtpWebResponse)
            Using streamReader As New IO.StreamReader(ftpRes.GetResponseStream)
                Dim res As String = streamReader.ReadToEnd
                Dim resCord As String = ftpRes.StatusCode.ToString
                Dim statusDescription As String = ftpRes.StatusDescription
                streamReader.Close()
                ftpRes.Close()

                Dim fileInfos As String() = res.Split(CType(vbCrLf, Char()))

                For Each fileInfo As String In fileInfos
                    If Not (fileInfo = "" OrElse fileInfo = vbLf OrElse Replace(fileInfo, ".", "") = "") Then
                        refFilesInfoList.Add(fileInfo)
                    End If
                Next
            End Using
            Return True
        Catch ex As Exception
            refErrMessage = ex.ToString
            Return False
        End Try
    End Function

End Class

Public Class ftpFileInfo
    ''' <summary>
    ''' Infoからファイル名を抽出する
    ''' </summary>
    ''' <param name="info"></param>
    ''' <returns></returns>
    Public ReadOnly Property GetFileName(ByVal info As String) As String
        Get
            Return info.Substring(55)
        End Get
    End Property

    ''' <summary>
    ''' Infoからファイルサイズを抽出する
    ''' </summary>
    ''' <param name="info"></param>
    ''' <returns></returns>
    Public ReadOnly Property GetFileSize(ByVal info As String) As Double
        Get
            Dim tmp As String = Split(Text.RegularExpressions.Regex.Replace(info, " +", ","), ",")(4)
            If IsNumeric(tmp) Then
                Return CDbl(tmp)
            Else
                Return -1
            End If
        End Get
    End Property
End Class


