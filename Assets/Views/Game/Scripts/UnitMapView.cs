using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Views {
    public class UnitMapView : MonoBehaviour {

        [SerializeField]
        private GameObject selectedDirector;

        [SerializeField]
        private Image image;

        [SerializeField]
        private float speed;

        public event Action OnMovementEnd;

        private const float duration = 2f;

        public void SetViewData(Sprite unitSprite){
            image.sprite = unitSprite;
        }

        public void SetSelectStatus(bool selected) {
            selectedDirector.SetActive(selected);
        }

        public void MoveUnitViewTo(Vector2 newPosition) {

            MoveUnitAsyncTo(newPosition);
        }

        private async void MoveUnitAsyncTo(Vector2 newPosition) {

            var timer = 0f;
            var rectTransform = transform as RectTransform;
            var initialPosition = rectTransform.anchoredPosition;

            while (timer < 2f) {

                timer += Time.smoothDeltaTime;
                var position = Vector2.Lerp(initialPosition, newPosition, timer / duration);
                rectTransform.anchoredPosition = position;

                await Task.Yield();
            }

            OnMovementEnd?.Invoke();
        }
    }
}