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

namespace RcMobile.Android.App.Adapter
{
    class TaskAdapter : BaseAdapter
    {
        private class TaskItemAdapterHelper : Java.Lang.Object
        {
            public TextView RoNumber { get; set; }
            public TextView VehicleYear { get; set; }
            public TextView VehicleMake { get; set; }
            public TextView VehicleLicense { get; set; }
            public TextView VehicleLicenseState { get; set; }
            public TextView DueOut { get; set; }
            //public ImageView Image { get; set; }

        }

        private Activity _activity;
        private IEnumerable<TaskViewModel> _taskList;
        
        public TaskAdapter(Activity activity, IEnumerable<TaskViewModel> taskList)
        {
            this._activity = activity;
            this._taskList = taskList;
        }

        public override int Count
        {
            get { return _taskList.Count(); }
        }

        public override Java.Lang.Object GetItem(int position)
        {
            var item = _taskList.ElementAt(position);
            return item.Id;
        }

        public override long GetItemId(int position)
        {
            {
                var item = _taskList.ElementAt(position);
                return item.Id;
            }
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            TaskItemAdapterHelper helper = null;
            if (convertView == null)
            {
                convertView = _activity.LayoutInflater.Inflate(Resource.Layout.RoList, null);
                helper = new TaskItemAdapterHelper();
                helper.RoNumber = convertView.FindViewById<TextView>(Resource.Id.txtRoNumber);
                helper.VehicleYear = convertView.FindViewById<TextView>(Resource.Id.txtVehicleYear);
                helper.VehicleMake = convertView.FindViewById<TextView>(Resource.Id.txtVehicleMake);
                helper.VehicleLicense = convertView.FindViewById<TextView>(Resource.Id.txtVehicleLicense);
                helper.VehicleLicenseState = convertView.FindViewById<TextView>(Resource.Id.txtVehicleLicenseState);
                helper.DueOut = convertView.FindViewById<TextView>(Resource.Id.txtDueOut);
                //helper.Image = convertView.FindViewById<ImageView>(Resource.Id.image);

                convertView.Tag = helper;
            }
            else
            {
                helper = convertView.Tag as TaskItemAdapterHelper;
            }

            var item = _taskList.ElementAt(position);
            helper.RoNumber.Text = item.RoNumber;
            helper.VehicleYear.Text = item.VehicleYear;
            helper.VehicleMake.Text = item.VehicleMake;
            helper.VehicleLicense.Text = item.VehicleLicense;
            helper.VehicleLicenseState.Text = item.VehicleLicenseState;
            helper.DueOut.Text = item.DueOut != null ? item.DueOut.Value.ToShortDateString() : string.Empty;
            return convertView;
        }
    }
}