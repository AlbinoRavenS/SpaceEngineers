''' <summary>
''' 設定の初期化処理を担当する
''' </summary>
Public Class SettingBase

    ''' <summary>
    ''' 初期値がNothingの場合値を入れる(Nothing=Trueを返す)
    ''' </summary>
    ''' <param name="var"></param>
    ''' <param name="str"></param>
    Public Overloads Function SetInitData(ByRef var As String, ByVal str As String) As Boolean
        If var Is Nothing Then
            var = str
            Return True
        End If
        Return False
    End Function

    ''' <summary>
    ''' 初期値が0の場合値を入れる(0=Trueを返す)
    ''' </summary>
    ''' <param name="var"></param>
    ''' <param name="str"></param>
    Public Overloads Function SetInitData(ByRef var As Integer, ByVal str As Integer) As Boolean
        If var = 0 Then
            var = str
            Return True
        End If
        Return False
    End Function

    '''<summary>
    '''ディレクトリを作成する(作成時Trueを返す)
    '''</summary>
    '''<param name="path"></param>
    '''<returns></returns>
    Public Function CreateDirectory(ByVal path As String) As Boolean
        Try
            If Not IO.Directory.Exists(path) Then
                IO.Directory.CreateDirectory(path)
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

    ''' <summary>
    ''' 空ファイルを作成(作成時Trueを返す)
    ''' </summary>
    ''' <param name="path"></param>
    ''' <returns></returns>
    Public Function CreateEmptyfile(ByVal path As String) As Boolean
        Try
            If Not IO.File.Exists(path) Then
                IO.File.Create(path)
                Return True
            End If
            Return False
        Catch ex As Exception
            Throw
        End Try
    End Function

End Class
