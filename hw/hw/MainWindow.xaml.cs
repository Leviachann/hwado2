using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

public partial class AdminView : Window
{
    private string connectionString;

    public AdminView()
    {
        InitializeComponent();

        connectionString = ConfigurationManager.ConnectionStrings["YourConnectionString"].ConnectionString;

        LoadProducts();
        LoadOrders();
    }

    private void LoadProducts()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductID, ProductName, Price FROM Products";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable productsTable = new DataTable();
                adapter.Fill(productsTable);

                dgProducts.ItemsSource = productsTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error occurred while loading products: " + ex.Message);
        }
    }

    private void LoadOrders()
    {
        try
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT OrderID, OrderDate, Total FROM Orders";
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                DataTable ordersTable = new DataTable();
                adapter.Fill(ordersTable);

                dgOrders.ItemsSource = ordersTable.DefaultView;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error occurred while loading orders: " + ex.Message);
        }
    }

    private void btnAddProduct_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            string productName = txtProductName.Text;
            decimal price = decimal.Parse(txtPrice.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO Products (ProductName, Price) VALUES (@ProductName, @Price)";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductName", productName);
                command.Parameters.AddWithValue("@Price", price);

                connection.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Product added successfully!");
                LoadProducts();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error occurred while adding product: " + ex.Message);
        }
    }

    private void btnUpdateProduct_Click(object sender, RoutedEventArgs e)
    {
        try
        {
            int productID = int.Parse(txtProductID.Text);
            string newProductName = txtNewProductName.Text;
            decimal newPrice = decimal.Parse(txtNewPrice.Text);

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "UPDATE Products SET ProductName = @ProductName, Price = @Price WHERE ProductID = @ProductID";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@ProductID", productID);
                command.Parameters.AddWithValue("@ProductName", newProductName);
                command.Parameters.AddWithValue("@Price", newPrice);

                connection.Open();
                command.ExecuteNonQuery();

                MessageBox.Show("Product updated successfully!");
                LoadProducts();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show("Error occurred while updating product: " + ex.Message);
        }
    }
}
