using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Text_Recognizer;
using Xamarin.CommunityToolkit.Extensions;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Text_Recognizer
{
    public partial class MainPage : ContentPage
    {        
        private byte[] imageByteArray=null;

        public MainPage()
        {
            InitializeComponent();  
        }   

        private async void OnImagBClick(object sender, EventArgs e)
        {            
            try
            {
                var result = await MediaPicker.PickPhotoAsync();
                Stream imageStream = await result.OpenReadAsync();
                imageByteArray=GetByteArray(imageStream);               

                ImagB.Source = ImageSource.FromStream(() => new MemoryStream(imageByteArray));
            }
            catch(Exception ex) 
            {                
            
            }
        }

        private async void OnRecognizeButtonClick(object sender, EventArgs e)
        {
            
            if (imageByteArray != null)
            {
                string result = DependencyService.Get<IRecognize>().Recognize(imageByteArray);

                if (result != string.Empty)
                {
                    await Navigation.ShowPopupAsync(new PopUpPage(result));
                }
                else
                {
                    await Navigation.ShowPopupAsync(new PopUpPage("There's no text to rcognize here!"));
                }
            }
            else
            {
                await Navigation.ShowPopupAsync(new PopUpPage("Choose image."));
            }    
        }

        private async void OnRecButtonClick(object sender, EventArgs e)
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();
                Stream imageStream = await result.OpenReadAsync();
                imageByteArray = GetByteArray(imageStream);

                ImagB.Source = ImageSource.FromStream(() => new MemoryStream(imageByteArray));

                
            }
            catch (Exception ex)
            {

            }
        }        

        private void OnToggled(object sender, ToggledEventArgs e)
        {
            if (e.Value)
            {
                ImagB.Aspect = Aspect.AspectFill;
            }
            else
            {
                ImagB.Aspect = Aspect.AspectFit;
            }
        }

        private byte[] GetByteArray(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        } 
    }
}
