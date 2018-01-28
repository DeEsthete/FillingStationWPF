using ClassLibr;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace FillingStation
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<Item> items;
        private string path;
        private int cash;
        public MainWindow()
        {
            InitializeComponent();
            path = Directory.GetCurrentDirectory() + "/items.txt";
            
            items = new List<Item>();
            Extract();
            //cash = 1000;
            cashTextBlock.Text = cash.ToString();
            //items = new List<Item>
            //{
            //    new Item
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "86",
            //        Count = 1000,
            //        Price = 2,
            //        Type = EnumTypeItem.Petrol
            //    },
            //    new Item
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "90",
            //        Count = 1000,
            //        Price = 4,
            //        Type = EnumTypeItem.Petrol
            //    }
            //};

            foreach (Item i in items)
            {
                product.Items.Add(i);
            }
            FillingStationWindow.Closed += Insert;
        }

        private void CalculateClick(object sender, RoutedEventArgs e)
        {
            if (product.SelectedItem != null)
            {
                Item item = (Item)product.SelectedItem;
                if (count.Text != null && count.Text != "")
                {
                    int price = item.Price * int.Parse(count.Text);
                    summa.Text = price.ToString();
                }
                else
                {
                    MessageBox.Show("Указано неверное количество!");
                }
            }
            else
            {
                MessageBox.Show("Извините но вы не выбрали нуждный товар!");
            }
        }

        private void IssueClick(object sender, RoutedEventArgs e)
        {
            cash += int.Parse(summa.Text);
            cashTextBlock.Text = cash.ToString();
            MessageBox.Show("Заказ успешно оформлен!");
        }

        #region BinaryFile
        public void Extract()
        {
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                cash = reader.ReadInt32();
                while (reader.PeekChar() > -1)
                {
                    Item temp = new Item();
                    temp.Name = reader.ReadString();
                    temp.Count = reader.ReadDouble();
                    temp.Price = reader.ReadInt32();
                    temp.Type = (EnumTypeItem)reader.ReadInt32();
                    temp.Id = Guid.NewGuid();
                    items.Add(temp);
                }
            }

        }

        public void Insert(object sender, EventArgs e)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate)))
            {
                writer.Write(cash);
                foreach (Item s in items)
                {
                    writer.Write(s.Name);
                    writer.Write(s.Count);
                    writer.Write(s.Price);
                    writer.Write((int)s.Type);
                }
            }
        }
        #endregion
    }
}
