using GameFlowUI.Core;

public class GFUIPopupsProvider : ResourcesGFUIProviderBase, IGFUIPopupsProvider
{
	public T GetPopupOfType<T>() where T : GFUIPopup
	{
		return FetchGFUIPrefabForTypeAtRoot(typeof(T),"Popups") as T;
	}
}
