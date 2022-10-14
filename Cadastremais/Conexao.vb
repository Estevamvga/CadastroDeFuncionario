Imports System.Data.Odbc

Module Conexao

    Public v_conexao As New OdbcConnection("DRIVER={MySQL ODBC 5.2 UNICODE Driver};server=192.168.0.151;database=sortee08_projetocli;option=3;port=3306;user=sortee_user_adm;password=80902019")
    'Public v_Conexao As New M("server=localhost; userid=root; passoword=; database=controle_acesso; port=;")
    Function AbreCone() As OdbcConnection
        Try
            If v_conexao.State = ConnectionState.Closed Then
                v_conexao.Open()
                'MessageBox.Show("Conexão Ok!", "A T E N C A O", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If

            Return v_conexao

        Catch ex As Exception
            MsgBox("Não consegui abrir a conexão" & vbCrLf & ex.Message)
        End Try
    End Function

    Sub fechaCone()
        Try
            If Not IsNothing(v_conexao) Then
                If v_conexao.State <> ConnectionState.Closed Then
                    v_conexao.Close()
                    'MessageBox.Show("Conexão fechada!", "A T E N C A O", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Não existe conexão aberta!", "A T E N C A O", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub
End Module
