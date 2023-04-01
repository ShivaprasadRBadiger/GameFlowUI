using System;

using GameFlowUI.Core;


public class GFUIViewsProvider : ResourcesGFUIProviderBase, IGFUIViewsProvider
{
	public T GetViewOfType<T>() where T : GFUIView
	{
		return GetViewPrefabOfType(typeof(T)) as T;
	}

	public GFUIView GetViewPrefabOfType(Type type)
	{
		return FetchGFUIPrefabForTypeAtRoot(type,"Views") as GFUIView;
	}

	public void Return(Type type)
	{
		//Do resource management stuff
	}
}
