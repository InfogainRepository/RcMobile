using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Java.IO;
using Android.Graphics;
using RcMobile.Android.Library.Helper;

namespace RcMobile.Android.App.Screens.RepairOrder.Photos
{
    /// <summary>
    /// This screen shows the list of all photos captured for this task/job.
    /// </summary>
    [Activity(Label = "Photos")]
    public class PhotoDetails : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.PhotoDetails);
            if (string.IsNullOrWhiteSpace(Intent.GetStringExtra("CaptureImgPath")))
            {   
                TextView textview = FindViewById<TextView>(Resource.Id.textView1);
                textview.Text = "All Captured Photos";             
            }
            else
            {
                TextView textview = FindViewById<TextView>(Resource.Id.textView1);
                textview.Text = "Captured Photos";
                ImageView capuredPhoto = FindViewById<ImageView>(Resource.Id.imageView1);
                var CaptureImgPath = Intent.GetStringExtra("CaptureImgPath");

                int height = Resources.DisplayMetrics.HeightPixels;
                int width = Resources.DisplayMetrics.WidthPixels;
                var bitmap = BitmapHelpers.LoadAndResizeBitmap(CaptureImgPath, width, height);
              
                capuredPhoto.SetImageBitmap(bitmap);
            }
        }
    }
}