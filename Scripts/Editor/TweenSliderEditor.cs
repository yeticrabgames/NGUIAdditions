//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenSlider))]
public class TweenSliderEditor : UITweenerEditor {
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6.0f);
		NGUIEditorTools.SetLabelWidth(120f);
		
		TweenSlider tw = target as TweenSlider;
		GUI.changed = false;
		
		float from = EditorGUILayout.FloatField("From", tw.from);
		float to = EditorGUILayout.FloatField("To", tw.to);

		if (GUI.changed) {
			NGUIEditorTools.RegisterUndo("Tween Change", tw);
			tw.from = Mathf.Clamp(from, 0.0f, 1.0f);
			tw.to = Mathf.Clamp(to, 0.0f, 1.0f);
			UnityEditor.EditorUtility.SetDirty(tw);
		}
		
		DrawCommonProperties();
	}
}
