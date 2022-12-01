using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JsonViewer {
	public class ScrollMod : IDisposable {
		
		private Dictionary<int, JsonData> records = new Dictionary<int, JsonData>();		
		public ScrollMod(JsonRoot root) {			
			for (int i = 0; i < root.Root.Count; i++) {
				records.Add(i, root.Root[i]);
			}
		}
		public List<JsonData> GetRecords (int start, int count) {
			return records.Values.Skip(start).Take(count).ToList();
		}
		public void Dispose() {
			records.Clear();
			records = null;

		}
	}
}