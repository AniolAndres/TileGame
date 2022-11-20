using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class UnitMapView : MonoBehaviour {

        [SerializeField]
        private GameObject selectedDirector;

        [SerializeField]
        private Image image;

        public event Action OnMovementEnd;

        private const float duration = 3f;

        public void SetViewData(Sprite unitSprite){
            image.sprite = unitSprite;
        }

        public void SetSelectStatus(bool selected) {
            selectedDirector.SetActive(selected);
        }

        public void MoveUnitViewTo(List<Vector2> pathPositions) {

            StartCoroutine(MoveUnitAsyncTo(pathPositions));
        }

        private IEnumerator MoveUnitAsyncTo(List<Vector2> pathPositions) { //Maybe a coroutine would be better

            var timer = 0f;
            var rectTransform = transform.AsRectTransform();
            var initialPosition = rectTransform.anchoredPosition;

            for (int i = 0; i < pathPositions.Count; i++) {
                Vector2 nextPosition = pathPositions[i];
                
                while(timer > duration) {

                }

            }

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