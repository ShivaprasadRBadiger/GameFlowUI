using System;

using GameFlowUI.Core;
using UnityEngine;

public abstract class ResourcesGFUIProviderBase
{
	protected static GFUIElement FetchGFUIPrefabForTypeAtRoot(Type type ,string root)
	{
		if (!typeof(GFUIElement).IsAssignableFrom(type))
		{
			throw new ArgumentException($"{nameof(type)} is not a subclass of {nameof(GFUIElement)}");
		}
		var prefabName = type.Name;
		var prefabPath = $"{root}/{prefabName}";
		var prefab = Resources.Load<GameObject>(prefabPath);
		if(prefab is null)
		{
			Debug.LogError($"Unable to find prefab at {prefabPath}");
			return default;
		}
		var component = prefab.GetComponent(type);
		if (component is null)
			Debug.LogError($"Could not find component {prefabName} in at path {prefabPath}");
		return component as GFUIElement;
	}

}