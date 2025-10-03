using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Business;
using Entity;

namespace Lab07Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {

            var business = new BProduct();
            var products = business.Read();

            ItemsDataGrid.ItemsSource = products;

        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("Name es requerido."); return;
            }

            if (!decimal.TryParse(txtPrice.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var price))
            {
                MessageBox.Show("Price inválido."); return;
            }

            if (!int.TryParse(txtStock.Text, out var stock))
            {
                MessageBox.Show("Stock inválido."); return;
            }

            var product = new Product
            {
                Name = txtName.Text.Trim(),
                Price = price,
                Stock = stock
            };

            try
            {
                var business = new BProduct();
                business.Create(product);
                MessageBox.Show("Product Created.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}"); 
            }
           
          


        }
    }
}