using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using Tog.mobile.web;

using System.Diagnostics;

namespace Tog_iOS
{
	public partial class ContactView : UIViewController
	{

		#region IBOutlets
		
		[Connect("name")]
		private UITextField name {
		    get { return ((UITextField) (this.GetNativeField("name"))); }
		    set { this.SetNativeField("name", value);}
		}
		
		[Connect("email")]
		private UITextField email {
		    get { return ((UITextField) (this.GetNativeField("email"))); }
		    set { this.SetNativeField("email", value);}
		}
		
		[Connect("comments")]
		private UITextView comments {
		    get { return ((UITextView) (this.GetNativeField("comments"))); }
		    set { this.SetNativeField("comments", value);}
		}
		
		[Connect("image")]
		private UIImageView image {
		    get { return ((UIImageView) (this.GetNativeField("image"))); }
		    set { this.SetNativeField("image", value);}
		}
		
		#endregion
		
		#region IBActions
		
		[Export ("onTouchDownInsideSubmit:")]
		public void onTouchDownInsideSubmit(int arg) {
		
		}
		
		#endregion
		
		public ContactView () : base ("ContactView", null)
		{
		}
		
		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public void sendMessage() {
			
			Feedback feedback = new Feedback(	name.Text,
			                                 	email.Text,
			                                 	"website_field_not_set",
			                                 	comments.Text);
			
			Debug.WriteLine("Send message: " + feedback.submit());
			
		}
		
		public override void TouchesBegan (NSSet touches, UIEvent evt)
		{
			 // hide keyboard
			name.ResignFirstResponder();
			email.ResignFirstResponder();
			comments.ResignFirstResponder();
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			
			image.Image = UIImage.FromFile("images/tog_114.png");
			sendButton.TouchUpInside += delegate { sendMessage(); };
			
			
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

