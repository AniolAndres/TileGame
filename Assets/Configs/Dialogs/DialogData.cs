
using System;
using System.Collections.Generic;
using Unity.Plastic.Newtonsoft.Json;

namespace Assets.Configs {

	[Serializable]
	public class DialogData {

		[JsonProperty(PropertyName = "DialogLines")]
		public List<SingleDialogData> dialogLines;
	}
}