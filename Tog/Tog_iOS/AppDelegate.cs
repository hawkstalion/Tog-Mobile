using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Tog.mobile;
using Tog.mobile.maps;
using Tog.mobile.media;
using Tog.mobile.web;

using System.Diagnostics;

namespace Tog_iOS
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the 
	// User Interface of the application, as well as listening (and optionally responding) to 
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public UIWindow window;
		
		//
		// This method is invoked when the application has loaded and is ready to run. In this 
		// method you should instantiate the window, load the UI into it and then make the window
		// visible.
		//
		// You have 17 seconds to return from this method, or iOS will terminate your application.
		//
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			// create a new window instance based on the screen size
			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			MainView mainView = new MainView();
			//window.RootViewController = mainView;
			
			UINavigationController navCon = new UINavigationController(mainView);
			navCon.NavigationBar.TintColor = UIColor.Black;
			navCon.NavigationBar.Hidden = true;
			window.RootViewController = navCon;
			
			// make the window visible
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

