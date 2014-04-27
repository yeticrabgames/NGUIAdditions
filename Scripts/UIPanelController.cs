//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using UnityEngine;

[RequireComponent(typeof(UIPanel))]
public abstract class UIPanelController : MonoBehaviour 
{
	public UINavigationController navigationController { get {return this._navigationController;} set {this._navigationController = value;} }
	public UIPanel panel { get {return this._panel;} private set {this._panel = value;} }

	private UINavigationController _navigationController;
	private UIPanel _panel;

	#region Panel handling
	public void PopPanel()
	{
		this.navigationController.PopPanel();
	}
	#endregion

	#region Event handling
	public virtual void OnPanelPushed(object panelConfigurationPayload)
	{
	}

	protected virtual void OnAwake() 
	{
	}
	#endregion

	#region MonoBehaviour message handling
	private void Awake()
	{
		this.panel = this.GetComponent<UIPanel>();

		this.OnAwake();
	}
	#endregion
}
