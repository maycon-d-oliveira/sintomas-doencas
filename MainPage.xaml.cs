
using SQLite;

namespace SintomasDoencas
{
    public partial class MainPage : ContentPage
    {
        private bool isPasswordVisible = false;
        string caminhoBD;  //caminho do banco
        SQLiteConnection conexao;

        public MainPage()
        {
            InitializeComponent();
            caminhoBD = System.IO.Path.Combine(Microsoft.Maui.Storage.FileSystem.AppDataDirectory, "pessoas.db3");
            conexao = new SQLiteConnection(caminhoBD);
            conexao.CreateTable<Pessoa>();
        }

        private void login_Clicked(object sender, EventArgs e)
        {
            string username = Usuario.Text;
            string password = Senha.Text;

            var loginResult = ValidateUser(username, password);

            if (loginResult == LoginResult.Success)
            {
                DisplayAlert("Sucesso", "Login realizado com sucesso", "OK");
                // Redirecionar para outra página
                Navigation.PushAsync(new Inicial());
         
            }
            else if (loginResult == LoginResult.UserNotFound)
            {
                DisplayAlert("Erro", "Usuário não encontrado", "OK");
            }
            else if (loginResult == LoginResult.IncorrectPassword)
            {
                DisplayAlert("Erro", "Senha incorreta", "OK");
            }
        }

        private void Cadastrar_Clicked(object sender, EventArgs e)
        {

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

        private LoginResult ValidateUser(string username, string password)
        {
           
                conexao.CreateTable<Pessoa>();
                var Pessoa = conexao.Table<Pessoa>().FirstOrDefault(p => p.Nome == username);

                if (Pessoa == null)
                {
                    return LoginResult.UserNotFound;
                }

                if (Pessoa.Password != password)
                {
                    return LoginResult.IncorrectPassword;
                }

                return LoginResult.Success;
            
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {

            var cadastroPage = new Cadastro();
            await Navigation.PushAsync(cadastroPage);
            NavigationPage.SetHasNavigationBar(cadastroPage, true); // Certifique-se de que a barra de navegação está ativada
            NavigationPage.SetBackButtonTitle(cadastroPage, "Voltar"); // Opcional: define o texto do botão de voltar
            NavigationPage.SetTitleView(cadastroPage, new Label { Text = "Cadastro", HorizontalOptions = LayoutOptions.Center, TextColor = Color.FromArgb("#ffffff") }); // Opcional: define um título personalizado
            NavigationPage.SetTitleIconImageSource(cadastroPage, "login.png"); // Opcional: define um ícone para a barra de navegação
            NavigationPage.SetHasBackButton(cadastroPage, true); // Opcional: ativa o botão de voltar
        }
    }

    public enum LoginResult
    {
        Success,
        UserNotFound,
        IncorrectPassword
    }
}


