//----------------------------------------------
// YetiTools
// Copyright © 2014 Yeti Crab Games
//----------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UIRoot))]
public class UINavigationController : MonoBehaviour {
	[SerializeField]
	private List<GameObject> panelPrefabs = new List<GameObject>();
	[SerializeField, HideInInspector]
	private string rootPanelName;
	private GameObject rootPanel;
	private Dictionary<string, GameObject> panelPrefabMap = new Dictionary<string, GameObject>();
	private Stack<GameObject> panels = new Stack<GameObject>();
	private UIRoot _uiRoot;

	public UIRoot uiRoot { get {return this._uiRoot;} private set {this._uiRoot = value;} }
	public GameObject topPanel { get {return this.panels.Peek();} }

	#region Panel stack management
	public void PushPanel(string panelName, object panelConfigurationPayload = null)
	{
		this.AssertPanelName(panelName);

		GameObject panel = NGUITools.AddChild(this.uiRoot.gameObject, this.panelPrefabMap[panelName]);
		panel.name = panelName;

		if (this.rootPanel == null) {
			this.rootPanel = panel;
		} else {
			this.panels.Peek().SetActive(false);
		}

		this.panels.Push(panel);

		this.InitializePanelController(panel.GetComponent<UIPanelController>(), panelConfigurationPayload);
	}

	public GameObject PopPanel()
	{
		this.AssertRootPanel();

		GameObject popedPanel = this.panels.Pop();
		Object.Destroy(popedPanel);
		this.panels.Peek().SetActive(true);

		return popedPanel;
	}

	public List<GameObject> PopToPanel(string panelName)
	{
		this.AssertPanelName(panelName);
		this.AssertRootPanel();

		List<GameObject> popedPanels = new List<GameObject>();

		for (int i = this.panels.Count - 1; i > 0; i--) {
			if (this.panels.Peek().name != panelName) {
				GameObject popedPanel = this.panels.Pop();
				Object.Destroy(popedPanel);
				popedPanels.Add(popedPanel);
			}
		}

		return popedPanels;
	}

	public List<GameObject> PopToRootPanel()
	{
		List<GameObject> popedPanels = new List<GameObject>();
		
		for (int i = 0; i < this.panels.Count - 1; i++) {
			GameObject popedPanel = this.panels.Pop();
			Object.Destroy(popedPanel);
			popedPanels.Add(popedPanel);
		}

		return popedPanels;
	}
	#endregion

	#region MonoBehaviour message handling
	private void Awake()
	{
		this.uiRoot = this.GetComponent<UIRoot>();

		this.panelPrefabs.RemoveAll(panelPrefab => panelPrefab.GetComponent<UIPanel>() == null);

		foreach (GameObject panelPrefab in this.panelPrefabs) {
			this.panelPrefabMap[panelPrefab.name] = panelPrefab;
		}

		this.PushPanel(this.rootPanelName);
	}
	#endregion

	#region Utility methods
	private void InitializePanelController(UIPanelController panelController, object panelConfigurationPayload)
	{
		if (panelController != null) {
			panelController.navigationController = this;
			panelController.OnPanelPushed(panelConfigurationPayload);
		}
	}
	#endregion

	#region Assertion checks
	private void AssertPanelName(string panelName)
	{
		if (string.IsNullOrEmpty(panelName) || !this.panelPrefabMap.ContainsKey(panelName)) {
			throw new System.Exception("Unable to find panel: '" + panelName + "'");
		}
	}

	private void AssertRootPanel()
	{
		if (this.rootPanel == null) {
			throw new System.Exception("No root panel found.");
		}
	}
	#endregion
}
