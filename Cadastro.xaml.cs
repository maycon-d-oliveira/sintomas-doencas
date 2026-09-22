using SQLite;

namespace SintomasDoencas;

public partial class Cadastro : ContentPage
{
    private bool isPasswordVisible = false;
    string caminhoBD;  //caminho do banco
    SQLiteConnection conexao;
    public Cadastro()
    {
        InitializeComponent();
        caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "pessoas.db3");
        conexao = new SQLiteConnection(caminhoBD);
        conexao.CreateTable<Pessoa>();

 

    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        string nome = Usuario.Text;
        string senha = Senha.Text;

        if (string.IsNullOrEmpty(nome) || string.IsNullOrEmpty(senha))
        {
            DisplayAlert("Erro", "Nome de usuário e senha são obrigatórios", "OK");
            return;
        }

        var novoUsuario = new Pessoa
        {
            Nome = nome,
            Password = senha
        };


         conexao.CreateTable<Pessoa>();

            try
            {
                conexao.Insert(novoUsuario);
                DisplayAlert("Sucesso", "Usuário cadastrado com sucesso", "OK");
                Navigation.PopAsync(); // Volta para a página anterior
            }
            catch (SQLiteException ex)
            {
                if (ex.Result == SQLite3.Result.Constraint)
                {
                    DisplayAlert("Erro", "Nome de usuário já existe", "OK");
                }
                else
                {
                    DisplayAlert("Erro", "Erro ao cadastrar usuário", "OK");
                }
            }
        }

    private void VerSenha_Clicked(object sender, EventArgs e)
    {
        isPasswordVisible = !isPasswordVisible;
        VerSenha.Source = isPasswordVisible ? "olhoaberto.png" : "olhofechado.png";

        // Lógica adicional para alternar entre mostrar e ocultar senha pode ser adicionada aqui
        // Se você estiver lidando com uma senha, pode alternar o Entry.IsPassword
        // Usuario.IsPassword = !isPasswordVisible;
        Senha.IsPassword = !isPasswordVisible;
    }
}