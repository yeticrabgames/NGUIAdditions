//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System.Collections;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TweenSequence))]
public class TweenSequenceEditor : UITweenerEditor {
	public override void OnInspectorGUI ()
	{
		GUILayout.Space(6.0f);
		NGUIEditorTools.SetLabelWidth(120.0f);

		TweenSequence tw = target as TweenSequence;
		GUI.changed = false;

		if (GUI.changed) {
			NGUIEditorTools.RegisterUndo("Tween Change", tw);
			UnityEditor.EditorUtility.SetDirty(tw);
		}

		this.DrawModifiedCommonProperties();
	}
	
	private void DrawModifiedCommonProperties()
	{
		UITweener tw = target as UITweener;

		SerializedProperty tweenersProperty = this.serializedObject.FindProperty("tweeners");
		while (true) {
    		Rect propertyRect = GUILayoutUtility.GetRect(0f, 16f);
							
			bool showChildren = (tweenersProperty.name == "tweeners") ? EditorGUI.PropertyField(propertyRect, tweenersProperty, new GUIContent("Tweens")) : EditorGUI.PropertyField(propertyRect, tweenersProperty);
			if (!tweenersProperty.NextVisible(showChildren)) {
				break;
			}
		}

		this.serializedObject.ApplyModifiedProperties();
	
		GUI.changed = false;

		if (GUI.changed) {
			NGUIEditorTools.RegisterUndo("Tween Change", tw);
			UnityEditor.EditorUtility.SetDirty(tw);
		}

		NGUIEditorTools.SetLabelWidth(80f);
		NGUIEditorTools.DrawEvents("On Finished", tw, tw.onFinished);
	}
}
