using Android.App;
using Android.Content;
using Android.Gms.Vision.Texts;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Text_Recognizer.Droid;
using static Android.Provider.MediaStore.Images;
using Android.Gms.Vision;
using System.IO;

[assembly: Dependency(typeof(Recognize))]
namespace Text_Recognizer.Droid
{
    internal class Recognize : IRecognize
    {
        string IRecognize.Recognize(byte[] input)
        {
            Bitmap bitmap = BitmapFactory.DecodeByteArray(input, 0, input.Length);

            return GetRecognizeResult(bitmap);
        }

        string GetRecognizeResult(Bitmap bitmap)
        {
            TextRecognizer txtRecognizer = new TextRecognizer
                .Builder(MainActivity.Instance.ApplicationContext)
                .Build();

            if (!txtRecognizer.IsOperational)
            {
                return "Can't create TextRecognizer Object";
            }
            else
            {
                Android.Gms.Vision.Frame frame = new Android.Gms.Vision
                    .Frame.Builder()
                    .SetBitmap(bitmap)
                    .Build();

                SparseArray items = txtRecognizer.Detect(frame);
                StringBuilder strBuilder = new StringBuilder();
                for (int i = 0; i < items.Size(); i++)
                {
                    TextBlock item = (TextBlock)items.ValueAt(i);
                    strBuilder.Append(item.Value);
                    strBuilder.Append("\n");
                }

                return strBuilder.ToString();
            }
        }
    }
}