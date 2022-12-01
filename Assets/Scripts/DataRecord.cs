using UnityEngine;
using UnityEngine.UI;
namespace JsonViewer {
	public class DataRecord : MonoBehaviour {
        
        [SerializeField]
        private Text text;
        [SerializeField]
        private RectTransform rect;        
        [SerializeField]
        private Button button;        
        
        public void SetPosition(Vector2 pos) {
            rect.anchoredPosition -= pos;
        }
        public void SetText( string s) {
            text.text = s;
		}
        public Button GetButton() {
            return button;
		}
        
    }
}