//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System;
using UnityEngine;

/// <summary>
/// Tween that animates a label's text between 2 integer values.
/// </summary>

[RequireComponent (typeof(UILabel))]
public class TweenLabelCounter : UITweener {
	public int from = 0;
	public int to = 0;
	public string formatString;
	private UILabel _label;

	public UILabel label { get { return this._label; } private set {this._label = value;} }
	public int Value { get { return Convert.ToInt32(this.label.text); } set { this.label.text = value.ToString(); } }

	#region Tween playback handling
	protected override void OnUpdate(float factor, bool isFinished)
	{
		// This check is here because the reference
		// might not be set in editor time.
		if (this.label == null) {
			this.label = this.GetComponent<UILabel>();
		}

		string counterText = Mathf.FloorToInt(from * (1.0f - factor) + to * factor).ToString();
		this.label.text = (this.formatString != null && this.formatString.Length > 0) ? string.Format(this.formatString, counterText) : counterText;
	}
	
	/// <summary>
	/// Start the tweening operation.
	/// </summary>
	
	static public TweenLabelCounter Begin(GameObject go, float duration, int value)
	{
		TweenLabelCounter comp = UITweener.Begin<TweenLabelCounter>(go, duration);
		comp.from = comp.Value;
		comp.to = value;
		
		if (duration <= 0.0f) {
			comp.Sample(1.0f, true);
			comp.enabled = false;
		}
		return comp;
	}
	#endregion

	#region MonoBehaviour message handling
	private void Awake()
	{
		this.label = this.GetComponent<UILabel>();
	}
	#endregion
}
