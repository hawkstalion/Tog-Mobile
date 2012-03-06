using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;

using System.Diagnostics;

namespace Tog_iOS
{
	
	public class TableViewDelegate : UITableViewDelegate {
		
		public override float GetHeightForRow (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath) {
			
			return 94.0f;
			
		}
		
		public override void RowSelected (UITableView tableView, NSIndexPath indexPath) {
			
			
		}
		
	}
	
	public class TableViewDataSource : UITableViewDataSource {
		
		
		public override UITableViewCell GetCell (UITableView tableView, MonoTouch.Foundation.NSIndexPath indexPath) {
			
			UITableViewCell cell = tableView.DequeueReusableCell("MainOptionCellIdent");
			
			if (cell == null) {
				//cell = new UITableViewCell (UITableViewCellStyle.Default, "MainOptionCellIdent");
				MainOptionCellController controller = new MainOptionCellController();
				cell = (UITableViewCell)controller.View;
			}
		
			//BasicTableViewItem item = this._tableItems[indexPath.Section].Items[indexPath.Row];
			

			return cell;
			
			/*
			//UITableViewCell cell;
			
			MainOptionCell cell = new MainOptionCell();
			return cell; 
			
			//MainOptionCellController controller = new MainOptionCellController();
			
			//controller.image.Image = UIImage.FromFile("images/about_tog_img.png");
			cell = (UITableViewCell)controller.View;
			
			return cell;
			*/
		}
		
		
		public override int RowsInSection (UITableView tableView, int section) {
		
			return 3;
			
		}
		
	}
	
	public partial class MainView : UIViewController
	{
		
		#region IBOutlets
		
		[Connect("mainImage")]
		private UIImageView mainImage {
		    get { return ((UIImageView) (this.GetNativeField("mainImage"))); }
		    set { this.SetNativeField("mainImage", value);}
		}
		
		[Connect("secondaryImage")]
		private UIImageView secondaryImage {
		    get { return ((UIImageView) (this.GetNativeField("secondaryImage"))); }
		    set { this.SetNativeField("secondaryImage", value);}
		}
		
		[Connect("hackerGlyph")]
		private UIImageView hackerGlyph {
		    get { return ((UIImageView) (this.GetNativeField("hackerGlyph"))); }
		    set { this.SetNativeField("hackerGlyph", value);}
		}
		
		[Connect("tableView")]
		private UITableView tableView {
		    get { return ((UITableView) (this.GetNativeField("tableView"))); }
		    set { this.SetNativeField("tableView", value);}
		}
		
		#endregion
		
		#region IBActions
		
		[Export ("onTouchWebsiteLabel:")]
		public void onTouchWebsiteLabel (int arg) {
		
		}
		
		#endregion
		
		public MainView () : base ("MainView", null)
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
			
			// tableview
			TableViewDataSource ds = new TableViewDataSource();
			tableView.DataSource = ds;
			
			TableViewDelegate del = new TableViewDelegate();
			tableView.Delegate = del;
			
			//mainImage.BackgroundColor = UIColor.Green;
			mainImage.Image = UIImage.FromFile("images/about_tog_img.png");
			secondaryImage.Image = UIImage.FromFile("images/map-marker.png");
			hackerGlyph.Image = UIImage.FromFile("images/glider.png");
			
			
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

