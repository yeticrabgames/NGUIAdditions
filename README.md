NGUI Additions
=============

A collection of scripts that add some additional functionality to the NGUI framework

It includes:

* Panel controller functionality similar to [UIViewController](https://developer.apple.com/library/ios/documentation/uikit/reference/UIViewController_Class/Reference/Reference.html#//apple_ref/occ/cl/UIViewController) (following an MVC paradigm).
* A panel navigation stack similar to [UINavigationController](https://developer.apple.com/library/ios/documentation/uikit/reference/UINavigationController_Class/Reference/Reference.html).
* Playing tween components in sequences or concurrent groups.
* Additional tween components for label counters and sliders.

To use UINavigationController you will need to:

1. Attach the UINavigationController component to your UIRoot.
2. Create a prefab for every panel in your game (essentially every screen).
3. Optionally add a UIPanelController sub-class to every panel. 
3. Pass the list of panel prefabs to the UINavigationController component.

In a UIPanelController you can transition to a new panel by calling:
	this.navigationController.PushPanel("InGamePanel", optionalPayload);
