using Assets.Catalogs;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.AddressableAssets;
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets.Settings.GroupSchemas;
using UnityEngine;
using System.Collections.Generic;

namespace Assets.Editor {
	public class CreateStateEditorWindow : EditorWindow {

		string stateName;
		const string statesCatalogPath = "Assets/Catalogs/StatesCatalog.asset";
		const string statesCatalogEntriesPath = "Assets/Catalogs/CatalogEntries/States";

		static StatesCatalog statesCatalog;

		[InitializeOnLoadMethod]
		static void LoadCatalog() {
			statesCatalog = AssetDatabase.LoadAssetAtPath<StatesCatalog>(statesCatalogPath);
		}

		[MenuItem("Wars/EditorWindow")]
		public static void ShowWindow() {
			GetWindow(typeof(CreateStateEditorWindow));
		}

		void OnGUI() {
			stateName = EditorGUILayout.TextField("State name:", stateName);

			if (GUILayout.Button("Create")){
				CreateState();
			}
		}

		private void CreateState() {
			var exists = statesCatalog.GetAllEntries().FirstOrDefault(x => x.Id == stateName) != null;
			if (exists) {
				EditorUtility.DisplayDialog("Error!", $"A state entry with id {stateName} already exists", "ok");
				return;
			}

			var settings = AddressableAssetSettingsDefaultObject.Settings;

			var newGroup = settings.CreateGroup(stateName, false, false, true, null, typeof(ContentUpdateGroupSchema), typeof(BundledAssetGroupSchema));
			var newEntry = CreateInstance<StateCatalogEntry>();
			newEntry.SetId(stateName);
			EditorUtility.SetDirty(newEntry);
			var assetPath = Path.Combine(statesCatalogEntriesPath, stateName + "CatalogEntry.asset");
			AssetDatabase.CreateAsset(newEntry, assetPath);
			statesCatalog.AddEntry(newEntry);
			EditorUtility.SetDirty(statesCatalog);
			AssetDatabase.Refresh();

			var guid = AssetDatabase.AssetPathToGUID(assetPath);
			var addressableEntry = settings.CreateOrMoveEntry(guid, newGroup, false, false);
			var entriesAdded = new List<AddressableAssetEntry>() { addressableEntry };
			newGroup.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entriesAdded, false, true);
			settings.SetDirty(AddressableAssetSettings.ModificationEvent.EntryMoved, entriesAdded, true, false);
		}
	}
}