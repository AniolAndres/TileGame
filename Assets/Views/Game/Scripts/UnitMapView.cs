using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class UnitMapView : MonoBehaviour {

        [SerializeField]
        private GameObject selectedDirector;

        [SerializeField]
        private Image image;

        public event Action OnMovementEnd;

        public event Action OnMovementStart;

        private const float durationPerSquare = 0.2f;

        public void SetViewData(Sprite unitSprite){
            image.sprite = unitSprite;
        }

        public void SetSelectStatus(bool selected) {
            selectedDirector.SetActive(selected);
        }

        public void MoveUnitViewTo(Vector2Int firstGridPosition, List<Vector2Int> gridPositions, List<Vector2> pathPositions) {

            StartCoroutine(MoveUnitAsyncTo(firstGridPosition, gridPositions, pathPositions));
        }

        private IEnumerator MoveUnitAsyncTo(Vector2Int firstGridPosition, List<Vector2Int> gridPositions, List<Vector2> pathPositions) { //Maybe a coroutine would be better

            if(pathPositions.Count != gridPositions.Count) {
                throw new NotSupportedException($"Somehow there are gridPositions: {gridPositions.Count} and pathPositions: {pathPositions.Count}, they should be equal ");
            }

            OnMovementStart?.Invoke();

            var rectTransform = transform.AsRectTransform();
            var initialPosition = rectTransform.anchoredPosition;
            var initialGridPosition = firstGridPosition;

            for (int i = 0; i < pathPositions.Count; i++) {

                var timer = 0f;

                var nextPosition = pathPositions[i];
                var nextGridPosition = gridPositions[i];

                var duration = Vector2.Distance(initialGridPosition, nextGridPosition) * durationPerSquare;
                
                while(timer < duration) {

                    timer += Time.smoothDeltaTime;
                    if (timer > duration) {
                        timer = duration;
                    }

                    var position = Vector2.Lerp(initialPosition, nextPosition, timer / duration);
                    rectTransform.anchoredPosition = position;
                    yield return null;
                }

                initialPosition = nextPosition;
                initialGridPosition = nextGridPosition;
            }

            rectTransform.anchoredPosition = pathPositions.Last();

            OnMovementEnd?.Invoke();
        }

        public void MoveOnCancelTo(Vector2 realEndPosition) {
			var rectTransform = transform.AsRectTransform();
			rectTransform.anchoredPosition = realEndPosition;

		}
    }
}
