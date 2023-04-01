using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assets.GameFlowUI.Core;
using UnityEngine;

using Object = UnityEngine.Object;

namespace GameFlowUI.Core
{
	public class GFUIFlowController : IGFUIFlowController
	{
		private IGFUIGraphReader _graphReader;
		private IGFUICanvasProvider _canvasProvider;
		private IGFUIViewsProvider _viewsProvider;
		private GFUIView _currentView;
		private Task _currentTransition;
		private Stack<Type> _viewsHistory;
		private Canvas _mainCanvas;

		public GFUIFlowController(IGFUICanvasProvider canvasProvider,IGFUIViewsProvider viewsProvider, IGFUIGraphReader graphReader)
		{
			_canvasProvider = canvasProvider;
			_viewsProvider = viewsProvider;
			_graphReader = graphReader;
			_viewsHistory = new Stack<Type>();
		}

		public async Task Initialize<T>() where T : GFUIView
		{
			_mainCanvas = _canvasProvider.GetCanvas(CanvasType.Main);
			await InitializeTo<T>();
		}
	
		public async Task InitializeTo<T>() where T : GFUIView
		{
			var requestedType = typeof(T);
			await OpenViewAsync(requestedType);
		}
		public async Task TransitionTo<T>() where T : GFUIView
		{
			await TransitionTo(typeof(T));
		}

		private async Task TransitionTo(Type type)
		{
			if (_currentTransition is { })
			{
				Debug.LogWarning("Transition in progress! Transition request ignored!");
				return;
			}
			if (_graphReader.HasLink(_currentView.GetType(), type))
			{
				_currentTransition = DoTransition(type);
				await _currentTransition;
				_currentTransition = null;
			}
			else
				Debug.LogError($"There is no link from  current view <{_currentView.GetType().FullName}> to <{type.FullName}> ");
		}

		private async Task DoTransition(Type type)
		{
			await CloseCurrentViewAsync();
			await OpenViewAsync(type);
		}

		private async Task OpenViewAsync(Type type)
		{
			var nextView = _viewsProvider.GetViewPrefabOfType(type);
			if (nextView is null)
			{
				Debug.LogError("Transition failed, Reason: Failed to get view from view provider!");
				return;
			}
			_currentView = InstantiateViewInMainCanvas(nextView);
			_currentView.Initialize();
			_currentView.Show();
			_viewsHistory.Push(_currentView.GetType());
			await _currentView.AnimateIn();
		}

		private async Task CloseCurrentViewAsync()
		{
			await _currentView.AnimateOut();
			_currentView.Hide();
			_currentView.Dispose();
			_viewsProvider.Return(_currentView.GetType());
			Object.Destroy(_currentView.gameObject);
		}

		private GFUIView InstantiateViewInMainCanvas(GFUIView nextView)
		{
			 return Object.Instantiate(nextView.gameObject, _mainCanvas.transform).GetComponent<GFUIView>();
		}
	}
}