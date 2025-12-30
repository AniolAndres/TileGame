
using Assets.Catalogs;
using Assets.Configs;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Assets.Data.Models {
    public class CameraModel {

        private readonly CameraConfig cameraConfig;
        private readonly LevelCatalogEntry levelCatalogEntry;
        private readonly MapData mapData;
        private readonly Vector2Int screenBounds;

        public CameraModel(CameraConfig cameraConfig, MapData mapData) {
            this.cameraConfig = cameraConfig;
            this.mapData = mapData;
            this.screenBounds = new Vector2Int(Screen.width, Screen.height);
        }

        public float GetSpeed() {
            
            return cameraConfig.Speed;
        }

        public bool CanCameraMoveVertical(Vector2 camPosition, Vector2Int direction) {

            var size = new Vector2Int { x = mapData.Width, y = mapData.Height };
            var leftOverHeight = size.y * levelCatalogEntry.TileSideLength - screenBounds.y;

            return  - direction.y * camPosition.y < leftOverHeight / 2f;
        }

        public bool CanCameraMoveHorizontal(Vector2 camPosition, Vector2Int direction) {

			var size = new Vector2Int { x = mapData.Width, y = mapData.Height };
			var leftoverWidth = size.x * levelCatalogEntry.TileSideLength - screenBounds.x;

            return camPosition.x * -direction.x < leftoverWidth / 2f;
        }

        public float GetSpeedModifier() {
            return cameraConfig.SpeedModifier;
        }
    }
}