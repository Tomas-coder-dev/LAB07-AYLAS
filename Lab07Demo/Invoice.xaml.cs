using Business;
using Entity;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace Lab07Demo
{
    public partial class Invoice : Window
    {
        private int? editedInvoiceId = null;

        public Invoice()
        {
            InitializeComponent();
            // Set default date
            dateInvoice.SelectedDate = DateTime.Now;
        }

        private void Read_Click(object sender, RoutedEventArgs e)
        {
            var business = new BInvoice();
            var invoices = business.Read();
            ItemsDataGrid.ItemsSource = invoices;
        }

        private void CreateOrUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtCustomerID.Text, out var customerId))
            {
                MessageBox.Show("Cliente ID es requerido y debe ser numérico."); return;
            }

            if (!dateInvoice.SelectedDate.HasValue)
            {
                MessageBox.Show("La fecha es requerida."); return;
            }

            if (!decimal.TryParse(txtTotal.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out var total))
            {
                MessageBox.Show("El total es inválido."); return;
            }

            var business = new BInvoice();

            if (editedInvoiceId == null)
            {
                // Crear factura nueva
                var invoice = new Entity.Invoice
                {
                    CustomerID = customerId,
                    Date = dateInvoice.SelectedDate.Value,
                    Total = total
                };

                try
                {
                    business.Create(invoice);
                    MessageBox.Show("Factura creada correctamente.");
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
                // Actualizar factura existente
                var invoice = new Entity.Invoice
                {
                    InvoiceID = editedInvoiceId.Value,
                    CustomerID = customerId,
                    Date = dateInvoice.SelectedDate.Value,
                    Total = total
                };

                try
                {
                    business.Update(invoice);
                    MessageBox.Show("Factura actualizada correctamente.");
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
            if (sender is Button btn && btn.DataContext is Entity.Invoice invoice)
            {
                editedInvoiceId = invoice.InvoiceID;
                txtCustomerID.Text = invoice.CustomerID.ToString();
                dateInvoice.SelectedDate = invoice.Date;
                txtTotal.Text = invoice.Total.ToString(CultureInfo.InvariantCulture);
                btnCreateOrUpdate.Content = "Actualizar";
                btnCancelEdit.Visibility = Visibility.Visible;
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.DataContext is Entity.Invoice invoice)
            {
                if (MessageBox.Show($"¿Desea eliminar la factura #{invoice.InvoiceID}?", "Confirmar eliminación", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
                {
                    var business = new BInvoice();
                    business.Delete(invoice.InvoiceID);
                    MessageBox.Show("Factura eliminada correctamente (eliminación lógica).");
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
            editedInvoiceId = null;
            txtCustomerID.Text = "";
            dateInvoice.SelectedDate = DateTime.Now;
            txtTotal.Text = "";
            btnCreateOrUpdate.Content = "Crear";
            btnCancelEdit.Visibility = Visibility.Collapsed;
        }
    }
}