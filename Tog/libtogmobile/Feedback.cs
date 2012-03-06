using System;
using System.Net;
using System.IO;
using System.Text;
using System.Reflection;

//using System.Diagnostics;

/*
 * Note: input limits are pretty arbitrary.
 */

namespace Tog.mobile.web
{
	public class Feedback
	{
		private string _name;
		private string _email;
		private string _website;
		private string _message;
		
		public string name { get { return _name; } }
		public string email { get { return _email; } }
		public string website { get { return _website; } }
		public string message { get { return _message; } }
		
		public Feedback(string a_name, string an_email, string a_website, string a_message) {
			
			setFields(a_name, an_email, a_website, a_message);
			
		}
		
		public void setFields(string a_name, string an_email, string a_website, string a_message) {
		
			setName(a_name);
			setEmail(an_email);
			setWebsite(a_website);
			setMessage(a_message);
			
		}
		public void setName(string a_name) {
			if(a_name.Length > 3 && a_name.Length < 128) {
				a_name = Uri.EscapeUriString(a_name);
				_name = a_name;
				return;
			}
			throw new System.ArgumentException("String did not meet min/max length criteria", "a_name");
		}
		public void setEmail(string an_email) {
			if(an_email.Length > 3 && an_email.Length < 512) {
				an_email = Uri.EscapeUriString(an_email);
				_email = an_email;
				return;
			}
			throw new System.ArgumentException("String did not meet min/max length criteria", "an_email");
		}
		public void setWebsite(string a_website) {
			if(a_website.Length > 3 && a_website.Length < 1024) {
				a_website = Uri.EscapeUriString(a_website);
				_website = a_website;
				return;
			}
			throw new System.ArgumentException("String did not meet min/max length criteria", "a_website");
		}
		public void setMessage(string a_message) {
			if(a_message.Length > 3 && a_message.Length < 4096) {
				a_message = Uri.EscapeUriString(a_message);
				_message = a_message;
				return;
			}
			throw new System.ArgumentException("String did not meet min/max length criteria", "a_message");
		}
			
		public bool submit() {
		
			string data = "";	// TODO: add info about source of POST message?
			
			return submitPost(data, "http://www.tog.ie/contact-2/#wpcf7-f2421-p52-o1" + getUrlEncodedVars());
			
			//return submitPost(data, "http://hroch486.icpf.cas.cz/cgi-bin/echo.pl");
			
		}
		
		private string getUrlEncodedVars() {
		
			string output = "?";
			bool hasVar = false;
			
			if(_name.Length > 0) {
				output += "your-name=" + _name;
				hasVar = true;
			}
			
			if(_email.Length > 0) {
				if(hasVar) {
					output += "&";	
				}
				output += "your-email=" + _email;
				hasVar = true;
			}
			
			if(_website.Length > 0) {
				if(hasVar) {
					output += "&";	
				}
				output += "your-url=" + _website;
				hasVar = true;
			}
			
			if(_message.Length > 0) {
				if(hasVar) {
					output += "&";	
				}
				output += "your-message=" + _message;
				hasVar = true;
			}
			
			return output;
			
		}
		private bool submitPost(string data, string url) {
			
			try {
				
				byte[] buffer = Encoding.ASCII.GetBytes(data);
				WebRequest request = System.Net.HttpWebRequest.Create(url + getUrlEncodedVars());
				request.Method = "POST";
				request.ContentType = "application/x-www-form-urlencoded";
				request.ContentLength = buffer.Length;
				
				Stream postData = request.GetRequestStream();
				postData.Write(buffer, 0, buffer.Length);
				postData.Close();
				
				HttpWebResponse response = (HttpWebResponse)request.GetResponse();
				
				/*
				StreamReader sr = new StreamReader(response.GetResponseStream());
				Debug.WriteLine(sr.ReadToEnd());
				sr.Close();
				*/
				
				return (response.StatusCode != HttpStatusCode.OK);
				
			} catch (Exception e) {
				
				// TODO: better error handling
				
				return false;
				
			}
			
		}
		
	}
}

