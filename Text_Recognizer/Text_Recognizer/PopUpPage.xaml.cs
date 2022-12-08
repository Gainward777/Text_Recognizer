using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Text_Recognizer
{
    
    public partial class PopUpPage : Popup
    {
        public PopUpPage(string hu)
        {
            InitializeComponent();
            pUp.Text = hu;  
        }   

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Clipboard.SetTextAsync(pUp.Text);
            Dismiss(null);
        }
    }
}