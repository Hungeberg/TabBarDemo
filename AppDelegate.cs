using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;

namespace TabBarDemo
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the
    // User Interface of the application, as well as listening (and optionally responding) to
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : UIApplicationDelegate
    {
        // class-level declarations
        UIWindow _window;
        MyTabBarController _viewController;

        //
        // This method is invoked when the application has loaded and is ready to run. In this
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            _window = new UIWindow(UIScreen.MainScreen.Bounds);

            _viewController = new MyTabBarController();

            // If you have defined a root view controller, set it here:
            _window.RootViewController = _viewController;
            
            // make the window visible
            _window.MakeKeyAndVisible();
            
            return true;
        }
    }

    public class MyTabBarController : UITabBarController
    {
        private UIViewController tab1, tab2, tab3, tab4, tab5;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            var mapViewController = new CNMapViewController()
                {
                    Title = "Map"
                };

            tab1 = new UINavigationController(mapViewController);

            tab1.TabBarItem = new UITabBarItem ();
            tab1.TabBarItem.Image = UIImage.FromBundle("location.png");
            tab1.TabBarItem.Title = "Map2";

            tab2 = new UIViewController();
            tab2.Title = "Orange";
            tab2.View.BackgroundColor = UIColor.Orange;
            tab3 = new UIViewController();
            tab3.Title = "Red";
            tab3.View.BackgroundColor = UIColor.Red;
            tab4 = new UIViewController();
            tab4.Title = "Blue";
            tab4.View.BackgroundColor = UIColor.Blue;
            tab5 = new UIViewController();
            tab5.Title = "Brown";
            tab5.View.BackgroundColor = UIColor.Brown;

            var tabs = new UIViewController[]
                {
                    tab1, tab2, tab3, tab4, tab5
                };

            ViewControllers = tabs;
            SelectedViewController = tab1;
        }
    }

    public class CNMapViewController : UIViewController
    {
        private UITextField _editField;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Blue;

            _editField = new UITextField(new RectangleF(20, 100, 100, 20));
            _editField.BackgroundColor = UIColor.Brown;
            _editField.TextColor = UIColor.White;
            _editField.EditingDidEndOnExit += (sender, e) => 
                { 
                    _editField.ResignFirstResponder();
                    var alert = new UIAlertView("You entered", _editField.Text, null, "OK", null);
                    alert.Show(); 
                };
            View.Add(_editField);
            View.BringSubviewToFront(_editField);
        }
    }
}

