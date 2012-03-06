using System;
using System.Drawing;
using System.Net;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.ObjCRuntime;

using Tog.mobile.media;

namespace Tog_iOS
{
	
	public partial class PhotoGalleryView : UIViewController
	{
		
		private PhotoGallery _gallery;
		
		#region IBOutlets
		
		[Connect("image")]
		private UIImageView image {
		    get { return ((UIImageView) (this.GetNativeField("image"))); }
		    set { this.SetNativeField("image", value);}
		}
		
		[Connect("photoTitle")]
		private UILabel photoTitle {
		    get { return ((UILabel) (this.GetNativeField("photoTitle"))); }
		    set { this.SetNativeField("photoTitle", value);}
		}
		
		[Connect("positionIndicator")]
		private UIPageControl positionIndicator {
		    get { return ((UIPageControl) (this.GetNativeField("positionIndicator"))); }
		    set { this.SetNativeField("positionIndicator", value);}
		}
		
		#endregion
		
		#region IBActions
		
		[Export ("onTapPhoto:")]
		public void onTapPhoto(int arg) {
		
		}
		
		#endregion
		
		
		public static Selector RightSwipeSelector
		{
		    get
		    {
		        return new Selector("HandleRightSwipe");
		    }
		}
		public class SwipeRecogniserDelegate : UIGestureRecognizerDelegate
		{
		    public override bool ShouldReceiveTouch (UIGestureRecognizer recognizer, UITouch touch)
		    {
		        return true;
		    }
		}
		[Export("HandleRightSwipe")]
		public void HandleRightSwipe(UISwipeGestureRecognizer recogniser)
		{
			setPhoto(_gallery.getNextPhoto());
		}
		
		
		
		public static Selector LeftSwipeSelector
		{
		    get
		    {
		        return new Selector("HandleLeftSwipe");
		    }
		}
		[Export("HandleLeftSwipe")]
		public void HandleLeftSwipe(UISwipeGestureRecognizer recogniser)
		{
			setPhoto(_gallery.getPrevPhoto());
		}
		
		
		
		
		
		
		
		
		
		
		public PhotoGalleryView () : base ("PhotoGalleryView", null)
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
			
			_gallery = new PhotoGallery();
			
			image.ContentMode = UIViewContentMode.ScaleToFill;
			setPhoto(_gallery.getCurrentPhoto());
			
			// gestures (RIGHT)
			UISwipeGestureRecognizer sgrRight=new UISwipeGestureRecognizer();
		    sgrRight.AddTarget(this,RightSwipeSelector);
		    sgrRight.Direction=UISwipeGestureRecognizerDirection.Right;
		    sgrRight.Delegate=new SwipeRecogniserDelegate();
		    View.AddGestureRecognizer(sgrRight);
			
			// gestures (LEFT)
			UISwipeGestureRecognizer sgrLeft=new UISwipeGestureRecognizer();
		    sgrLeft.AddTarget(this,LeftSwipeSelector);
		    sgrLeft.Direction=UISwipeGestureRecognizerDirection.Left;
		    sgrLeft.Delegate=new SwipeRecogniserDelegate();
		    View.AddGestureRecognizer(sgrLeft);
			
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		private void setPhoto(PhotoGallery.Photo photo) {
			
			positionIndicator.Pages = _gallery.photos.Count;
			positionIndicator.CurrentPage = _gallery.index;
			
			photoTitle.Text = photo.title;
		
			WebClient wc = new WebClient();
			Uri uri = new Uri(photo.path);
			wc.DownloadFile(uri, "avatar.png");
			UIImage img = UIImage.FromFile("avatar.png");
			
			image.Image = img;
			
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

