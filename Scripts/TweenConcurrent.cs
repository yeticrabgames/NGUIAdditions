//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Executes a series of tweens in parallel.
/// </summary>

public class TweenConcurrent : UITweener {
	[SerializeField]
	private List<UITweener> tweeners = new List<UITweener>();
	private int finishedTweensCount = 0;

	#region Tween playback handling
	// Since no actual tweening is
	// taking place this function is empty.
	protected override void OnUpdate (float factor, bool isFinished)
	{
	}

	static public TweenConcurrent Begin (GameObject go, float duration)
	{
		TweenConcurrent comp = UITweener.Begin<TweenConcurrent>(go, duration);		
		return comp;
	}
	#endregion

	#region MonoBehaviour message handling
	private void Awake()
	{
		foreach (UITweener tweener in this.tweeners) {
			tweener.onFinished.Add(new EventDelegate(this.OnTweenFinished));
			tweener.enabled = false;
		}	
	}
	
	private void OnEnable()
	{
		this.tweeners.ForEach((UITweener tweener) => {tweener.enabled = true;});
	}

	private void OnDisable()
	{
		this.tweeners.ForEach((UITweener tweener) => {tweener.enabled = false;});
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
		if (++this.finishedTweensCount == this.tweeners.Count) {
			EventDelegate.Execute(this.onFinished);
			this.enabled = false;
		}
	}
	#endregion
}
