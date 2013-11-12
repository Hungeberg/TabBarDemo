using System;
using System.Collections.Generic;
using System.Linq;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Drawing;
using MonoTouch.Dialog;

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
        //EntryElement _login, _password;

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
            //UITabBar.Appearance.BackgroundColor = UIColor.Red;
            //UITabBar.Appearance.TintColor = UIColor.LightGray;

            /*_window.RootViewController = new DialogViewController (new RootElement ("Login") 
                {
                    new Section ("Credentials")
                        {
                            (_login = new EntryElement ("Login", "Enter your login", "")),
                            (_password = new EntryElement ("Password", "", "", true))
                        },
                    new Section () 
                        {
                            new StringElement ("Login", delegate 
                                {
                                    var alert = new UIAlertView("Login", string.Format("User {0} log-in", _login.Value), null, "OK", null);
                                    alert.Show();
                                    _window.RootViewController = new MyTabBarController(); 
                                })  
                        }
                });*/

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
                   Title = "Map",
                //HidesBottomBarWhenPushed = true
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
        private UIButton _b1, _b2, _b3, _b4, _b5, _b6, _b7;

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            View.BackgroundColor = UIColor.Blue;

            /*TabBarController.TabBar.BarStyle = UIBarStyle.Black;
            TabBarController.TabBar.Translucent = false;
            TabBarController.TabBar.Opaque = false;
            TabBarController.TabBar.Alpha = 0.9f;*/

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

            //_editField.AutoresizingMask = UIViewAutoresizing.FlexibleHeight;
            //View.SetNeedsLayout();

            _b1 = new UIButton(new RectangleF(20, 130, 200, 20));
            _b1.SetTitle("Toggle BarStyle Black/Default", UIControlState.Normal);
            _b1.TouchUpInside += (sender, e) => 
                { 
                    if (TabBarController.TabBar.BarStyle == UIBarStyle.Black)
                    {
                        TabBarController.TabBar.BarStyle = UIBarStyle.Default; 
                    }
                    else
                    {
                        TabBarController.TabBar.BarStyle = UIBarStyle.Black; 
                    }
                };

            _b2 = new UIButton(new RectangleF(20, 160, 200, 20));
            _b2.SetTitle("Bar Translucent = false", UIControlState.Normal);
            _b2.TouchUpInside += (sender, e) => { TabBarController.TabBar.Translucent = false; };

            _b3 = new UIButton(new RectangleF(20, 190, 200, 20));
            _b3.SetTitle("Bar Translucent = true", UIControlState.Normal);
            _b3.TouchUpInside += (sender, e) => { TabBarController.TabBar.Translucent = true; };

            _b4 = new UIButton(new RectangleF(20, 220, 200, 20));
            _b4.SetTitle("Bar Opaque = false", UIControlState.Normal);
            _b4.TouchUpInside += (sender, e) => { TabBarController.TabBar.Opaque = false; };

            _b5 = new UIButton(new RectangleF(20, 250, 200, 20));
            _b5.SetTitle("Bar Opaque = true", UIControlState.Normal);
            _b5.TouchUpInside += (sender, e) => { TabBarController.TabBar.Opaque = true; };

            _b6 = new UIButton(new RectangleF(20, 280, 200, 20));
            _b6.SetTitle("Bar Alpha--", UIControlState.Normal);
            _b6.TouchUpInside += (sender, e) => 
                { 
                    if (TabBarController.TabBar.Alpha >= 0.1f)
                    {
                        TabBarController.TabBar.Alpha = TabBarController.TabBar.Alpha - 0.1f; 
                    }
                };

            _b7 = new UIButton(new RectangleF(20, 310, 200, 20));
            _b7.SetTitle("Bar Alpha++", UIControlState.Normal);
            _b7.TouchUpInside += (sender, e) => 
                { 
                    if (TabBarController.TabBar.Alpha <= 0.9f)
                    {
                       TabBarController.TabBar.Alpha = TabBarController.TabBar.Alpha + 0.1f; 
                    }
                };

            View.AddSubviews(_b1, _b2, _b3, _b4, _b5, _b6, _b7);
        }
    }
}

