using System;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;

namespace GameFlowUI.Core
{
	[RequireComponent(typeof(Button))]
	public class GFUIButton:GFUIWidget
	{ 
		public event Action OnClick;
		private Button _button;

		public override void Initialize()
		{
			_button = GetComponent<Button>();
			_button.onClick.AddListener(OnClickHandler);
		}

		public override void Dispose()
		{
			_button.onClick.RemoveListener(OnClickHandler);
			base.Dispose();
		}

		protected virtual void OnClickHandler()
		{
			OnClick?.Invoke();
		}
		
		public void SetInteractable(bool interactablitiy)
		{
			_button.interactable = interactablitiy;
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
	}
}