
using Assets.Catalogs.Scripts;
using Assets.Configs;
using System;
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

        public bool CanCameraMove(Vector2 camPosition) {
            return true;
        }
    }
}