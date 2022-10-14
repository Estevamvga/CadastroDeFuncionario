Imports System.Data.Odbc
Imports MySql.Data.MySqlClient

Public Class CadastroCli

    Dim cpf As String
    Dim usu As String

    'Dim V_conexao1 As OdbcConnection

    ' PASSANDO CONEXÃO VIA MODULO

    Public v_conexao As New OdbcConnection '("DRIVER={MySQL ODBC 5.2 UNICODE Driver};server=192.168.0.151;database=sortee08_projetocli;option=3;port=3306;user=sortee_user_adm;password=80902019")

    Private Sub CadastroCli_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Listar()
        rbNome.Checked = True

    End Sub

    'CARREGAR DADOS DIRETAMENTO NO GRID 1
    Sub Listar()
        Try
            v_conexao = AbreCone()
            Dim dt As New DataTable ' tabela
            Dim da As OdbcDataAdapter
            Dim sql As String
            sql = "SELECT * FROM funcionario ORDER by nome_funcionario asc" 'SELECT DIRETAMENTO NO BANCO  

            da = New OdbcDataAdapter(sql, v_conexao) 'CHAMA A CONEXÃO 
            da.Fill(dt)
            Dg.DataSource = dt
            FormadaDG()
        Catch ex As Exception
            MsgBox("Erro ao listar" & vbCrLf & ex.Message)
        End Try

    End Sub

    Private Sub btnSalvar_Click(sender As Object, e As EventArgs) Handles btnSalvar.Click
        If txtNome.Text.Trim <> String.Empty AndAlso txtCPF.Text.Trim <> String.Empty Then

            'txtNome.Text = "" Or txtCPF.Text = "" Then
            'If txtLogin.Text.Trim <> String.Empty AndAlso txtSenha.Text.Trim <> String.Empty Then

            Try
                'VALIDAR O CPF CASO ELE JÁ ESTEJA CADASTRADO
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmdCPF As OdbcCommand
                Dim readerCPF As OdbcDataReader ' devolver tudo que foi executado
                Dim sqlCPF As String
                sqlCPF = " SELECT * FROM funcionario WHERE cpf= '" & txtCPF.Text & "' " 'SELECT PARA CONSULTAR O CPF NO BANCO DE DADOS
                cmdCPF = New OdbcCommand(sqlCPF, v_conexao)
                readerCPF = cmdCPF.ExecuteReader()

                If readerCPF.Read = True Then
                    readerCPF.Close() 'TODAS AS VEZES QUE USAR O READER PRECISA SER FECHADO 
                    MessageBox.Show("CPF já cadastrado!", "Atenção")
                    txtCPF.Text = ""
                    txtCPF.Focus()
                    Exit Sub
                End If

                'VALIDAR O USUARIO CASO ELE JÁ ESTEJA CADASTRADO
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmdUSER As OdbcCommand
                Dim readerUSER As OdbcDataReader ' devolver tudo que foi executado
                Dim sqlUSER As String
                sqlUSER = " SELECT * FROM funcionario WHERE nome_funcionario= '" & txtNome.Text & "' " 'SELECT PARA CONSULTAR O CPF NO BANCO DE DADOS
                cmdUSER = New OdbcCommand(sqlUSER, v_conexao)
                readerUSER = cmdUSER.ExecuteReader()
                If readerUSER.Read = True Then
                    readerUSER.Close() 'TODAS AS VEZES QUE USAR O READER PRECISA SER FECHADO 
                    MessageBox.Show("Funcionário já cadastrado!", "Atenção")
                    txtNome.Text = ""
                    txtNome.Focus()
                    Exit Sub
                End If



                'REALIZAR INCLUSÃO DOS DADOS CADASTRADO NO BANCO DE DADOS
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmd As OdbcCommand
                Dim sql As String
                sql = "INSERT INTO funcionario (nome_funcionario, cpf, telefone, endereco, bairro, cidade, email) VALUES ('" & txtNome.Text & "', '" & txtCPF.Text & "', '" & txtTelefone.Text & "', '" & txtEndereco.Text & "', '" & txtBairro.Text & "', '" & txtCidade.Text & "', '" & txtEmail.Text & "') "
                cmd = New OdbcCommand(sql, v_conexao)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Salvo com sucesso", "Confirmação!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Limpar()
                Listar()
                DesabilitarCampos()

                'End If

            Catch ex As Exception
                MsgBox("Erro ao salvar", MessageBoxIcon.Information & vbCrLf & ex.Message)
            End Try
        Else
            MsgBox("Preencha os campos!!")

        End If
    End Sub


    Private Sub btnEditar_Click(sender As Object, e As EventArgs) Handles btnEditar.Click
        If txtNome.Text <> "" Or txtCPF.Text <> "" Then
            Try
                'VALIDAR O CPF CASO ELE JÁ ESTEJA CADASTRADO
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmdCPF As OdbcCommand
                Dim readerCPF As OdbcDataReader ' devolver tudo que foi executado
                Dim sqlCPF As String

                sqlCPF = " SELECT * FROM funcionario WHERE cpf= '" & txtCPF.Text & "' " 'SELECT PARA CONSULTAR O CPF NO BANCO DE DADOS
                cmdCPF = New OdbcCommand(sqlCPF, v_conexao)
                readerCPF = cmdCPF.ExecuteReader()

                If readerCPF.Read = True And txtCPF.Text <> cpf Then
                    readerCPF.Close() 'TODAS AS VEZES QUE USAR O READER PRECISA SER FECHADO 
                    MessageBox.Show("CPF já cadastrado. Desejar continuar?", "Atenção")
                    txtCPF.Text = ""
                    txtCPF.Focus()
                    Exit Sub
                End If

                'VALIDAR O USUARIO CASO ELE JÁ ESTEJA CADASTRADO
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmdUSER As OdbcCommand
                Dim readerUSER As OdbcDataReader ' devolver tudo que foi executado
                Dim sqlUSER As String

                sqlUSER = " SELECT * FROM funcionario WHERE nome_funcionario = '" & txtNome.Text & "' " 'SELECT PARA CONSULTAR O CPF NO BANCO DE DADOS
                cmdUSER = New OdbcCommand(sqlUSER, v_conexao)
                readerUSER = cmdUSER.ExecuteReader()

                If readerUSER.Read = True And txtEndereco.Text <> usu Then
                    readerUSER.Close() 'TODAS AS VEZES QUE USAR O READER PRECISA SER FECHADO 
                    MessageBox.Show("Nome já cadastrado. Desejar continuar?", "Atenção")
                    txtEndereco.Text = ""
                    txtEndereco.Focus()
                    Exit Sub
                End If

                Dim resultado As DialogResult = MessageBox.Show("Confirmar alteração?", "Alterar Registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resultado = vbYes Then

                    'REALIZAR ATUALIZAÇÃO DOS DADOS CADASTRADO NO BANCO DE DADOS
                    v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                    Dim cmd As OdbcCommand
                    Dim sql As String

                    sql = "UPDATE funcionario SET nome_funcionario = '" & txtNome.Text & "',  telefone = '" & txtTelefone.Text & "', cpf = '" & txtCPF.Text & "', endereco = '" & txtEndereco.Text & "', bairro = '" & txtBairro.Text & "', cidade = '" & txtCidade.Text & "', email = '" & txtEmail.Text & "'  where id_funcionario = '" & txtCodigo.Text & "' "
                    cmd = New OdbcCommand(sql, v_conexao)
                    cmd.ExecuteNonQuery()
                    Limpar()
                    Listar()
                    DesabilitarCampos()

                End If

            Catch ex As Exception
                MsgBox("Erro ao salvar", MessageBoxIcon.Information & vbCrLf & ex.Message)
            End Try
        Else
            MsgBox("Preencha os campos!!")
        End If
    End Sub

    Private Sub btnDeletar_Click(sender As Object, e As EventArgs) Handles btnDeletar.Click
        Try
            'REALIZAR DELETAR DADOS CADASTRADO NO BANCO DE DADOS
            Dim resultado As DialogResult = MessageBox.Show("Confirmar Exclusão?", "Excluir registro", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultado = vbYes Then
                v_conexao = AbreCone() 'CONEXÃO PADRÃO USADA
                Dim cmd As OdbcCommand
                Dim sql As String

                sql = "DELETE FROM funcionario WHERE id_funcionario = '" & txtCodigo.Text & "' "
                cmd = New OdbcCommand(sql, v_conexao)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Cliente Excluido", "Confirmação!!!", MessageBoxButtons.OK, MessageBoxIcon.Information)

                Limpar()
                Listar()
                DesabilitarCampos()
            End If
        Catch ex As Exception
            MsgBox("Erro ao excluir", ex.Message)
        End Try


    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        txtNome.Focus()
        HabilitarCarmpos()
        Limpar()
        btnSalvar.Enabled = True
        btnCancelar.Enabled = True
    End Sub

    Sub HabilitarCarmpos()
        txtNome.Enabled = True
        txtCPF.Enabled = True
        txtTelefone.Enabled = True
        txtEndereco.Enabled = True
        txtBairro.Enabled = True
        txtCidade.Enabled = True
        txtEmail.Enabled = True
    End Sub

    Sub DesabilitarCampos()
        txtNome.Enabled = False
        txtCPF.Enabled = False
        txtTelefone.Enabled = False
        txtEndereco.Enabled = False
        txtBairro.Enabled = False
        txtCodigo.Enabled = False
        txtCidade.Enabled = False
        txtEmail.Enabled = False
    End Sub

    Sub Limpar()
        txtNome.Text = ""
        txtCPF.Text = ""
        txtTelefone.Text = ""
        txtEndereco.Text = ""
        txtBairro.Text = ""
        txtBuscaNome.Text = ""
        txtCidade.Text = ""
        txtEmail.Text = ""
        txtCodigo.Text = ""
    End Sub

    Sub FormadaDG()

        'OCULTAR COLUNA NO GRID
        Dg.Columns(0).Visible = False

        'ADD COLUNA NO GRID
        Dg.Columns(0).HeaderText = "ID do Funcionário"
        Dg.Columns(1).HeaderText = "Nome"
        Dg.Columns(2).HeaderText = "CPF"
        Dg.Columns(3).HeaderText = "Telefone"
        Dg.Columns(4).HeaderText = "Endereço"
        Dg.Columns(5).HeaderText = "Bairro"
        Dg.Columns(6).HeaderText = "Cidade"
        Dg.Columns(7).HeaderText = "E-mail"

        Dg.Columns(1).Width = 200
        Dg.Columns(2).Width = 100
        Dg.Columns(3).Width = 100
        Dg.Columns(4).Width = 200
        Dg.Columns(5).Width = 200
        Dg.Columns(6).Width = 200
        Dg.Columns(7).Width = 200

    End Sub

    Sub DesabilitaAlterar()
        btnEditar.Enabled = True
        btnDeletar.Enabled = True
        btnCancelar.Enabled = True
        btnSalvar.Enabled = False
        btnNovo.Enabled = False
        txtCodigo.Visible = True
    End Sub



    Private Sub Dg_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles Dg.CellClick
        DesabilitaAlterar()
        HabilitarCarmpos()

        txtCodigo.Text = Dg.CurrentRow.Cells(0).Value
        txtNome.Text = Dg.CurrentRow.Cells(1).Value
        txtTelefone.Text = Dg.CurrentRow.Cells(2).Value
        txtCPF.Text = Dg.CurrentRow.Cells(3).Value
        txtEndereco.Text = Dg.CurrentRow.Cells(4).Value
        txtBairro.Text = Dg.CurrentRow.Cells(5).Value
        txtCidade.Text = Dg.CurrentRow.Cells(6).Value
        txtEmail.Text = Dg.CurrentRow.Cells(7).Value


        cpf = Dg.CurrentRow.Cells(3).Value
        usu = Dg.CurrentRow.Cells(4).Value



    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        Limpar()
        DesabilitarCampos()
        btnCancelar.Enabled = False
        btnSalvar.Enabled = False
        btnNovo.Enabled = True
    End Sub

    Private Sub rbNome_CheckedChanged(sender As Object, e As EventArgs) Handles rbNome.CheckedChanged

        txtBuscaNome.Text = ""
        txtBuscaNome.Visible = True

        txtBuscarCPF.Text = ""
        txtBuscarCPF.Visible = False


    End Sub

    Private Sub rbCPF_CheckedChanged(sender As Object, e As EventArgs) Handles rbCPF.CheckedChanged

        txtBuscaNome.Text = ""
        txtBuscaNome.Visible = False

        txtBuscarCPF.Text = ""
        txtBuscarCPF.Visible = True
    End Sub

    Private Sub txtBuscaNome_TextChanged(sender As Object, e As EventArgs) Handles txtBuscaNome.TextChanged
        BuscarNome()
    End Sub

    Private Sub txtBuscarCPF_TextChanged(sender As Object, e As EventArgs) Handles txtBuscarCPF.TextChanged
        BuscarCPF()
    End Sub

    Sub BuscarNome()
        Try
            v_conexao = AbreCone()
            Dim dt As New DataTable ' tabela
            Dim da As OdbcDataAdapter
            Dim sql As String

            sql = "SELECT * from funcionario where nome_funcionario LIKE '" & txtBuscaNome.Text & "%' order by nome_funcionario asc" 'SELECT DIRETAMENTO NO BANCO  
            da = New OdbcDataAdapter(sql, v_conexao) 'CHAMA A CONEXÃO 

            da.Fill(dt)
            Dg.DataSource = dt

        Catch ex As Exception
            MsgBox("Erro ao listar" & vbCrLf & ex.Message)
        End Try

    End Sub

    Sub BuscarCPF()
        Try
            v_conexao = AbreCone()
            Dim dt As New DataTable ' tabela
            Dim da As OdbcDataAdapter
            Dim sql As String

            sql = "SELECT * from funcionario where cpf LIKE '" & txtBuscarCPF.Text & "%' order by nome_funcionario asc" 'SELECT DIRETAMENTO NO BANCO  
            da = New OdbcDataAdapter(sql, v_conexao) 'CHAMA A CONEXÃO 

            da.Fill(dt)
            Dg.DataSource = dt

        Catch ex As Exception
            MsgBox("Erro ao listar" & vbCrLf & ex.Message)
        End Try
    End Sub


End Class