using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;

namespace ScrollViewer {
	
	public class ScrollController : MonoBehaviour {
		[SerializeField]
		GameObject ScrollViewGo;
		[SerializeField]
		private ScrollView scrollView;
		[SerializeField]
		private Gui gui;
		private ScrollDB scrollDB;
		[SerializeField]
		private const string url = "https://drive.google.com/uc?export=download&id=1MWptUq7lb78W-8dI6uQ7mbNEuKTJRVZB";

		void Start() {
			ScrollViewGo.SetActive(false);
			JSONLoader loader = new JSONLoader(url, (root) => {
				ScrollViewGo.SetActive(true);
				scrollDB = new ScrollDB(root);
				scrollView.OnScrollChanged += ScrollControl_OnValueCangeg;
				scrollView.OnScrollClicked += ScrollView_OnScrollClicked;
				scrollView.UpdateRecords(scrollDB.GetRecords(0, scrollView.ContentRecordCount));
			});
		}

		private void ScrollView_OnScrollClicked(JsonData record) {
			gui.SetData(record);
			gui.Show(true);
			
		}

		private void ScrollControl_OnValueCangeg(int index) {			
			scrollView.UpdateRecords(scrollDB.GetRecords(index, scrollView.ContentRecordCount));
		}
		private void OnDestroy() {
			scrollView.OnScrollChanged -= ScrollControl_OnValueCangeg;
			scrollView.OnScrollClicked -= ScrollView_OnScrollClicked;
			scrollDB.Dispose();
			RequestHelper.Dispose();
			
		}
	}
}
