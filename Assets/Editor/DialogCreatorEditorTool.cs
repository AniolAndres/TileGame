using Assets.Configs;
using Assets.Data.Level;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Plastic.Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

public class DialogCreatorEditorTool{

	private static string dialogFilesPath = "Assets/Catalogs/Catalogs/Dialogs/DialogFiles/MockDialog.json";


	private static DialogData dialogData = new DialogData {

		dialogLines = new List<SingleDialogData> {
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the first message!"
			},
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the second awesome message!"
			},
			new SingleDialogData {
				IsTop = true,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the third message and it should be on top!"
			},
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "blue",
				IconId = "something",
				MessageText = "This is the fourth message and it should be blue!"
			},
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the fifth message!"
			},
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the sixth message!"
			},
			new SingleDialogData {
				IsTop = true,
				ArmyColorId = "blue",
				IconId = "something",
				MessageText = "This is the seventh message and it should be blue and on top!"
			},
			new SingleDialogData {
				IsTop = false,
				ArmyColorId = "red",
				IconId = "something",
				MessageText = "This is the last message, bye!"
			}
		}
	};

	[MenuItem("Wars/Create Mock Dialog")]
	static void CreateDialog() {
		var data = JsonConvert.SerializeObject(dialogData);
		File.WriteAllText(dialogFilesPath, data.ToString());
		EditorUtility.DisplayDialog("Success!", "Dialog created", "Ok");
	}
}
