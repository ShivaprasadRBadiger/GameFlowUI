
using UnityEngine;

namespace Assets.GameFlowUI.Core
{
	public interface IGFUICanvasProvider
	{
		Canvas GetCanvas(CanvasType canvasType);
	}
}