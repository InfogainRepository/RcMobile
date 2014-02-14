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
    public class InsuredAddress : Activity
    {
        private string _authenticationToken { get; set; }

        private InsuredDetailsViewModel _insuredDetails { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddressLayout);
            var ShopId = Intent.GetStringExtra("ShopId");
            var ShopOrgId = Intent.GetStringExtra("ShopOrgId");
            var JobId = Intent.GetStringExtra("JobId");
            _authenticationToken = Intent.GetStringExtra("AuthToken");
            _insuredDetails = new TaskRepository().Details(_authenticationToken, ShopOrgId, ShopId, JobId);


            TextView Address = FindViewById<TextView>(Resource.Id.txtAddress);
            TextView Name = FindViewById<TextView>(Resource.Id.txtNameFirst);
            TextView City = FindViewById<TextView>(Resource.Id.txtCity);
            TextView Email = FindViewById<TextView>(Resource.Id.txtEmail);
            TextView PhoneHome = FindViewById<TextView>(Resource.Id.txtPhoneHome);
            TextView PostalCode = FindViewById<TextView>(Resource.Id.txtPostalCode);
            TextView ProvinceState = FindViewById<TextView>(Resource.Id.txtProvinceState);

            Address.Text = _insuredDetails.Address1 + " " + _insuredDetails.Address2;
            Name.Text = _insuredDetails.NameFirst + " " + _insuredDetails.NameLast;
            City.Text = _insuredDetails.City;
            Email.Text = _insuredDetails.Email;
            PhoneHome.Text = _insuredDetails.PhoneHome;
            PostalCode.Text = _insuredDetails.PostalCode;
            ProvinceState.Text = _insuredDetails.ProvinceState;
        }
    }
}