using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using Unity.Plastic.Newtonsoft.Json.Serialization;
using UnityEngine;

namespace Assets.Configs {

	[Serializable]
	public class LevelData {

		[JsonProperty(PropertyName = "map")]
		public MapData MapData;
		
		[JsonProperty(PropertyName = "triggers")]
		public TriggerData[] Triggers;
		
		[JsonProperty(PropertyName = "players")]
		public PlayerData[] Players;
		
		[JsonProperty(PropertyName = "endGame")]
		public EndGameData EndGame;
	}

	public class EndGameData
	{
		[JsonProperty(PropertyName = "winStrategy")]
		public string WinStrategy;
		
		[JsonProperty(PropertyName = "loseStrategy")]
		public string LoseStrategy;
	}

	public class PlayerData
	{
		[JsonProperty(PropertyName = "commanderId")]
		public string CommanderId;
		
		[JsonProperty(PropertyName = "playerIndex")]
		public int PlayerIndex;
		
		[JsonProperty(PropertyName = "teamIndex")]
		public int TeamId;	
		
		[JsonProperty(PropertyName = "color")]
		public string ColorId;
	}

	public class TriggerData
	{
		[JsonProperty(PropertyName = "kind")] //Could be nice if it was an enum
		public string Kind;
		
		[JsonProperty(PropertyName = "action")]
		public string Action;
		
		[JsonProperty(PropertyName = "data")]
		public string Data;
	}

	public class MapData
	{
		[JsonProperty(PropertyName ="tiles")]
		public List<TileData> Tiles;

		[JsonProperty(PropertyName = "width")]
		public int Width;

		[JsonProperty(PropertyName = "height")]
		public int Height;
		
		[JsonProperty(PropertyName = "tileSideLength")]
		public int TileSideLength;
	}

	public class TileData {
		[JsonProperty(PropertyName ="x")]
		public int xPosition;

		[JsonProperty(PropertyName = "y")]
		public int yPosition;

		[JsonProperty(PropertyName = "type")]
		public string tileType;

		[JsonProperty(PropertyName = "owner")]
		public int owner;
	}
}