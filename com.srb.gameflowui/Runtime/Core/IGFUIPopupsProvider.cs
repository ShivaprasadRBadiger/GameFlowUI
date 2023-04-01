namespace GameFlowUI.Core
{
	public interface IGFUIPopupsProvider
	{
		public T GetPopupOfType<T>() where T : GFUIPopup;
	}
}