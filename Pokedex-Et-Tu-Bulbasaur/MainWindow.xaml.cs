using System.Net.Http;
using System.Text;
using System.Text.Json;
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

namespace Pokedex_Et_Tu_Bulbasaur
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5010/")
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private async void InputTextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var input = InputTextBox.Text.Trim();
            if (string.IsNullOrEmpty(input))
            {
                MessageBox.Show("Please Enter a Pokémon name or ID.");
                return;
            }

            OutputTextBlock.Text = "Loading...";

            try
            {
                var response = await _httpClient.GetAsync($"api/pokemon/{input}");
                if (!response.IsSuccessStatusCode)
                {
                    OutputTextBlock.Text = $"Error: Pokémon not found or API error.";
                    return;
                }

                var json = await response.Content.ReadAsStringAsync();

                var pokemon = JsonSerializer.Deserialize<PokemonResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {

                OutputTextBlock.Text = $"Exception: {ex.Message}";
            }
        }
    }
}