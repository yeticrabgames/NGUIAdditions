//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Executes a series of tweens in sequence.
/// </summary>

public class TweenSequence : UITweener {
	[SerializeField]
	private List<UITweener> tweeners = new List<UITweener>();
	private int currentTweenIndex = 0;

	#region Tween playback handling
	// Since no actual tweening is
	// taking place this function is empty.
	protected override void OnUpdate(float factor, bool isFinished)
	{
	}

	static public TweenSequence Begin(GameObject go, float duration)
	{
		TweenSequence comp = UITweener.Begin<TweenSequence>(go, duration);
		return comp;
	}
	#endregion

	#region MonoBehaviour message handling
	private void Awake()
	{
		foreach (UITweener tweener in this.tweeners) {
			if (tweener == null) {
				continue;
			}
			
			tweener.onFinished.Add(new EventDelegate(this.OnTweenFinished));
			tweener.enabled = false;
		}	
	}
	
	private void OnEnable()
	{
		if (this.tweeners.Count > 0 && (this.tweeners[0] != null)) {
			this.tweeners[0].enabled = true;
		}
	}

	private void OnDisable()
	{
		this.tweeners.ForEach((UITweener tweener) => {if (tweener != null) {tweener.enabled = false;}});
	}
	
	// We provide an empty Start method
	// since the Base one is calling Update.
	private new void Start() 
	{
	}
	
	// We provide an empty Update method
	// because the listeners should only be notified when
	// all individual tweens have finished.
	private void Update()
	{
	}
	#endregion

	#region Callback handling
	private void OnTweenFinished()
	{		
		currentTweenIndex++;
			
		if (currentTweenIndex > this.tweeners.Count - 1) {
			EventDelegate.Execute(this.onFinished);
			this.enabled = false;
		} else if (this.tweeners[currentTweenIndex] != null) {
			this.tweeners[currentTweenIndex].enabled = true;
		}
	}
	#endregion
}
