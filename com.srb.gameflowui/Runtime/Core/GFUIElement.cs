using System;
using System.Threading.Tasks;
using UnityEngine;

namespace GameFlowUI.Core
{
	[RequireComponent(typeof(RectTransform))]
	public abstract class GFUIElement: MonoBehaviour, GFUIIInitializable, IDisposable
	{
		public abstract void Initialize();
		public abstract void Show();
		public abstract Task AnimateIn();
		public abstract Task AnimateOut();
		public abstract void Hide();
		public abstract void Dispose();
	}
}