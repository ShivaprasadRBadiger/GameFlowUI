using System;
using GameFlowUI.Core;

public interface IGFUIGraphReader
{
	 bool ContainsView<T>() where T : GFUIView;
	 bool HasLink(Type fromType, Type toType);
}