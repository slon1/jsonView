using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace JsonViewer {
	public class HttpRequest {
		static CancellationTokenSource tokenSource = new CancellationTokenSource();
		
		private static float timestamp;
		public static void Clear() {
			tokenSource.Cancel();
		}

		public static void Get(string url, Action<string> callback) {
			CancellationToken token = tokenSource.Token;
			float time = Time.time;
			GetAsync(url, callback, time, token); 
		}

		private static async Task GetAsync(string url, Action<string> callback, float time, CancellationToken token) {

			float delay = 2f;
			float dt = delay - (time - timestamp);
			if (dt > 0) {		
				await Task.Delay((int)(dt * 1000));
			}
			timestamp = time;
			if (token.IsCancellationRequested)
				return;

			using (UnityWebRequest webRequest = UnityWebRequest.Get(url)) {

				var operation = webRequest.SendWebRequest();
				while (!operation.isDone) {
					if (token.IsCancellationRequested) {
						webRequest.Abort();
						return;
					}
					await Task.Yield();
				}
				switch (webRequest.result) {
					case UnityWebRequest.Result.ConnectionError:
					case UnityWebRequest.Result.DataProcessingError:
						Debug.LogError(url + ": Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.ProtocolError:
						Debug.LogError(url + ": HTTP Error: " + webRequest.error);
						break;
					case UnityWebRequest.Result.Success:						
						callback?.Invoke(webRequest.downloadHandler.text);
						break;
				}
			}
		}
	}
}