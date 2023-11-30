using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace storage.Pages
{
    public partial class winCart : Window
    {
        private StorageEContext _context = StorageEContext.GetContext();
        List<Product> source = new List<Product>();
        public winCart()
        {
            InitializeComponent();


            List<string> punkts = new List<string>();

            _context.Punkts.ToList().ForEach(punkt =>
            {
                punkts.Add(punkt.Address);
            });

            cmbPunkt.ItemsSource = punkts;

            UpdateSource();
        }

        private void UpdateSource()
        {
            source = new List<Product>();
            Global.sale!.ProductSales.ToList().ForEach(ps =>
            {
                ps.IdProductNavigation.Count = ps.Count;
                ps.IdProductNavigation.Price = ps.IdProductNavigation.Price * ps.Count;
                source.Add(ps.IdProductNavigation);
            });
            dgProduct.ItemsSource = source;
            tbTotal.Text = "Итого: " + GetPrice().ToString() + " рублей";
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Focus();
            this.Close();
        }

        private void btnAddSale_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dpDate.SelectedDate != null && dpDate.SelectedDate > DateTime.Now)
                {
                    if (cmbPunkt.SelectedValue != null)
                    {
                        Global.sale!.DateAdd = dpDate.SelectedDate.Value;
                        Global.sale!.Price = GetPrice();
                        Global.sale.IdPunkt = cmbPunkt.SelectedIndex;
                        if (MessageBox.Show("Вы уверены?", "Подтвердите заказ",MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            Global.sale.ProductSales.ToList().ForEach(ps =>
                            {
                                _context.ProductSales.Add(ps);
                            });
                            _context.Sales.Add(Global.sale!);
                            Global.sale = null;
                            btnBack_Click(sender, e);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Выберите адресс");
                    }
                }
                else
                {
                    MessageBox.Show("Выберите правильную дату");
                }

                _context.SaveChanges();
            }
            catch
            {

                MessageBox.Show("Ошибка оформления заказа");
            }
        }

        private double GetPrice()
        {
            double price = 0;
            source.ForEach(p =>
            {
                try
                {
                    price += p.Price - (p.Price * (double)p.Discount!) / 100;
                }
                catch
                {
                    price += p.Price;
                }
            });

            return price;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var tb = (TextBox)sender;
            if (tb.Text == "0")
            {
                Global.sale!.ProductSales.ToList().ForEach(ps =>
                {
                    var prod = (Product)dgProduct.SelectedItem;
                    if (ps.IdProductNavigation == prod)
                    {
                        if (MessageBox.Show("Вы уверены?", "Подтвердите удаление товар", MessageBoxButton.YesNo)==MessageBoxResult.Yes)
                        {
                            Global.sale.ProductSales.Remove(ps);
                        }
                    }
                });

                if (Global.sale.ProductSales.Count ==0)
                {
                    this.Close();
                }
                UpdateSource();
            }
        }
    }
}
