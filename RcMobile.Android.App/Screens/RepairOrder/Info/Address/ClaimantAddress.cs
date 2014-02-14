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
    public class ClaimantAddress : Activity
    {
        private string _authenticationToken { get; set; }

        private InsuredDetailsViewModel _claimantsDetails { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.AddressLayout);
            var ShopId = Intent.GetStringExtra("ShopId");
            var ShopOrgId = Intent.GetStringExtra("ShopOrgId");
            var JobId = Intent.GetStringExtra("JobId");
            _authenticationToken = Intent.GetStringExtra("AuthToken");
            _claimantsDetails = new TaskRepository().GetRoClaimantDetails(_authenticationToken, ShopOrgId, ShopId, JobId);

            TextView Address = FindViewById<TextView>(Resource.Id.txtAddress);
            TextView Name = FindViewById<TextView>(Resource.Id.txtNameFirst);
            TextView City = FindViewById<TextView>(Resource.Id.txtCity);
            TextView Email = FindViewById<TextView>(Resource.Id.txtEmail);
            TextView PhoneHome = FindViewById<TextView>(Resource.Id.txtPhoneHome);
            TextView PostalCode = FindViewById<TextView>(Resource.Id.txtPostalCode);
            TextView ProvinceState = FindViewById<TextView>(Resource.Id.txtProvinceState);

            Address.Text = _claimantsDetails.Address1 + " " + _claimantsDetails.Address2;
            Name.Text = _claimantsDetails.NameFirst + " " + _claimantsDetails.NameLast;
            City.Text = _claimantsDetails.City;
            Email.Text = _claimantsDetails.Email;
            PhoneHome.Text = _claimantsDetails.PhoneHome;
            PostalCode.Text = _claimantsDetails.PostalCode;
            ProvinceState.Text = _claimantsDetails.ProvinceState;

        }
    }
}