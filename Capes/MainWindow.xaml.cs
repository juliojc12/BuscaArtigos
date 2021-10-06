using BLL;
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

namespace Capes
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnBuscar_Click(object sender, RoutedEventArgs e)
        {
            string termo1 = TxtTermo1.Text;
            string termo2 = TxtTermo2.Text;

            if (string.IsNullOrEmpty(termo1) || string.IsNullOrEmpty(termo2))
            {
                return;
            }
            else
            {
                Busca busca = new Busca();
                busca.Executar(termo1, termo2);
            }

        }

    }
}
