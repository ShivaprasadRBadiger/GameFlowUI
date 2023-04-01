using System.Collections.Generic;

using Assets.GameFlowUI.Core;

using UnityEngine;

public class ResourcesCanvasProvider : IGFUICanvasProvider
{
	private const string MAIN_TEMPLATE_CANVAS = "Canvases/MainTemplate";
	private Dictionary<CanvasType, Canvas> _canvasLookup;

	public ResourcesCanvasProvider()
	{
		_canvasLookup = new Dictionary<CanvasType, Canvas>();
	}

	public Canvas GetCanvas(CanvasType canvasType)
	{
		if(_canvasLookup.TryGetValue(canvasType,out var result))
			return result;
		return SpawnCanvas(canvasType);
	}

	private Canvas SpawnCanvas(CanvasType canvasType)
	{
		var mainTemplateCanvas=	Resources.Load<GameObject>(MAIN_TEMPLATE_CANVAS);
		if(mainTemplateCanvas is null)
		{
			Debug.LogError($"Failed to locate main template canvas at {MAIN_TEMPLATE_CANVAS}");
			return null;
		}
		var canvas = Object.Instantiate(mainTemplateCanvas).GetComponent<Canvas>();
		if(canvas is null)
		{
			Debug.LogError($"Prefab located in resources at {MAIN_TEMPLATE_CANVAS} does not contain canvas component!");
			return null;
		}
		canvas.gameObject.name =$"Canvas-{canvasType}";
		_canvasLookup.Add(canvasType, canvas);
		canvas.sortingOrder =(int)canvasType;
		return canvas;
	}
}