using System;
using System.Collections.Generic;
using System.Xml;
using System.Net;
using System.IO;
using System.Reflection;

namespace Tog.mobile.media
{
	public class PhotoGallery
	{
		
		private int _cursor;
		private List<Photo>	_photos;
		public List<Photo> photos {
			get { return _photos; }	
		}
		public int index {
			get { return _cursor; }	
		}
		
		public class Photo {
		
			public string path;
			public string title;
			
		}
		
		public PhotoGallery() {
			
			_photos = new List<Photo>();
			refresh();	
			
		}
		
		public Photo getNextPhoto() {
			
			if(_photos.Count == 0) {
				throw new MethodAccessException("photos have not been initialized.");
			}
			
			_cursor++;
			
			if(_cursor > _photos.Count-1) {
				_cursor = 0;	
			}
			
			return _photos[_cursor];
			
		}
		public Photo getPrevPhoto() {
			
			if(_photos.Count == 0) {
				throw new MethodAccessException("photos have not been initialized.");
			}
			
			_cursor--;
			
			if(_cursor < 0 && _photos.Count > 1) {
				_cursor = _photos.Count-1;	
			}
			
			return _photos[_cursor];
			
		}
		public Photo getCurrentPhoto() {
			
			if(_photos.Count == 0) {
				throw new MethodAccessException("photos have not been initialized.");
			}
			
			return _photos[_cursor];
			
		}
		
		public List<Photo>getPhotos() {
			
			if(_photos.Count > 0) {
				return _photos;
			}
			
			refresh();
			
			return _photos;
			
		}
		private List<Photo>getPhotosFromWeb(string urlpath) {
			
			List<Photo>newPhotos = new List<Photo>();
			WebRequest request;
			HttpWebResponse response;
			
			try {
				request	= System.Net.HttpWebRequest.Create(urlpath);
				response = (HttpWebResponse)request.GetResponse();
			} catch(Exception e) {
				return newPhotos;
				// TODO: better error handling
			}
			
			if(response.StatusCode != HttpStatusCode.OK) {
				return newPhotos;
				// TODO: better error handling
			}
		
			return parsePhotosFromStream(response.GetResponseStream());
			
		}	
		private List<Photo>getPhotosFromLocal(string filepath) {
		
			Assembly _assembly = Assembly.GetExecutingAssembly();
			Stream _stream = _assembly.GetManifestResourceStream("Tog.mobile.data.gallery.xml");
			return parsePhotosFromStream(_stream);
				
		}
		private List<Photo>parsePhotosFromStream(Stream stream) {
			
			List<Photo>newPhotos = new List<Photo>();
			
			XmlTextReader reader = new XmlTextReader(stream);
			Photo photo = new Photo();
			
			while(reader.Read()) {
				
				if(reader.NodeType == XmlNodeType.Element) {
					
					switch(reader.Name.ToLower()) {
						
						case "item":
							photo = new Photo();
							break;
						
						case "title":
							photo.title = reader.ReadString();
							break;
						
						case "link":
							photo.path = reader.ReadString();
							break;
						
					}
					
					reader.Read();
				
				} else if(reader.NodeType == XmlNodeType.EndElement && reader.Name.ToLower() == "item") {
					newPhotos.Add(photo);
				}
				
			}
			
			return newPhotos;
			
		}
		
		public List<Photo> refresh() {
		
			// attempt to get new photos from site
			// overwrite current photos
			
			_photos = getPhotosFromWeb("http://tog.ie/wp-content/plugins/nextgen-gallery/xml/media-rss.php");
			if(_photos.Count > 0) {
				return _photos;
			}
			
			_photos = getPhotosFromLocal("Tog.mobile.data.gallery.xml");
			return _photos;
			
		}
		
	}
}

