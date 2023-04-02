using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using DG.Tweening;

using GameFlowUI.Core;

using UnityEngine;

public class HomeScreenFooter : GFUIWidget
{
	private RectTransform _rectTransform;
	private Vector2 _initialPosition;

	public override void Initialize()
	{
		base.Initialize();
		_rectTransform = GetComponent<RectTransform>();
		_initialPosition = _rectTransform.anchoredPosition;
		 PostInitializeAsync();
	}

	private async void PostInitializeAsync()
	{
		await AnimateIn();
	}

	public override async Task AnimateIn()
	{
		await _rectTransform.DOAnchorPos(Vector2.zero,0.5f).AsyncWaitForCompletion();
		var taskList = new List<Task>();
		foreach(var childWidget in _childWidgets)
		{
			await Task.Delay(250);
			taskList.Add(childWidget.Value.AnimateIn());
		}
		await Task.WhenAll(taskList);
	}

	public override async Task AnimateOut()
	{
		var taskList = new List<Task>();
		foreach (var childWidget in _childWidgets)
		{
			await Task.Delay(250);
			taskList.Add(childWidget.Value.AnimateOut());
		}
		await Task.WhenAll(taskList);
		await _rectTransform.DOAnchorPos(_initialPosition,0.5f).AsyncWaitForCompletion();
	}

	public override void Hide()
	{
		gameObject.SetActive(false);
	}

	public override void Show()
	{
		gameObject.SetActive(true);
	}
}
