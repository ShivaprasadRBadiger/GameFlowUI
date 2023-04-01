using System;
using System.Threading.Tasks;

using Assets.GameFlowUI.Core;
using com.srb.DependencyInjection;
using GameFlowUI.Core;
using UnityEngine;

public class BootStrap : MonoBehaviour
{
	private IGFUIFlowController _flowController;

	private void Awake()
	{
		SimpleDI.Default.Bind((IGFUICanvasProvider)new ResourcesCanvasProvider());
		SimpleDI.Default.Bind((IGFUIViewsProvider)new GFUIViewsProvider());
		SimpleDI.Default.Bind((IGFUIPopupsProvider)new GFUIPopupsProvider());
		SimpleDI.Default.Bind((IGFUIWidgetsProvider)new GFUIWidgetsProvider());
	}
	private async void Start()
	{
		_flowController = GetFlowController<TestView1>();
		await _flowController.Initialize<TestView1>();
	}


	private IGFUIFlowController GetFlowController<T>() where T : GFUIView
	{
		var canvasProvider = SimpleDI.Default.GetInstance<IGFUICanvasProvider>();
		var viewsProvider = SimpleDI.Default.GetInstance<IGFUIViewsProvider>();
		return new GFUIGraphBuilder()
			.AddLink<TestView1, TestView2>()
			.Build(canvasProvider,viewsProvider);
	}

	public void Update()
	{
		if(Input.GetKeyDown(KeyCode.Space))
			_flowController.TransitionTo<TestView2>();
	}
}
