Imports System.Data.Odbc

Public Class Login

    Public v_conexao As New OdbcConnection

    Dim nomePorteiro As String
    Dim nomeUsuario As String


    Sub login()
        If txtUsuario.Text = "" Then
            MessageBox.Show("Dados invalidos!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtUsuario.Focus()
        ElseIf txtSenha.Text = "" Then
            MessageBox.Show("Dados invalidas!", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtSenha.Focus()
        Else
            Try
                v_conexao = AbreCone()
                Dim cmd As OdbcCommand
                Dim reader As OdbcDataReader
                Dim sql As String
                sql = "SELECT * FROM usuario where usuario = '" & txtUsuario.Text & "' and senha = '" & txtSenha.Text & "' "
                cmd = New OdbcCommand(sql, v_conexao)
                reader = cmd.ExecuteReader()

                If reader.Read = True Then

                    Dim nomeUsu As String
                    nomeUsu = reader(3)

                    Dim nomePort As String
                    nomePort = reader(1)

                    nomePorteiro = nomePort
                    nomeUsuario = nomeUsu

                    reader.Close()

                    Dim form = New CadastroCli
                    Me.Hide()
                    form.ShowDialog()

                    Exit Sub

                End If
                reader.Close()

            Catch ex As Exception

                MsgBox("Erro ao conectar", ex.Message)
            Finally

            End Try

            txtUsuario.Text = ""
            txtSenha.Text = ""
            txtUsuario.Focus()
            MessageBox.Show("Usuário e Senha invalido!", "Atenção!", MessageBoxButtons.OK, MessageBoxIcon.Stop)


        End If
    End Sub


    Private Sub Guna2GradientButton2_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton2.Click
        CadLogin.Show()
    End Sub

    Private Sub Guna2GradientButton1_Click(sender As Object, e As EventArgs) Handles Guna2GradientButton1.Click
        login()
    End Sub

End Class
