using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string authority = "https://indra.sts.atc.intranet.gencat.cat/adfs";
            string resourceURI = "https://dev.seu.atc.intranet.gencat.cat/WebApiTest/";

            string clientID = "bc972dee-8b30-4c08-82d8-afeb402c9e56";
            string clientReturnURI = "http://localhost:4200/";

            var authContext = new AuthenticationContext(authority, false);
            var authResult = await authContext.AcquireTokenAsync(resourceURI, clientID, new Uri(clientReturnURI), new PlatformParameters(PromptBehavior.Auto));

            string authHeader = authResult.CreateAuthorizationHeader();

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://dev.seu.atc.intranet.gencat.cat/WebApiTest/api/values");
            request.Headers.TryAddWithoutValidation("Authorization", authHeader);
            var response = await client.SendAsync(request);
            string responseString = await response.Content.ReadAsStringAsync();
            MessageBox.Show(responseString);
        }
    }
}
