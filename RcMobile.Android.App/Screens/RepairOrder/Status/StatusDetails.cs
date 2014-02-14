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

namespace RcMobile.Android.App.Screens.RepairOrder.Status
{
    [Activity(Label = "My Activity")]
    public class StatusDetails : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //SetContentView(Resource.Layout.AddressLayout);
            //var task = Intent.GetStringExtra("task");
            //TextView textValue = FindViewById<TextView>(Resource.Id.txtNameFirst);
            //textValue.Text = task;
            TextView textview = new TextView(this);
            textview.Text = "This is the My Status tab";
            SetContentView(textview);
        }
    }
}