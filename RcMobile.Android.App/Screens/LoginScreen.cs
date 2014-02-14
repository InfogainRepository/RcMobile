using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using RcMobile.Android.Library.Repository;

namespace RcMobile.Android.App.Screens
{
    /// <summary>
    /// Login Activity. i.e. entry point of the applictaion.
    /// </summary>
    [Activity(Label = "Rc Mobile", MainLauncher = true, Icon = "@drawable/caricon")]
    public class LoginScreen : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            //Back button was pressed, user wants to log out, close the application.
            if(Intent.GetBooleanExtra("ClosingFlag",false))
            {
                Finish();
            }
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Login);

            // Get our button from the layout resource, and attach an event to it
            Button button = FindViewById<Button>(Resource.Id.login_button);

            EditText txtClientId = FindViewById<EditText>(Resource.Id.txtClientId);
            EditText txtUserName = FindViewById<EditText>(Resource.Id.txtUserId);
            EditText txtPassword = FindViewById<EditText>(Resource.Id.txtPassword);
            TextView txtResponse = FindViewById<TextView>(Resource.Id.response);

            button.Click += delegate
            {
                if (string.IsNullOrWhiteSpace(txtClientId.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
                {
                    Toast.MakeText(this, "Please enter all details.", ToastLength.Long).Show();
                }
                else
                {
                    var userCredential = new AuthenticationRepository().IsAuthenticated(txtClientId.Text, txtUserName.Text, txtPassword.Text);
                    if (userCredential == null)
                    {
                        txtResponse.Text = "Login failed!";
                    }
                    else
                    {
                        //Invalid login details passed.
                        var taskList = new Intent(this, typeof(HomeScreen));
                        taskList.PutExtra("AuthToken", userCredential.AuthToken);
                        taskList.PutExtra("ShopId", userCredential.ShopId);
                        taskList.PutExtra("ShopOrgId", userCredential.ShopOrgId);
                        
                        StartActivity(taskList);
                    }
                }
            };
        }
    }
}