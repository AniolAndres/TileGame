
using UnityEngine;
using UnityEngine.UI;

namespace Assets.ScreenMachine {

    public abstract class UiView : MonoBehaviour {

        private GraphicRaycaster graphicRaycaster;

        private void Awake() {
            graphicRaycaster = GetComponent<GraphicRaycaster>();
        }

        public virtual void OnUpdate() { }

        public void DisableRaycast() {
            graphicRaycaster.enabled = false;
        }

        public void EnableRaycast() {
            graphicRaycaster.enabled = true;
        }
    }
}