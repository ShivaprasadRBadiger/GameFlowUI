using GameFlowUI.Core;

public class GFUIWidgetsProvider : ResourcesGFUIProviderBase, IGFUIWidgetsProvider
{
	public T GetWidgetOfType<T>() where T : GFUIWidget
	{
		return FetchGFUIPrefabForTypeAtRoot(typeof(T), "Widgets") as T;
	}
}