using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.Views {
	public class DialogSampleView : MonoBehaviour {

		[SerializeField]
		private TextMeshProUGUI textMessage;

		[SerializeField]
		private float letterSpeed; 

		private string cachedMessage;

		private Coroutine textCoroutine;

		private bool isFinished;
		public bool IsFinished => isFinished;

		public void SetMessage(string message) {
			cachedMessage = message;

			textCoroutine = StartCoroutine(StartTextAnimationCR());
		}

		private IEnumerator StartTextAnimationCR() {

			isFinished = false;

			for (int i = 0; i < cachedMessage.Length; i++) {
				
				var messageToDisplay = cachedMessage.Substring(0, i);

				textMessage.text = messageToDisplay;

				yield return new WaitForSeconds(1/letterSpeed);
			}


			isFinished = true;
		}

		public void Complete() {
			if(isFinished) {
				throw new NotSupportedException("Shouldnt be able to complete a null coroutine, check if its finished first");
			}

			StopCoroutine(textCoroutine);

			isFinished = true;

			textMessage.text = cachedMessage;
		}
	}
}