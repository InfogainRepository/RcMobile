using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

using Newtonsoft.Json;
using RcMobile.Android.Library.Helper;
using RcMobile.Android.Library.ViewModels;

using System.Threading.Tasks;

using Xamarin.Media;

namespace RcMobile.Android.App.Screens.RepairOrder.Photos
{
    [Activity(Label = "Capture Photos", Icon = "@drawable/caricon")]
    class CaptureScreen : Activity
    {
        private TaskViewModel _jsonPassedIntentData = null;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _jsonPassedIntentData = JsonConvert.DeserializeObject<TaskViewModel>(Intent.GetStringExtra("task"));
            var picker = new MediaPicker(this);
            if (!picker.IsCameraAvailable)
                Toast.MakeText(this, "No camera!", ToastLength.Long).Show();
            else
            {
                var intent = picker.GetTakePhotoUI(new StoreCameraMediaOptions
                {
                    Name = "test.jpg",//TODO: provide a good name using task id
                    Directory = "RCMobileCapture"
                });
                StartActivityForResult(intent, 1);
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            // User canceled
            if (resultCode == Result.Canceled)
            {
                //Cancel button was clicked on dialog
                StartActivity(RoDetailsSceenIntent());
                return;
            }
            data.GetMediaFileExtraAsync(this).ContinueWith(t =>
            {
                //Show captured image on the dialog 
                AlertDialog.Builder builder = new AlertDialog.Builder(this);

                var customView = LayoutInflater.Inflate(Resource.Layout.ShowImageDialog, null);
                builder.SetView(customView);
                ImageView capuredPhoto = customView.FindViewById<ImageView>(Resource.Id.capturedPhoto);
                int height = Resources.DisplayMetrics.HeightPixels;
                int width = Resources.DisplayMetrics.WidthPixels;
                var bitmap = BitmapHelpers.LoadAndResizeBitmap(t.Result.Path, width, height);
                capuredPhoto.SetImageBitmap(bitmap);

                AlertDialog dialog = builder.Create();
                dialog.SetTitle("Captured Photo");
                dialog.SetMessage("Click OK to confirm adding it to the task.");
                dialog.SetIcon(Resource.Drawable.cameraIcon);
                dialog.SetButton("OK", (s, ev) =>
                {
                    //OK button was clicked on dialog, Add and show the captured photo in gallery
                    var roDetails = RoDetailsSceenIntent();
                    roDetails.PutExtra("CaptureImgPath", t.Result.Path);
                    StartActivity(roDetails);
                });
                dialog.SetButton2("Recaputre", (s, ev) =>
                {
                    //Recaputre button was clicked on dialog
                    var intent = new Intent(this, typeof(CaptureScreen));
                    intent.PutExtra("task", JsonConvert.SerializeObject(_jsonPassedIntentData));
                    intent.AddFlags(ActivityFlags.NewTask);
                    this.StartActivity(intent);
                });
                dialog.SetButton3("Cancel", (s, ev) =>
                {
                    //Cancel button was clicked on dialog
                    dialog.Cancel();
                });
                dialog.CancelEvent += new System.EventHandler((s, ev) =>
                {
                    //back button is pressed
                    StartActivity(RoDetailsSceenIntent());
                });
                dialog.Show();
            }, TaskScheduler.FromCurrentSynchronizationContext());
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.default_actionbar_menu, menu);
            this.ActionBar.SetDisplayShowHomeEnabled(true);
            this.ActionBar.SetDisplayHomeAsUpEnabled(true);
            this.ActionBar.SetHomeButtonEnabled(true);

            menu.FindItem(Resource.Id.action_refresh).SetEnabled(false);
            menu.FindItem(Resource.Id.action_capture).SetEnabled(false);
            return base.OnCreateOptionsMenu(menu);
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
            // app icon in action bar clicked; go home
            StartActivity(RoDetailsSceenIntent());
        }

        /// <summary>
        /// Returns the intent of RoDetails activity with data <see cref="_jsonPassedIntentData"/> as named "task".
        /// </summary>
        private Intent RoDetailsSceenIntent()
        {
            var roDetailsScreen = new Intent(this, typeof(RoDetails));
            roDetailsScreen.PutExtra("task", JsonConvert.SerializeObject(_jsonPassedIntentData));
            roDetailsScreen.AddFlags(ActivityFlags.NewTask);
            StartActivity(roDetailsScreen);
            return roDetailsScreen;
        }
    }
}