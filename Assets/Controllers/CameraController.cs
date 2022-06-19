using UnityEngine;
using Assets.Views.Game;
using Assets.Data.Models;

namespace Assets.Controllers {
    public class CameraController {

        private readonly CameraView cameraView;

        private readonly CameraModel cameraModel;

        public CameraController(CameraView cameraView, CameraModel model) {
            this.cameraView = cameraView;
            this.cameraModel = model;
        }
        
        public void Init() {
            cameraView.OnCameraMoved += MoveCamera;
        }

        private void MoveCamera(Vector2Int direction) {
            //check if it's okay to move it blabla

            if (!cameraModel.CanCameraMove(cameraView.Position)) {
                return;
            }

            var speed = cameraModel.GetSpeed();

            cameraView.MoveCamera(direction, speed);
        }

        public void Destroy() {
            cameraView.OnCameraMoved -= MoveCamera;
        }
    }
}