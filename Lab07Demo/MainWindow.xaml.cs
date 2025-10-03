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
    public partial class MainWindow : Window
    {
        private int? editedProductId = null; // Para saber si estamos editando

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

        private void CreateOrUpdate_Click(object sender, RoutedEventArgs e)
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

            var business = new BProduct();

            if (editedProductId == null)
            {
                // Crear producto nuevo
                var product = new Product
                {
                    Name = txtName.Text.Trim(),
                    Price = price,
                    Stock = stock
                };

                try
                {
                    business.Create(product);
                    MessageBox.Show("Product Created.");
                    Read_Click(null, null);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                // Actualizar producto existente
                var product = new Product
                {
                    ProductID = editedProductId.Value,
                    Name = txtName.Text.Trim(),
                    Price = price,
                    Stock = stock
                };

                try
                {
                    business.Update(product);
                    MessageBox.Show("Product Updated.");
                    Read_Click(null, null);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Product product)
            {
                editedProductId = product.ProductID;
                txtName.Text = product.Name;
                txtPrice.Text = product.Price.ToString(CultureInfo.InvariantCulture);
                txtStock.Text = product.Stock.ToString();
                btnCreateOrUpdate.Content = "Update";
                btnCancelEdit.Visibility = Visibility.Visible;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Product product)
            {
                if (MessageBox.Show($"¿Desea eliminar el producto '{product.Name}'?", "Confirmar", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    var business = new BProduct();
                    business.Delete(product.ProductID);
                    MessageBox.Show("Product deleted (logical).");
                    Read_Click(null, null);
                    ClearForm();
                }
            }
        }

        private void CancelEdit_Click(object sender, RoutedEventArgs e)
        {
            ClearForm();
        }

        private void ClearForm()
        {
            editedProductId = null;
            txtName.Text = "";
            txtPrice.Text = "";
            txtStock.Text = "";
            btnCreateOrUpdate.Content = "Create";
            btnCancelEdit.Visibility = Visibility.Collapsed;
        }

        // Método para abrir la ventana de clientes
        private void Clientes_Click(object sender, RoutedEventArgs e)
        {
            var ventanaClientes = new Customer();
            ventanaClientes.ShowDialog();
        }

        // Método para abrir la ventana de facturas
        private void Invoices_Click(object sender, RoutedEventArgs e)
        {
            var ventanaFacturas = new Invoice();
            ventanaFacturas.ShowDialog();
        }
    }
}