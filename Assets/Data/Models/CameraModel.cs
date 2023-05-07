
using Assets.Catalogs;
using Assets.Configs;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Assets.Data.Models {
    public class CameraModel {

        private readonly CameraConfig cameraConfig;
        private readonly LevelCatalogEntry levelCatalogEntry;
        private readonly SerializableLevelData serializableLevelData;
        private readonly Vector2Int screenBounds;

        public CameraModel(CameraConfig config, LevelCatalogEntry levelCatalogEntry) {
            this.cameraConfig = config;
            this.levelCatalogEntry = levelCatalogEntry;
            serializableLevelData = JsonConvert.DeserializeObject<SerializableLevelData>(levelCatalogEntry.LevelJson.ToString());
            this.screenBounds = new Vector2Int(Screen.width, Screen.height);
        }

        public float GetSpeed() {
            
            return cameraConfig.Speed;
        }

        public bool CanCameraMoveVertical(Vector2 camPosition, Vector2Int direction) {

            var size = new Vector2Int { x = serializableLevelData.width, y = serializableLevelData.height };
            var leftOverHeight = size.y * levelCatalogEntry.TileSideLength - screenBounds.y;

            return  - direction.y * camPosition.y < leftOverHeight / 2f;
        }

        public bool CanCameraMoveHorizontal(Vector2 camPosition, Vector2Int direction) {

			var size = new Vector2Int { x = serializableLevelData.width, y = serializableLevelData.height };
			var leftoverWidth = size.x * levelCatalogEntry.TileSideLength - screenBounds.x;

            return camPosition.x * -direction.x < leftoverWidth / 2f;
        }

        public float GetSpeedModifier() {
            return cameraConfig.SpeedModifier;
        }
    }
}