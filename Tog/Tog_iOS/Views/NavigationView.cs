using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.MapKit;

namespace Tog_iOS
{
	public partial class NavigationView : UIViewController
	{
		
		#region IBOutlets
		
		[Connect("mapview")]
		private MKMapView mapview {
		    get { return ((MKMapView) (this.GetNativeField("mapview"))); }
		    set { this.SetNativeField("mapview", value);}
		}
		
		[Connect("toolbar")]
		private UIToolbar toolbar {
		    get { return ((UIToolbar) (this.GetNativeField("toolbar"))); }
		    set { this.SetNativeField("toolbar", value);}
		}
		
		#endregion
		
		#region IBActions
		
		[Export ("onFindLocalHackerspace:")]
		public void onFindLocalHackerspace(int arg) {
		
		}
		
		[Export ("onOpenInNativeMapApp:")]
		public void onOpenInNativeMapApp(int arg) {
		
		}
		
		[Export ("onShowPOI:")]
		public void onShowPOI(int arg) {
		
		}
		
		#endregion
		
		public NavigationView () : base ("NavigationView", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

