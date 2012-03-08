using System;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
		
        /**
         * @author Dave Kerr
         * Implemented tog API to get the open or close status of the 
         * Tog hackerspace
         */
		public DoorStatusKind getCurrentStatus() {

            WebClient client = new WebClient();
            string json = client.DownloadString("http://www.tog.ie/cgi-bin/space");
		
	    //Json parsers from newtonsoft.json
            JsonSerializer serializer = new JsonSerializer();
            JObject o = JObject.Parse(json);
            
            Boolean status = (Boolean)o["open"];

            client.Dispose();
            o = null;
            serializer = null;

            if(status){
                _status = DoorStatusKind.Open;
                return DoorStatusKind.Open;
            }
            
            else{
				_status = DoorStatusKind.Closed;
				return DoorStatusKind.Closed;
			}
		}
		
		public void update() {

			getCurrentStatus();			
		}		
	}
}