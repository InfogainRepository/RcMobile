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
using RcMobile.Android.App.Screens.RepairOrder.Photos;

namespace RcMobile.Android.App.Screens.RepairOrder
{
    /// <summary>
    /// This TabActivity shows the Task Details screen which contains multiple tabs.
    /// </summary>
    [Activity(Label = "Task Details", Icon = "@drawable/caricon")]
    public class RoDetails : TabActivity
    {
        private TaskViewModel _jsonPassedIntentData = null;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.RoDetails);
            _jsonPassedIntentData = JsonConvert.DeserializeObject<TaskViewModel>(Intent.GetStringExtra("task"));
            CreateTab(typeof(Screens.RepairOrder.Info.InfoDetails), "Info", "Info", Resource.Drawable.tab_element);
            CreateTab(typeof(Screens.RepairOrder.Status.StatusDetails), "Status", "Status", Resource.Drawable.tab_element);
            CreateTab(typeof(Screens.RepairOrder.Photos.PhotoDetails), "Photos", "Photos", Resource.Drawable.tab_element);
            CreateTab(typeof(Screens.RepairOrder.Tasks.TasksDetails), "Tasks", "Tasks", Resource.Drawable.tab_element);

            //If intent contains the value of "CaptureImgPath" then we have to show the Photos Tab as active
            if (!string.IsNullOrWhiteSpace(Intent.GetStringExtra("CaptureImgPath")))
            {
                TabHost.SetCurrentTabByTag("Photos");
            }
        }

        /// <summary>
        /// Intializing the action bar.
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.default_actionbar_menu, menu);
            this.ActionBar.SetDisplayShowHomeEnabled(true);
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);
            return base.OnCreateOptionsMenu(menu);
        }

        /// <summary>
        /// TODO: DO real implemention of refresh button.
        /// Reload the current screen.
        /// </summary>
        /// <param name="featureId"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public override bool OnMenuItemSelected(int featureId, IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.action_refresh:
                    Toast.MakeText(this, "Refreshing RO Details..", ToastLength.Long).Show();
                    return true;
                case Resource.Id.action_capture:
                    var captureScreen = new Intent(this, typeof(CaptureScreen));
                    captureScreen.PutExtra("task", JsonConvert.SerializeObject(_jsonPassedIntentData));
                    StartActivity(captureScreen);                    
                    return true;
            }
            return base.OnMenuItemSelected(featureId, item);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            // app icon in action bar clicked; go home and show the Task List 
            var taskList = new Intent(this, typeof(HomeScreen));
            taskList.PutExtra("ShopId", Intent.GetStringExtra("ShopId"));
            taskList.PutExtra("ShopOrgId", Intent.GetStringExtra("ShopOrgId"));
            taskList.PutExtra("AuthToken", Intent.GetStringExtra("AuthToken"));
            SetResult(Result.Ok, taskList);
            StartActivity(taskList);
            //Finish();
        }


        private void CreateTab(Type activityType, string tag, string label, int drawableId)
        {
            var spec = TabHost.NewTabSpec(tag);
            var newIntent = new Intent(this, activityType);
            newIntent.AddFlags(ActivityFlags.NewTask);

            newIntent.PutExtra("task", JsonConvert.SerializeObject(_jsonPassedIntentData));
            if (tag == "Info")
            {
                newIntent.PutExtra("ShopId", Intent.GetStringExtra("ShopId"));
                newIntent.PutExtra("ShopOrgId", Intent.GetStringExtra("ShopOrgId"));
                newIntent.PutExtra("JobId", Intent.GetStringExtra("JobId"));
                newIntent.PutExtra("AuthToken", Intent.GetStringExtra("AuthToken"));
            }
            else if (tag == "Photos")
            {
                newIntent.PutExtra("CaptureImgPath", Intent.GetStringExtra("CaptureImgPath"));
            }
            spec.SetIndicator(label, Resources.GetDrawable(drawableId));
            spec.SetContent(newIntent);
            TabHost.AddTab(spec);
        }
    }
}