using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using com.srb.DependencyInjection;

namespace GameFlowUI.Core
{
	public abstract class GFUIView : GFUIWidget
	{
		private Stack<GFUIPopup> _popupHistory;
		private IGFUIPopupsProvider _popupsProvider;
		public override void Initialize()
		{
			_popupHistory = new Stack<GFUIPopup>();
			_popupsProvider = SimpleDI.Default.GetInstance<IGFUIPopupsProvider>();
			base.Initialize();
		}
		public override void Show()
		{
			gameObject.SetActive(true);
		}
		public override Task AnimateIn()
		{
			return Task.CompletedTask;
		}
		public override Task AnimateOut()
		{
			return Task.CompletedTask;
		}
		public override void Hide()
		{
			gameObject.SetActive(false);
		}

		private void ClosePreviousPopup()
		{
			if (_popupHistory.Count == 0)
				return;
			var popupToClose = _popupHistory.Pop();
			popupToClose.AnimateOut();
		}

		public void ShowPopup<T>() where T:GFUIPopup
		{
			var popup = _popupsProvider.GetPopupOfType<T>();
			_popupHistory.Push(popup);
			popup.Initialize();
			popup.AnimateIn();
		}
	}
}