using System.Threading.Tasks;

namespace GameFlowUI.Core
{
	public interface IGFUIWidgetsProvider
	{
		public T GetWidgetOfType<T>() where T : GFUIWidget;
	}
}