//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using UnityEngine;

/// <summary>
/// Tween that animates a slider's value.
/// </summary>

[RequireComponent (typeof(UISlider))]
public class TweenSlider : UITweener {
	[Range(0,1)]
	public float from = 0.0f;
	[Range(0,1)]
	public float to = 1.0f;
	private UISlider _slider;
	
	public UISlider slider { get { return this._slider; } private set {this._slider = value;} }
	public float value { get { return this._slider.value; } set { this._slider.value = value; } }

	#region Tween playback handling
	protected override void OnUpdate(float factor, bool isFinished)
	{
		// This check is here because the reference
		// might not be set in editor time.
		if (this.slider == null) {
			this.slider = this.GetComponent<UISlider>();
		}

		this.slider.value = from * (1.0f - factor) + to * factor;
	}
	
	/// <summary>
	/// Start the tweening operation.
	/// </summary>
	
	static public TweenSlider Begin(GameObject go, float duration, float value)
	{
		TweenSlider comp = UITweener.Begin<TweenSlider>(go, duration);
		comp.from = comp.value;
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
		this.slider = this.GetComponent<UISlider>();
	}
	#endregion
}
