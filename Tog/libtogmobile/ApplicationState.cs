using System;
using System.Collections.Generic;

using Tog.mobile;
using Tog.mobile.maps;
using Tog.mobile.web;
using Tog.mobile.media;

namespace Tog.mobile
{
	public delegate void onStateChange( ApplicationState.AvailableStates newstate);
	
	public class ApplicationState
	{
		#region Singleton
		private static ApplicationState instance;
		
		private ApplicationState() {
			
			doorStatus = new DoorStatus();
			
		}
		
		public static ApplicationState Instance
		{
			get 
			{
				if (instance == null)
				{
					instance = new ApplicationState();
				}
				return instance;
			}
		}
		#endregion
		
		#region members
		public ApplicationState.AvailableStates state  	= AvailableStates.Unknown;
		public onStateChange onStateChangeDelegate		= null;
		public DoorStatus doorStatus					= null;
		public List<TwitterUser>users					= null;
		public PhotoGallery gallery						= null;
		public List<PointOfInterest>poi_hackerspaces	= null;
		#endregion
		
		public enum AvailableStates {
			
			Unknown			= -1,
			Main,
			PhotoGallery,
			Twitter,
			Feedback,
			Maps
			
		}
		
		public void setState(ApplicationState.AvailableStates newstate) {
			
			switch(newstate) {
			/*
				default:
				case AvailableStates.Unknown:
					// here be dragons.
					return;
				*/
				case AvailableStates.Main:
					doorStatus.update();
					return;
			
				case AvailableStates.Twitter:
					users = Twitter.getUsersForList("tog_dublin","members");
					return;
				
				case AvailableStates.PhotoGallery:
					if(gallery == null) {
						gallery = new PhotoGallery();
					}
					return;
			
				case AvailableStates.Maps:
					if(poi_hackerspaces == null) {
						poi_hackerspaces = PointOfInterest.getPoints(POI_Kind.Hackerspace);
					}
					return;
				
			}
			
			if(onStateChangeDelegate != null) {
				onStateChangeDelegate(newstate);	
			}
			
		}
		
	}
}

