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

namespace RcMobile.Android.App.Screens.RepairOrder.Info
{
    [Activity(Label = "My Activity")]
    public class InfoDetails : TabActivity
    {
        private TaskViewModel _jsonPassedIntentData = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RoInfoLayout);
            _jsonPassedIntentData = JsonConvert.DeserializeObject<TaskViewModel>(Intent.GetStringExtra("task"));
            TextView RoNumber = FindViewById<TextView>(Resource.Id.txtRoNumber);
            TextView ArrivedDate = FindViewById<TextView>(Resource.Id.txtArrivedDate);
            TextView VehicleYear = FindViewById<TextView>(Resource.Id.txtVehicleYear);
            TextView VehicleMake = FindViewById<TextView>(Resource.Id.txtVehicleMake);
            TextView VehicleLicense = FindViewById<TextView>(Resource.Id.txtVehicleLicense);
            TextView VehicleLicenseState = FindViewById<TextView>(Resource.Id.txtVehicleLicenseState);
            TextView VehicleVin = FindViewById<TextView>(Resource.Id.txtVehicleVin);
            TextView ClaimNumber = FindViewById<TextView>(Resource.Id.txtClaimNumber);
            TextView InsuranceCompanyName = FindViewById<TextView>(Resource.Id.txtInsuranceCompanyName);
            
            RoNumber.Text = _jsonPassedIntentData.RoNumber;
            ArrivedDate.Text = _jsonPassedIntentData.ArrivedDate != null ? _jsonPassedIntentData.ArrivedDate.Value.ToShortDateString() : string.Empty;
            VehicleYear.Text = _jsonPassedIntentData.VehicleYear;
            VehicleMake.Text = _jsonPassedIntentData.VehicleMake;
            VehicleLicense.Text = _jsonPassedIntentData.VehicleLicense;
            VehicleLicenseState.Text = _jsonPassedIntentData.VehicleLicenseState;
            VehicleVin.Text = _jsonPassedIntentData.VehicleVin;
            ClaimNumber.Text = _jsonPassedIntentData.ClaimNumber;
            InsuranceCompanyName.Text = _jsonPassedIntentData.InsuranceCompanyName;
            
            CreateTab(typeof(Screens.RepairOrder.Info.Address.OwnerAddress), "Owner", "Owner", Resource.Drawable.tab_element);
            CreateTab(typeof(Screens.RepairOrder.Info.Address.InsuredAddress), "Insured", "Insured", Resource.Drawable.tab_element);
            CreateTab(typeof(Screens.RepairOrder.Info.Address.ClaimantAddress), "Claimant", "Claimant", Resource.Drawable.tab_element);
        }

        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {          
            var ShopId = Intent.GetStringExtra("ShopId");
            var ShopOrgId = Intent.GetStringExtra("ShopOrgId");
            var JobId = Intent.GetStringExtra("JobId");
            var authenticationToken = Intent.GetStringExtra("AuthToken");
            var intent = new Intent(this, activityType);
            intent.AddFlags(ActivityFlags.NewTask);
            intent.PutExtra("task", JsonConvert.SerializeObject(_jsonPassedIntentData));
            intent.PutExtra("ShopId", ShopId);
            intent.PutExtra("ShopOrgId", ShopOrgId);
            intent.PutExtra("JobId", JobId);
            intent.PutExtra("AuthToken", authenticationToken);
            var spec = TabHost.NewTabSpec(tag);
            var drawableIcon = Resources.GetDrawable(drawableId);
            spec.SetIndicator(label, drawableIcon);
            spec.SetContent(intent);

            TabHost.AddTab(spec);
        }
    }
}