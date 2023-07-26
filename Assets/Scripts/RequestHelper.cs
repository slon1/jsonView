using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Utils {	
	public static class RequestHelper {
		private static Dictionary<int, (Action<string>, Action<string>)> hash = new Dictionary<int, (Action<string>, Action<string>)>();
		private static CancellationTokenSource tokenSource;
		static RequestHelper() {
			tokenSource = new CancellationTokenSource();
		}

		public static void HttpGet(string url, Action<string> OnSuccess, Action<string> OnFail) {
			HttpGetAsync(url, OnSuccess, OnFail);
		}
		private static async void HttpGetAsync(string url, Action<string> OnSuccess, Action<string> OnFail) {
			CancellationToken token = tokenSource.Token;

			using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {
				hash.Add(webRequest.GetHashCode(), (OnSuccess, OnFail));				
				var operation = webRequest.SendWebRequest();
				while (!operation.isDone && !token.IsCancellationRequested) {
					await Task.Yield();
				}
				switch (webRequest.result) {
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
					case UnityWebRequest.Result.ProtocolError:
						hash[webRequest.GetHashCode()].Item2?.Invoke(webRequest.error);
						break;
					case UnityWebRequest.Result.Success:
						hash[webRequest.GetHashCode()].Item1?.Invoke(webRequest.downloadHandler.text);
						break;
				}
				hash.Remove(webRequest.GetHashCode());
			}
		}

		public static void Dispose() {
			tokenSource.Cancel();
			hash?.Clear();
			hash = null;
		}
	}

}