using Business;
using Entity;
using System.Windows;
using System.Xml.Linq;

namespace Lab07Demo
{
    public partial class Customer : Window
    {
        private int? editedCustomerId = null;

        public Customer()
        {
            InitializeComponent();
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            var business = new BCustomer();
            var customers = business.Read();
            ItemsDataGrid.ItemsSource = customers;
        }

        private void CreateOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text))
            {
                MessageBox.Show("El nombre es requerido."); return;
            }

            if (string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("La dirección es requerida."); return;
            }

            if (string.IsNullOrWhiteSpace(txtPhone.Text))
            {
                MessageBox.Show("El teléfono es requerido."); return;
            }

            var business = new BCustomer();

            if (editedCustomerId == null)
            {
                // Crear cliente nuevo
                var customer = new Entity.Customer
                {
                    Name = txtName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };

                try
                {
                    business.Create(customer);
                    MessageBox.Show("Cliente creado correctamente.");
                    Read_Click(null, null);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al crear: {ex.Message}");
                }
            }
            else
            {
                // Actualizar cliente existente
                var customer = new Entity.Customer
                {
                    CustomerID = editedCustomerId.Value,
                    Name = txtName.Text.Trim(),
                    Address = txtAddress.Text.Trim(),
                    Phone = txtPhone.Text.Trim()
                };

                try
                {
                    business.Update(customer);
                    MessageBox.Show("Cliente actualizado correctamente.");
                    Read_Click(null, null);
                    ClearForm();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar: {ex.Message}");
                }
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button btn && btn.DataContext is Entity.Customer customer)
            {
                editedCustomerId = customer.CustomerID;
                txtName.Text = customer.Name;
                txtAddress.Text = customer.Address;
                txtPhone.Text = customer.Phone;
                btnCreateOrUpdate.Content = "Actualizar";
                btnCancelEdit.Visibility = Visibility.Visible;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Button btn && btn.DataContext is Entity.Customer customer)
            {
                if (MessageBox.Show($"¿Desea eliminar el cliente '{customer.Name}'?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var business = new BCustomer();
                    business.Delete(customer.CustomerID);
                    MessageBox.Show("Cliente eliminado correctamente (eliminación lógica).");
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
            editedCustomerId = null;
            txtName.Text = "";
            txtAddress.Text = "";
            txtPhone.Text = "";
            btnCreateOrUpdate.Content = "Crear";
            btnCancelEdit.Visibility = Visibility.Collapsed;
        }
    }
}