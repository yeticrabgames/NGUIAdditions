//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenLabelCounter))]
public class TweenLabelCounterEditor : UITweenerEditor {
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6f);
		NGUIEditorTools.SetLabelWidth(120f);
		
		TweenLabelCounter tw = target as TweenLabelCounter;
		GUI.changed = false;
		
		int from = EditorGUILayout.IntField("From", tw.from);
		int to = EditorGUILayout.IntField("To", tw.to);
		string formatString = EditorGUILayout.TextField("Format String", tw.formatString);
		
		if (GUI.changed) {
			NGUIEditorTools.RegisterUndo("Tween Change", tw);
			tw.from = from;
			tw.to = (to > from) ? to : from;
			tw.formatString = formatString;
			UnityEditor.EditorUtility.SetDirty(tw);
		}
		
		DrawCommonProperties();
	}
}
