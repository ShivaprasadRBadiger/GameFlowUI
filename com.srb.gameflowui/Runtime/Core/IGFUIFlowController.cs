using System.Threading.Tasks;

namespace GameFlowUI.Core
{
	public interface IGFUIFlowController
	{
		Task Initialize<T>() where T : GFUIView;
		Task TransitionTo<T>() where T : GFUIView;
	}
}