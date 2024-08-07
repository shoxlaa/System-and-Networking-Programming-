using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;
using EncryptDecrypt.ViewModels;
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

namespace EncryptDecrypt.Views
{
    /// <summary>
    /// Interaction logic for ModelListView.xaml
    /// </summary>
    public partial class ModelListView : UserControl
    {
        public ModelListView()
        {
            InitializeComponent();
            WeakReferenceMessenger.Default.Register<ValueChangedMessage<string>>(this, (sender, message) =>
            {
                if (message.Value == "OpenNewWindow")
                {
                    var view2 = new EncryptingView();
                    view2.Show();
                }

            });



        }
    }
}
