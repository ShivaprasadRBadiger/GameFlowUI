using System;
using System.Collections.Generic;
using com.srb.DependencyInjection;
using UnityEngine;

namespace GameFlowUI.Core
{
	public abstract class GFUIWidget: GFUIElement
	{
		[SerializeField]
		private List<GFUIWidget> _serializedWidgets;
		private IGFUIWidgetsProvider _widgetProvider;
		private Dictionary<Type, GFUIWidget> _childWidgets;

		public override void Initialize()
		{
			_childWidgets = new Dictionary<Type, GFUIWidget>();
			_widgetProvider =SimpleDI.Default.GetInstance<IGFUIWidgetsProvider>();
			LoadSerializedWidgets();
			InitializeChildren();
		}

		public override void Dispose()
		{
			DisposeChildren();
		}

		private void DisposeChildren()
		{
			foreach (var widget in _childWidgets)
				widget.Value.Dispose();
		}

		private void InitializeChildren()
		{
			foreach (var widget in _childWidgets)
			{
				widget.Value.Initialize();
			}
		}
		private void LoadSerializedWidgets()
		{
			foreach (var serializedWidget in _serializedWidgets)
			{
				if (_childWidgets.ContainsKey(serializedWidget.GetType()))
				{
					Debug.LogError($"Duplicate widget added to {gameObject.name}");
					continue;
				}
				if (!serializedWidget.transform.IsChildOf(transform))
				{
					Debug.LogError($"The referenced widget {serializedWidget.name} is not the child of {name}, its ignored");
					continue;
				}
				_childWidgets.Add(serializedWidget.GetType(), serializedWidget);
			}
		}

		public void AddWidget<T>() where T: GFUIWidget
		{
			if (_childWidgets.ContainsKey(typeof(T)))
			{
				Debug.LogError("Trying to add same widget multiple times! Ignored");
				return;
			}
			var widget = _widgetProvider.GetWidgetOfType<T>();
			_childWidgets.Add(typeof(T), widget);
			widget.transform.SetParent(transform, true);
			widget.Initialize();
			widget.AnimateIn();
		}

		public async void RemoveWidget<T>() where T : GFUIWidget
		{
			var typeKey = typeof(T);
			if (!_childWidgets.ContainsKey(typeKey))
			{
				Debug.LogError($"Widget {typeKey.Name} is not present in {this.GetType().Name}!");
				return;
			}
			var widgetToRemove=_childWidgets[typeKey];
			await widgetToRemove.AnimateOut();
			widgetToRemove.Dispose();
			_childWidgets.Remove(typeKey);
		}
	}
}