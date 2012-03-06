using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Reflection;

using System.Diagnostics;

namespace Tog.mobile.maps
{
	
	public enum POI_Kind {
	
		Unknown			= -1,
		Hackerspace,
		MagicUnicorns
		
	}
	
	public class GPS_Location {
		public string longitude = "undefined";
		public string latitude = "undefined";
	}
	
	public class PointOfInterest
	{
		public string title;
		public string description;
		public string website;
		public string twitter;
		public string address;
		public GPS_Location location;
		
		public PointOfInterest() {
			location = new GPS_Location();	
		}
		
		public static List<PointOfInterest>getPoints(POI_Kind kind) {
			
			Assembly _assembly = Assembly.GetExecutingAssembly();
			Stream _stream;
			
			switch(kind) {

				default:
				case POI_Kind.Unknown:
					throw new ArgumentException("No path exists for POI_KIND of kind 'Unknown'.");
				
				case POI_Kind.Hackerspace: 
					_stream = _assembly.GetManifestResourceStream("Tog.mobile.data.poi_hackerspaces.xml");
					break;
				
			}
			
			List<PointOfInterest>points = new List<PointOfInterest>();
			
			XmlTextReader reader = new XmlTextReader(_stream);
			PointOfInterest poi = new PointOfInterest();
			
			bool hasNewPoi = false;
			while(reader.Read()) {
				
				if(reader.NodeType == XmlNodeType.Element) {
					
					switch(reader.Name.ToLower()) {
						
						case "poi":
							poi = new PointOfInterest();
							hasNewPoi = true;
							break;
						
						case "title":
							poi.title = reader.ReadString();
							break;
						
						case "description":
							poi.description = reader.ReadString();
							break;
						
						case "address":
							poi.address = reader.ReadString();
							break;
						
						case "website":
							poi.website = reader.ReadString();
							break;
						
						case "latitude":
							poi.location.latitude = reader.ReadString();
							break;
						
						case "longitude":
							poi.location.longitude = reader.ReadString();
							break;
						
					}
					
					reader.Read();
					
				} else if(reader.NodeType == XmlNodeType.EndElement && hasNewPoi) {
					points.Add(poi);
					//poi = new PointOfInterest();
					hasNewPoi = false;
				}
				
			}
			
			return points;
			
		}
		
	}
	
}

