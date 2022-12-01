using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JsonViewer {
    
    [System.Serializable]
    public class JsonData {
        public int id;
        public string first_name;
        public string last_name;
        public string email;
        public string gender;
        public string ip_address;
        public string address;
    }
    [System.Serializable]
    public class JsonRoot {
        public List<JsonData> Root;
		public JsonRoot() {
            Root = new List<JsonData>();
		}
	}
    public class JSONLoader  {
        private const string url = "https://drive.google.com/uc?export=download&id=1MWptUq7lb78W-8dI6uQ7mbNEuKTJRVZB";
		private const string prefix = @"{""Root"":";
		private const string postfix = @"}";
        private Action<JsonRoot> onCompleted;
		public JSONLoader(Action<JsonRoot> OnCompleted) {
            onCompleted = OnCompleted;
            HttpRequest.Get(url, OnResponse);
        }				

		private void OnResponse(string json) {            
            onCompleted?.Invoke (JsonUtility.FromJson<JsonRoot>(prefix + json + postfix));            
        }

		
    }
}