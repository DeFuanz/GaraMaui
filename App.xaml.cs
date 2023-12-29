using System.Diagnostics;

namespace Gara
{
    public partial class App : Application
    {
        public App()
        {
            
            InitializeComponent();

            MainPage = new AppShell();
        }

    }
}
