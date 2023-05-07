using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Assets.Controllers {

	[Serializable]
	public class SerializableLevelData {

		[JsonProperty(PropertyName ="tiles")]
		public List<SerializableTileData> tiles;
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