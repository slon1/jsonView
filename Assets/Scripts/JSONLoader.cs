using System;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace ScrollViewer {

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
		private const string prefix = @"{""Root"":";
		private const string postfix = @"}";       
		public JSONLoader(string url, Action<JsonRoot> OnCompleted) {
            
            RequestHelper.HttpGet(url, (json)=> OnCompleted?.Invoke(JsonUtility.FromJson<JsonRoot>(prefix + json + postfix)),OnHttpFail);            
        }

        private void OnHttpFail(string error) {
            Debug.Log(error);

        }
    }
}