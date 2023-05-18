using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Assets.Configs {

	[Serializable]
	public class SingleDialogData {

		[JsonProperty(PropertyName = "message")]
		public string MessageText { get; set; }

		[JsonProperty(PropertyName = "iconId")]
		public string IconId { get; set; }

		[JsonProperty(PropertyName = "colorId")]
		public string ArmyColorId { get; set; }

		[JsonProperty(PropertyName = "top")]
		public bool IsTop { get; set; }
    }
}