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
using RcMobile.Android.Library.Repository;
using RcMobile.Android.App.Adapter;
using RcMobile.Android.Library.ViewModels;
using Newtonsoft.Json;
using RcMobile.Android.App.Screens.RepairOrder;

namespace RcMobile.Android.App.Screens
{
    /// <summary>
    /// This ListActivity shows the list of ROs. Which is our home screen after Login screen.
    /// </summary>
    [Activity(Label = "RO List", Icon = "@drawable/caricon")]
    public class HomeScreen : ListActivity
    {
        private IList<TaskViewModel> _taskList { get; set; }
        private string _authenticationToken { get; set; }
        private string _shopId { get; set; }
        private string _shopOrgId { get; set; }

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            _authenticationToken = Intent.GetStringExtra("AuthToken");
            _shopId = Intent.GetStringExtra("ShopId");
            _shopOrgId = Intent.GetStringExtra("ShopOrgId");

            // Create your application here
            _taskList = new TaskRepository().GetTaskList(_authenticationToken, _shopId);
            ListAdapter = new TaskAdapter(this, _taskList);
        }

        public override void OnBackPressed()
        {
            //base.OnBackPressed();
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            AlertDialog dialog = builder.Create();
            dialog.SetTitle("Logout");
            dialog.SetMessage("Are you sure you want to logout from current session?");
            //dialog.SetIcon(Resource.Drawable.cameraIcon);
            dialog.SetButton("OK", (s, ev) =>
            {
                //OK button was clicked on dialog, Add and show the captured photo in gallery
                this.Finish();
                var task = new Intent(this, typeof(LoginScreen));
                task.PutExtra("ClosingFlag", true);
                task.AddCategory(Intent.CategoryHome);
                task.AddFlags(ActivityFlags.NewTask);
                StartActivity(task);
            });
            dialog.SetButton2("Cancel", (s, ev) =>
            {
                //Cancel button was clicked on dialog
                dialog.Cancel();
            });
            dialog.Show();
        }

        /// <summary>
        /// Handling the selection event of any list item.
        /// </summary>
        /// <param name="l"></param>
        /// <param name="v"></param>
        /// <param name="position"></param>
        /// <param name="id"></param>
        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            TaskViewModel taskSelected = _taskList.ElementAt(position);

            var task = new Intent(this, typeof(RoDetails));
            task.PutExtra("AuthToken", _authenticationToken);
            task.PutExtra("JobId", taskSelected.JobId);
            task.PutExtra("ShopId", _shopId);
            task.PutExtra("ShopOrgId", _shopOrgId);

            task.PutExtra("task", JsonConvert.SerializeObject(taskSelected));

            StartActivity(task);
        }
    }
}