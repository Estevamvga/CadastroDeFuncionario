Imports System.Data.Odbc

Public Class CadLogin

    Public v_conexao As New OdbcConnection

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click


        'VALIDAR O USUARIO CADASTRO DE USUARIO
        If txtLogin.Text.Trim <> String.Empty AndAlso txtSenha.Text.Trim <> String.Empty Then
            Try
                v_conexao = AbreCone()
                Dim cmdUSU As OdbcCommand
                Dim readerUSU As OdbcDataReader
                Dim sqlUSU As String
                sqlUSU = " SELECT * FROM usuario WHERE usuario= '" & txtLogin.Text & "' "
                cmdUSU = New OdbcCommand(sqlUSU, v_conexao)
                readerUSU = cmdUSU.ExecuteReader()

                If readerUSU.Read = True Then
                    readerUSU.Close()
                    MessageBox.Show("Usuario já cadastrado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtLogin.Text = ""
                    txtLogin.Focus()
                    Exit Sub
                End If

                'VALIDAR O SENHA CADASTRO DE USUARIO
                v_conexao = AbreCone()
                Dim cmdSENHA As OdbcCommand
                Dim readerSenha As OdbcDataReader
                Dim sqlUSER As String
                sqlUSER = " SELECT * FROM usuario WHERE usuario= '" & txtSenha.Text & "' " '
                cmdSENHA = New OdbcCommand(sqlUSER, v_conexao)
                readerSenha = cmdSENHA.ExecuteReader()
                If readerSenha.Read = True Then
                    readerSenha.Close()
                    MessageBox.Show("Usuário já cadastrado.", "Atenção")
                    txtSenha.Text = ""
                    txtSenha.Focus()
                    Exit Sub
                End If

                'VALIDAR NOME USUARIO CADASTRO DE USUARIO
                v_conexao = AbreCone()
                Dim cmdNOME As OdbcCommand
                Dim readerNOME As OdbcDataReader
                Dim sqlNOME As String
                sqlNOME = " SELECT * FROM usuario WHERE usuario= '" & txtSenha.Text & "' " '
                cmdNOME = New OdbcCommand(sqlNOME, v_conexao)
                readerNOME = cmdNOME.ExecuteReader()
                If readerNOME.Read = True Then
                    readerNOME.Close()
                    MessageBox.Show("Usuário já cadastrado.", "Atenção")
                    txtNomeUsu.Text = ""
                    txtNomeUsu.Focus()
                    Exit Sub
                End If

                'CADASTRAR USUARIO INSERT
                v_conexao = AbreCone()
                Dim cmd As OdbcCommand
                Dim sql As String
                sql = "INSERT INTO usuario (nome_usuario, usuario, senha) VALUES ('" & txtNomeUsu.Text & "','" & txtLogin.Text & "', '" & txtSenha.Text & "') "
                cmd = New OdbcCommand(sql, v_conexao)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Usuário Cadastrado com sucesso!!!", "Confirmação!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Login.Show()
                Me.Close()
            Catch ex As Exception
                MsgBox("Erro ao salvar", ex.Message)
            End Try
        Else
            txtNomeUsu.Text = ""
            txtLogin.Text = ""
            txtSenha.Text = ""
            txtNomeUsu.Focus()
            MessageBox.Show("Dados invalidos!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            MessageBox.Show("Dados invalidos!!!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Stop)
            Exit Sub
        End If
    End Sub

    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs)
        Me.Close()
    End Sub

End Class