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
using RcMobile.Android.Library.ViewModels;
using Newtonsoft.Json;
using RcMobile.Android.Library.Repository;


namespace RcMobile.Android.App.Screens.RepairOrder.Info.Address
{
    [Activity(Label = "My Activity")]
    public class OwnerAddress : Activity
    {


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddressLayout);
            var x = JsonConvert.DeserializeObject<TaskViewModel>(Intent.GetStringExtra("task"));
            TextView Address = FindViewById<TextView>(Resource.Id.txtAddress);
            TextView Name = FindViewById<TextView>(Resource.Id.txtNameFirst);
            TextView City = FindViewById<TextView>(Resource.Id.txtCity);
            TextView Email = FindViewById<TextView>(Resource.Id.txtEmail);
            TextView PhoneHome = FindViewById<TextView>(Resource.Id.txtPhoneHome);
            TextView PostalCode = FindViewById<TextView>(Resource.Id.txtPostalCode);
            TextView ProvinceState = FindViewById<TextView>(Resource.Id.txtProvinceState);

            Address.Text = x.Address.AddressLine;
            Name.Text = x.OwnerFirstName+ " " + x.OwnerLastName;
            City.Text = x.Address.City;
            Email.Text = x.Address.OwnerEmail;
            PhoneHome.Text = x.Address.OwnerWorkPhone;
            PostalCode.Text = x.Address.PostalCode;
            ProvinceState.Text = x.Address.Province;


            
        }
    }
}