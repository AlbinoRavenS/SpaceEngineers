'''<summary>
'''デシリアライズ用　型バインダ
'''</summary>
'''<remarks>
'''動的参照されたアセンブリのシリアル化を可能にする（Serializerバグ対策）
'''</remarks>
Friend Class XMLDeserializationBinder
    Inherits System.Runtime.Serialization.SerializationBinder

    '''<summary>
    '''型バインド
    '''</summary>
    '''<param name="assemblyName">アセンブリ名</param>
    '''<param name="typeName">型名</param>
    '''<returns>型オブジェクト</returns>
    '''<remarks></remarks>
    Public Overrides Function BindToType(ByVal assemblyName As String, ByVal typeName As String) As System.Type
        Try
            Dim result As Type

            '動的参照を含む、ロード済みアセンブリの検索
            result = Nothing
            For Each asm As Reflection.Assembly In My.Application.Info.LoadedAssemblies
                If asm.GetName.Name = assemblyName Then
                    'アセンブリ名が一致する場合、型名で型を取得
                    result = asm.GetType(typeName) '失敗の場合、Nothing
                    If result IsNot Nothing Then Exit For '型取得に成功した場合、検索を抜ける
                End If
            Next

            Return result
        Catch ex As Exception
            Throw
        End Try
    End Function


End Class
