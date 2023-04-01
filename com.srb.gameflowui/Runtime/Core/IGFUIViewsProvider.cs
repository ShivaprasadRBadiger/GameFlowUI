using System;

namespace GameFlowUI.Core
{
	public interface IGFUIViewsProvider
	{
		public T GetViewOfType<T>() where T : GFUIView;
		public GFUIView GetViewPrefabOfType(Type type);
		void Return(Type type);
	}
}