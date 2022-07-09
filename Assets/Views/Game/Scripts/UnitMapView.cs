using UnityEngine;

namespace Assets.Views {
    public class UnitMapView : MonoBehaviour {

        [SerializeField]
        private GameObject selectedDirector;

        public void SetSelectStatus(bool selected) {
            selectedDirector.SetActive(selected);
        }

    }
}