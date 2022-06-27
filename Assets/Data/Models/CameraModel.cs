
using Assets.Catalogs.Scripts;
using Assets.Configs;
using UnityEngine;

namespace Assets.Data.Models {
    public class CameraModel {

        private readonly CameraConfig cameraConfig;
        private readonly LevelCatalogEntry levelCatalogEntry;
        private readonly Vector2Int screenBounds;

        public CameraModel(CameraConfig config, LevelCatalogEntry levelCatalogEntry) {
            this.cameraConfig = config;
            this.levelCatalogEntry = levelCatalogEntry;
            this.screenBounds = new Vector2Int(Screen.width, Screen.height);
        }

        public float GetSpeed() {
            
            return cameraConfig.Speed;
        }

        public bool CanCameraMoveVertical(Vector2 camPosition, Vector2Int direction) {

            var leftOverHeight = levelCatalogEntry.Size.y * levelCatalogEntry.TileSideLength - screenBounds.y;

            return  - direction.y * camPosition.y < leftOverHeight / 2f;
        }

        public bool CanCameraMoveHorizontal(Vector2 camPosition, Vector2Int direction) {

            var leftoverWidth = levelCatalogEntry.Size.x * levelCatalogEntry.TileSideLength - screenBounds.x;

            return camPosition.x * -direction.x < leftoverWidth / 2f;
        }

        public float GetSpeedModifier() {
            return cameraConfig.SpeedModifier;
        }
    }
}