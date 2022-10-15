
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScreenMachine {

    public abstract class UiView : MonoBehaviour {

        private Canvas uiViewCanvas;

        private GraphicRaycaster graphicRaycaster;

        private void Awake() {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
            uiViewCanvas = GetComponent<Canvas>();
        }

        public virtual void OnUpdate() { }

        public void DisableRaycast() {
            graphicRaycaster.enabled = false;
        }

        public void EnableRaycast() {
            graphicRaycaster.enabled = true;
        }

        public void SetCanvasOrder(int v) {
            uiViewCanvas.sortingOrder = v;
        }
    }
}