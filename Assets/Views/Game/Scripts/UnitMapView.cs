using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class UnitMapView : MonoBehaviour {

        [SerializeField]
        private GameObject selectedDirector;

        [SerializeField]
        private Image image;

        public event Action OnMovementEnd;

        private const float duration = 0.5f;

        public void SetViewData(Sprite unitSprite){
            image.sprite = unitSprite;
        }

        public void SetSelectStatus(bool selected) {
            selectedDirector.SetActive(selected);
        }

        public void MoveUnitViewTo(Vector2 newPosition) {

            StartCoroutine(MoveUnitAsyncTo(newPosition));
        }

        private IEnumerator MoveUnitAsyncTo(Vector2 newPosition) { //Maybe a coroutine would be better

            var timer = 0f;
            var rectTransform = transform.AsRectTransform();
            var initialPosition = rectTransform.anchoredPosition;

            while (timer < duration) {

                timer += Time.smoothDeltaTime;
                if(timer > duration) {
                    timer = duration;
                }
                var position = Vector2.Lerp(initialPosition, newPosition, timer / duration);
                rectTransform.anchoredPosition = position;

                yield return null;
            }
            
            rectTransform.anchoredPosition = newPosition;

            OnMovementEnd?.Invoke();
        }
    }
}