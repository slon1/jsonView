using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace JsonViewer {
	public class ScrollController : MonoBehaviour {
		[SerializeField]
		GameObject ScrollViewGo;
		[SerializeField]
		private ScrollView scrollView;
		[SerializeField]
		private GUIController gui;
		private ScrollMod scrollMod;
		
		void Start() {
			ScrollViewGo.SetActive(false);
			JSONLoader loader = new JSONLoader((root) => {
				ScrollViewGo.SetActive(true);
				scrollMod = new ScrollMod(root);
				scrollView.OnScrollChanged += ScrollControl_OnValueCangeg;
				scrollView.OnScrollClicked += ScrollView_OnScrollClicked;
				scrollView.UpdateRecords(scrollMod.GetRecords(0, scrollView.ContentRecordCount));
			});
		}

		private void ScrollView_OnScrollClicked(JsonData record) {
			gui.SetData(record);
			gui.Show(true);
			
		}

		private void ScrollControl_OnValueCangeg(int index) {			
			scrollView.UpdateRecords(scrollMod.GetRecords(index, scrollView.ContentRecordCount));
		}
		private void OnDestroy() {
			scrollView.OnScrollChanged -= ScrollControl_OnValueCangeg;
			scrollView.OnScrollClicked -= ScrollView_OnScrollClicked;
			scrollMod.Dispose();
			HttpRequest.Clear();
		}
	}
}
