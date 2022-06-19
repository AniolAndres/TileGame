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

            if(!cameraModel.CanCameraMoveHorizontal(cameraView.Position, direction)) {
                direction.x = 0;
            }

            if (!cameraModel.CanCameraMoveVertical(cameraView.Position, direction)) {
                direction.y = 0;
            }

            if(direction == Vector2Int.zero) {
                return;
            }

            var speed = cameraModel.GetSpeed();

            var speedModifier = cameraModel.GetSpeedModifier();

            cameraView.MoveCamera(direction, speed, speedModifier);
        }

        public void Destroy() {
            cameraView.OnCameraMoved -= MoveCamera;
        }
    }
}