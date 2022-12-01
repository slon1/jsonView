using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace JsonViewer {
	public class ScrollView : MonoBehaviour {

		public float RecordHeight = 30;
		public int ContentRecordCount = 7;
		public ScrollRect Scroll;
		public GameObject RecordPrefab;
		public int RecordMax = 3000;

		public event Action<int> OnScrollChanged;
		public event Action<JsonData> OnScrollClicked;

		private int topRec, bottonRec, index;
		private float ContentHeight=> ContentRecordCount * RecordHeight;		
		private DataRecord[] recordDatas;
		private int recordCount => RecordMax - ContentRecordCount;


		void Start() {			
			topRec = 0;			
			bottonRec = ContentRecordCount;
			Scroll.onValueChanged.AddListener(OnValueChanged);
			Scroll.content.sizeDelta = new Vector2(0, RecordMax * RecordHeight);
			recordDatas = new DataRecord[ContentRecordCount];
			for (int i = 0; i < ContentRecordCount; i++) {
				recordDatas[i]= Instantiate(RecordPrefab, Scroll.content).GetComponent<DataRecord>();
				recordDatas[i].SetPosition ( new Vector2(0, RecordHeight * i));			
			}
		}
		
		public void UpdateRecords(List<JsonData> records) {
			int count = topRec;
			for (int i = 0; i < ContentRecordCount; i++) {
				count = (topRec + i) % ContentRecordCount;				
				AddListener(recordDatas[count].GetButton(), records[i]);
				recordDatas[count].SetText( records[i].first_name);
			}
		}
		private void AddListener(Button b, JsonData data) {
			b.onClick.RemoveAllListeners();
			b.onClick.AddListener(()=> OnScrollClicked?.Invoke(data));
		}		

		private void OnValueChanged(Vector2 value) {
			int newIndex = Mathf.FloorToInt((1f - value.y) * recordCount);
			int dt = Mathf.Abs(newIndex - index);
			bool dir = newIndex > index;
			if (newIndex != index) {
				for (int i = 0; i < dt; i++) {
					recordDatas[dir ? topRec : bottonRec].SetPosition( new Vector2(0, ContentHeight * (dir ? 1 : -1)));					
					UpdateTopBottom(dir);					
				}
				OnScrollChanged?.Invoke(index = newIndex);
			}
		}
		
		private void UpdateTopBottom(bool dir) {
			if (dir) {
				bottonRec = topRec;
				topRec = (topRec + 1) % ContentRecordCount;
			}
			else {
				topRec = bottonRec;
				bottonRec = (bottonRec > 0 ? bottonRec : ContentRecordCount) - 1;
			}

		}

		private void OnDestroy() {			
			Scroll.onValueChanged.RemoveListener(OnValueChanged);
			recordDatas = null;
		}
	}
}