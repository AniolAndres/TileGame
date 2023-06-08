

using Assets.Catalogs;
using Assets.Configs;
using Assets.Views;
using System;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine;

namespace Assets.Data.Models {
	public class DialogStateModel {

		private readonly DialogData dialog;

		private int dialogIndex = 0;

		public DialogStateModel(DialogCatalogEntry dialogCatalogEntry) {
			var jsonData = dialogCatalogEntry.DialogJson.ToString();
			dialog = JsonConvert.DeserializeObject<DialogData>(jsonData);
		}
		
		public DialogViewData GetCurrentDialogViewData() {
			var data = dialog.dialogLines[dialogIndex];

			return new DialogViewData {
				IsTop = data.IsTop,
				Message = data.MessageText,
				Color = Color.red
			};
		}

		public void AdvanceDialog() {
			dialogIndex++;
		}

		public bool IsDialogFinished() {
			return dialogIndex >= dialog.dialogLines.Count;
		}
	}
}