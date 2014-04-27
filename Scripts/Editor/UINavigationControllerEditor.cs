//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(UINavigationController))]
public class UINavigationControllerEditor : Editor {
	private SerializedProperty rootPanelNameProperty;
	private SerializedProperty panelPrefabsProperty;
	private string[] panelNames;
	private int[] panelIndices;
	private int rootPanelIndex = 0;

	#region Editor overrides
	public override void OnInspectorGUI()
	{
		this.serializedObject.Update();

		this.DrawDefaultInspector();

		this.ConfigurePanelsPopupState();

		this.rootPanelIndex = EditorGUILayout.IntPopup("Root Panel", this.rootPanelIndex, this.panelNames, this.panelIndices);

		if (GUI.changed) {
			this.rootPanelNameProperty.stringValue = this.panelNames[this.rootPanelIndex];

			this.serializedObject.ApplyModifiedProperties();
		}
	}
	#endregion

	#region Editor message handling
	private void OnEnable()
	{
		this.rootPanelNameProperty = this.serializedObject.FindProperty("rootPanelName");
		this.panelPrefabsProperty = this.serializedObject.FindProperty("panelPrefabs");

		this.ConfigurePanelsPopupState();
		this.rootPanelIndex = this.GetPanelNameIndex(this.rootPanelNameProperty.stringValue);
	}
	#endregion

	#region Utility methods
	private void ConfigurePanelsPopupState()
	{
		int panelPrefabCount = this.panelPrefabsProperty.arraySize;
		int currentActivePanelIndex = 0;
		List<string> panelNames = new List<string>();
		List<int> panelIndices = new List<int>();

		for (int i = 0; i < panelPrefabCount; i++) {
			GameObject panelPrefab = this.panelPrefabsProperty.GetArrayElementAtIndex(i).objectReferenceValue as GameObject;

			if (panelPrefab.GetComponent<UIPanel>() != null) {
				panelNames.Add(panelPrefab.name);
				panelIndices.Add(currentActivePanelIndex++);
			}
		}

		this.panelNames = panelNames.ToArray();
		this.panelIndices = panelIndices.ToArray();
	}

	private int GetPanelNameIndex(string panelName)
	{
		int panelNameCount = this.panelNames.Length;
		int panelNameIndex = 0;

		for (int i = 0; i < panelNameCount; i++) {
			if (this.panelNames[i] == panelName) {
				panelNameIndex = i;
				break;
			}
		}

		return panelNameIndex;
	}
	#endregion
}
