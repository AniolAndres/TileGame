using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Assets.Configs {

	[Serializable]
	public class SerializableLevelData { //Rename to levelData, get rid of old data class

		[JsonProperty(PropertyName ="tiles")]
		public List<SerializableTileData> tiles;

		[JsonProperty(PropertyName = "players")]
		public int playersCount;

		[JsonProperty(PropertyName = "width")]
		public int width;

		[JsonProperty(PropertyName = "height")]
		public int height;
	}

	public class SerializableTileData {
		[JsonProperty(PropertyName ="x")]
		public int xPosition;

		[JsonProperty(PropertyName = "y")]
		public int yPosition;

		[JsonProperty(PropertyName = "type")]
		public string tileType;

		[JsonProperty(PropertyName = "main")]
		public int owner;
	}
}