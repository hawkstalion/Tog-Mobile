using System;
using System.Xml;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Tog.mobile.web
{
	
	#region Data Types
	public class TwitterUser {
		
		public string id;
		public string screen_name;
		public string name;
		public string profile_image_url;
		
		public TwitterStatus status;
		
	}
	
	public class TwitterStatus {
		
		public string id;
		public string text;
		public string created_at;
		
		public TwitterUser user;
		
	}
	#endregion
	
	public class Twitter
	{
		
		public static List<TwitterStatus>getStatusesForUser(string screen_name) {
			
			string path = "http://api.twitter.com/1/statuses/user_timeline.xml?screen_name=" + Uri.EscapeUriString(screen_name);
			return parseTwitterStatuses(path);
			
		}
		public static List<TwitterUser>getUsersForList(string screen_name, string slug) {
			
			string path = "https://api.twitter.com/1/lists/members.xml?slug=" + Uri.EscapeUriString(slug) + "&owner_screen_name=" + Uri.EscapeUriString(screen_name);
			return parseTwitterUsers(path);
			
		}
	
		// TODO: remove redunant code.
		public static List<TwitterUser>parseTwitterUsers(string urlpath) {
			
			List<TwitterUser>users = new List<TwitterUser>();
			WebRequest request;
			HttpWebResponse response;
			
			try {
				request	= System.Net.HttpWebRequest.Create(urlpath);
				response = (HttpWebResponse)request.GetResponse();
			} catch(Exception e) {
				return users;
				// TODO: better error handling
			}
			
			if(response.StatusCode != HttpStatusCode.OK) {
				return users;
				// TODO: better error handling
			}
			
			XmlTextReader reader = new XmlTextReader(response.GetResponseStream());
			TwitterStatus status = new TwitterStatus();
			TwitterUser user = new TwitterUser();
			
			bool insideUserElement = false;
			bool insideStatusElement = false;
			while(reader.Read()) {
				
				if(reader.NodeType == XmlNodeType.Element) {
					
					if(insideStatusElement) {
						
						switch(reader.Name.ToLower()) {
							
							case "id":
								status.id = reader.ReadString();
								break;
							
							case "text":
								status.text = reader.ReadString();
								break;
							
							case "created_at":
								status.created_at = reader.ReadString();
								break;
							
							
						}
						
					} else if(insideUserElement) {
						
						switch(reader.Name.ToLower()) {
							
							case "id":
								user.id = reader.ReadString();
								break;
							
							case "screen_name":
								user.screen_name = reader.ReadString();
								break;
							
							case "name":
								user.name = reader.ReadString();
								break;
							
							case "profile_image_url":
								user.profile_image_url = reader.ReadString();
								break;
							
							case "status":
								status = new TwitterStatus();
								insideStatusElement = true;
								break;
								
							
						}
						
					} else {
						
						switch(reader.Name.ToLower()) {
							
							case "user":
								user = new TwitterUser();
								insideUserElement = true;
								break;
							
						}
						
					}
					
				} else if(reader.NodeType == XmlNodeType.EndElement) {
				
					switch(reader.Name.ToLower()) {
						
						case "status":
							insideStatusElement = false;
							user.status = status;
							break;
						
						case "user":
							insideUserElement = false;
							users.Add(user);
							break;
						
					}	
				
				}
				
			}
			
			return users;
			
		}
		
		// TODO: be happy.
		public static List<TwitterStatus>parseTwitterStatuses(string urlpath) {
			
			List<TwitterStatus>statuses = new List<TwitterStatus>();
			WebRequest request;
			HttpWebResponse response;
			
			try {
				request	= System.Net.HttpWebRequest.Create(urlpath);
				response = (HttpWebResponse)request.GetResponse();
			} catch(Exception e) {
				return statuses;
				// TODO: better error handling
			}
			
			if(response.StatusCode != HttpStatusCode.OK) {
				return statuses;
				// TODO: better error handling
			}
			
			XmlTextReader reader = new XmlTextReader(response.GetResponseStream());
			TwitterStatus status = new TwitterStatus();
			TwitterUser user = new TwitterUser();
			
			bool insideUserElement = false;
			bool insideStatusElement = false;
			while(reader.Read()) {
				
				if(reader.NodeType == XmlNodeType.Element) {
					
					if(insideUserElement) {
						
						switch(reader.Name.ToLower()) {
							/*
							case "user":
								user = new TwitterUser();
								insideUserElement = true;
								break;
							*/
							case "id":
								user.id = reader.ReadString();
								break;
							
							case "screen_name":
								user.screen_name = reader.ReadString();
								break;
							
							case "name":
								user.name = reader.ReadString();
								break;
							
							case "profile_image_url":
								user.profile_image_url = reader.ReadString();
								break;
							
						}
						
					} else if(insideStatusElement) {
						
						switch(reader.Name.ToLower()) {
							
							case "id":
								status.id = reader.ReadString();
								break;
							
							case "text":
								status.text = reader.ReadString();
								break;
							
							case "created_at":
								status.created_at = reader.ReadString();
								break;
							
							case "user":
								user = new TwitterUser();
								insideUserElement = true;
								break;
							
						}
						
					} else {
						
						switch(reader.Name.ToLower()) {
							
							case "status":
								status = new TwitterStatus();
								insideStatusElement = true;
								break;
							
						}
						
					}
					
				} else if(reader.NodeType == XmlNodeType.EndElement) {
				
					switch(reader.Name.ToLower()) {
						
						case "status":
							insideStatusElement = false;
							statuses.Add(status);
							break;
						
						case "user":
							insideUserElement = false;
							status.user = user;
							break;
						
					}	
				
				}
				
			}
			
			return statuses;
			
		}

	}
}

