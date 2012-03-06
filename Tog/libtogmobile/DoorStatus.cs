using System;
using System.Net;
using System.IO;

namespace Tog.mobile.web
{
	public class DoorStatus
	{
		public enum DoorStatusKind {
			Open	= 1,
			Closed	= 0
		}
		
		private DoorStatusKind _status;
		
		public DoorStatusKind status {
			get { return _status; }	
		}
		
		public DoorStatus () {
			getCurrentStatus();
		}
		
		public DoorStatusKind getCurrentStatus() {
			
			// TODO: use JSON parser, not done yet due to time constraints and awesome.
			
			WebRequest request			= System.Net.HttpWebRequest.Create("http://www.tog.ie/state/");
			HttpWebResponse response	= (HttpWebResponse)request.GetResponse();
			string body = "";
			
			if(response.StatusCode != HttpStatusCode.OK) {
				_status = DoorStatusKind.Closed;
				return DoorStatusKind.Closed;
			}
			
			StreamReader sr = new StreamReader(response.GetResponseStream());
			body = sr.ReadToEnd();
			sr.Close();
			
			if(body.ToLower().IndexOf("space closed") == 0) {
				_status = DoorStatusKind.Open;	
			} else {
				_status = DoorStatusKind.Closed;
			}
			
			return _status;
			
		}
		
		public void update() {
		
			getCurrentStatus();
			
		}
		
	}
}

