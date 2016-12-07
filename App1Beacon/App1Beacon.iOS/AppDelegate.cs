using System;
using System.Collections.Generic;
using System.Linq;
using CoreLocation;
using Estimote;
using Foundation;
using UIKit;

namespace App1Beacon.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.


    [Register ("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {

        BeaconManager beaconManager;
        CLBeaconRegion beaconRegion;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching (UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init ();
            LoadApplication (new App ());

            beaconManager = new BeaconManager ();
            beaconRegion = new CLBeaconRegion (new NSUuid ("F7826DA6-4FA2-4E98-8024-BC5B71E0893E"), "BeaconSample");
            beaconManager.RequestAlwaysAuthorization ();
            beaconManager.ReturnAllRangedBeaconsAtOnce = true;
            beaconManager.AuthorizationStatusChanged += (sender, e) => {
                StartRangingBeacons ();
            };
            beaconManager.RangedBeacons += (sender, e) => {
                if (e.Beacons.Length > 0)
                    new UIAlertView ("Beacons Found", "Just found: " + e.Beacons.Length + " beacons.", null, "OK").Show ();
            };
            //beaconManager.ExitedRegion += (sender, e) => {
            //    var notification = new UILocalNotification ();
            //    notification.AlertBody = "Exit region notification";
            //    UIApplication.SharedApplication.PresentLocalNotificationNow (notification);
            //};

            //beaconManager.EnteredRegion += (sender, e) => {
            //    var notification = new UILocalNotification ();
            //    notification.AlertBody = "Enter region notification";
            //    UIApplication.SharedApplication.PresentLocalNotificationNow (notification);
            //};

            beaconRegion.NotifyOnEntry = true;
            beaconRegion.NotifyOnExit = true;
            beaconManager.StartRangingBeaconsInRegion (beaconRegion);

            return base.FinishedLaunching (app, options);
        }

        private void StartRangingBeacons ()
        {

        }
    }
}
