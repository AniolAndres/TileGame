
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using Assets.Configs;

public class MapCreatorEditorTool
{
    static string levelPath = "Assets/Editor/FirstLevel.json";

	static SerializableLevelData levelData = new SerializableLevelData {
        playersCount = 2,
        height = 4,
        width = 4,
        tiles = new List<SerializableTileData> { 
            
            new SerializableTileData {
                xPosition = 0,
                yPosition = 0,
                tileType = "headquarters",
                owner = 1,

            },new SerializableTileData {
                xPosition = 1,
                yPosition = 0,
                tileType = "grass",
                owner = -1,

            },new SerializableTileData {
                xPosition = 2,
                yPosition = 0,
                tileType = "grass",
                owner = -1,

            },new SerializableTileData {
                xPosition = 3,
                yPosition = 0,
                tileType = "mountain",
                owner = -1,

            },
            
            new SerializableTileData {
                xPosition = 0,
                yPosition = 1,
                tileType = "building",
                owner = 1,

            },new SerializableTileData {
                xPosition = 1,
                yPosition = 1,
                tileType = "water",
                owner = -1,

            },new SerializableTileData {
                xPosition = 2,
                yPosition = 1,
                tileType = "building",
                owner = 0,

            },new SerializableTileData {
                xPosition = 3,
                yPosition = 1,
                tileType = "water",
                owner = -1,

            },
            
            new SerializableTileData {
                xPosition = 0,
                yPosition = 2,
                tileType = "grass",
                owner = -1,

            }, new SerializableTileData {
                xPosition = 1,
                yPosition = 2,
                tileType = "water",
                owner = -1,

            }, new SerializableTileData {
                xPosition = 2,
                yPosition = 2,
                tileType = "mountain",
                owner = -1,

            }, new SerializableTileData {
                xPosition = 3,
                yPosition = 2,
                tileType = "grass",
                owner = -1,

            }, 
            
            new SerializableTileData {
                xPosition = 0,
                yPosition = 3,
                tileType = "water",
                owner = -1,

            }, new SerializableTileData {
                xPosition = 1,
                yPosition = 3,
                tileType = "water",
                owner = -1,

            }, new SerializableTileData {
                xPosition = 2,
                yPosition = 3,
                tileType = "building",
                owner = 2,

            }, new SerializableTileData {
                xPosition = 3,
                yPosition = 3,
                tileType = "headquarters",
                owner = 2,
            },


        }
    };

    [MenuItem("Wars/Create Mock Level")]
    static void CreateNewLevel() {
        var data = JsonConvert.SerializeObject(levelData);
        File.WriteAllText(levelPath, data.ToString());
        EditorUtility.DisplayDialog("Success!", "Level created", "Ok");
    }
}
