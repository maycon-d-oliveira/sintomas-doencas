namespace SintomasDoencas
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();

            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping(nameof(BorderlessEntry), (Handler, view) =>
            {
#if __ANDROID__
                Handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif __IOS__
                Handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                Handler.PlatformView.BorderStyle = UIKit.UITextBorderStyle.None;
#endif
            });


        }
    }
}
