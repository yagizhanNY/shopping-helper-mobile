using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ShoppingHelperForms.Droid;
using ShoppingHelperForms.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

[assembly: Xamarin.Forms.Dependency(typeof(AudioService))]
namespace ShoppingHelperForms.Droid
{
    class AudioService : IAudio
    {
        public void PlaySound(string file)
        {
            MediaPlayer player = new MediaPlayer();
            Android.Content.Res.AssetFileDescriptor fd = Application.Context.Assets.OpenFd(file);

            player.Prepared += (s, e) =>
            {
                player.Start();
            };

            player.SetDataSource(fd.FileDescriptor, fd.StartOffset, fd.Length);
            player.Prepare();
        }
    }
}