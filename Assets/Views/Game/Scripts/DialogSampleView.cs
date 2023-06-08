using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Views {
	public class DialogSampleView : MonoBehaviour {

		[SerializeField]
		TextMeshProUGUI textMessage;

		public void SetMessage(string message) {
			textMessage.text = message;
		}
	}
}