using UnityEngine;
using System;
using Assets.Extensions;

namespace Assets.Views.Game {
    public class CameraView : MonoBehaviour {

        public event Action<Vector2Int> OnCameraMoved;

        public Vector2 Position => mapHolder.transform.localPosition;

        [SerializeField]
        private GameObject mapHolder;

        private void Update() {

            var direction = Vector2Int.zero;

            if (Input.GetKey(KeyCode.W)) {
                direction += Vector2Int.up;
            }

            if (Input.GetKey(KeyCode.S)) {
                direction += Vector2Int.down;
            }

            if (Input.GetKey(KeyCode.A)) {
                direction += Vector2Int.left;
            }

            if (Input.GetKey(KeyCode.D)) {
                direction += Vector2Int.right;
            }

            if(direction == Vector2Int.zero) {
                return;
            }

            OnCameraMoved?.Invoke(direction);
        }

        public void MoveCamera(Vector2Int direction, float speed) {
            mapHolder.transform.position -= direction.ToVector3() * speed * Time.smoothDeltaTime;
        }
    }
}