using storage.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace storage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private StorageEContext _context = StorageEContext.GetContext();
        public MainWindow()
        {
            InitializeComponent();
            dgProduct.ItemsSource = _context.Products.ToList();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            new winCart().ShowDialog();
        }

        private void cmAdd_Click(object sender, RoutedEventArgs e)
        {
            if (Global.sale == null)
            {
                Global.sale = new Sale();
            }
            var prod = (Product)dgProduct.SelectedItem;
            var prodSale = Global.sale.ProductSales.ToList().Find(ps => ps.IdProductNavigation == prod);
            if (prodSale == null)
            {             
            Global.sale.ProductSales.Add(new ProductSale()
            {
                IdProduct = prod.Id,
                Count = 1,
                IdSale = Global.sale.Id,
                IdProductNavigation = prod,
                IdSaleNavigation = Global.sale
            });
            }
            else
            {
                prodSale.Count++;
            }

            if (Global.sale!.ProductSales.Count > 0)
            {
                btnCart.Visibility = Visibility.Visible;
            }
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            try
            {
                if (Global.sale!.ProductSales.Count == 0)
                {
                    btnCart.Visibility = Visibility.Hidden;
                }
            }
            catch
            {

            }

        }
    }
}
