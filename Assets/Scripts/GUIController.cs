using UnityEngine;
using UnityEngine.UI;

namespace JsonViewer {
	public class GUIController : MonoBehaviour {
        [SerializeField]
        private GameObject go;
        [SerializeField]
        private Text id;
        [SerializeField]
        private Text first_name;
        [SerializeField]
        private Text last_name;
        [SerializeField]
        private Text email;
        [SerializeField]
        private Text gender;
        [SerializeField]
        private Text ip_address;
        [SerializeField]
        private Text address;
        public void SetData (JsonData data ) {
            id.text = data.id.ToString();
            first_name.text = data.first_name;
            last_name.text = data.last_name;
            email.text = data.email;
            ip_address.text = data.ip_address;
            address.text = data.address;
		}
        public void Show(bool show) {
            go.SetActive(show);
        }
       
    }
}