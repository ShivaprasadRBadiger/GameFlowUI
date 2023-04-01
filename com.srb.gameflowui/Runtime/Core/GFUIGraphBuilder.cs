using System;
using System.Collections.Generic;
using Assets.GameFlowUI.Core;
using UnityEngine;

namespace GameFlowUI.Core
{
	public class GFUIGraphBuilder: IGFUIGraphReader
	{
		private HashSet<Type> _viewsLookup;
		private Dictionary<Type, HashSet<Type>> _linksLookUp;

		public GFUIGraphBuilder()
		{
			_viewsLookup = new HashSet<Type>();
			_linksLookUp = new Dictionary<Type, HashSet<Type>>();
		}
	
		public GFUIGraphBuilder AddLink<T,U>() where T:GFUIView where U :GFUIView
		{
			if (!ContainsView<T>())
				AddView<T>();		
			if (!ContainsView<U>())
				AddView<U>();
			if(HasLink(typeof(T),typeof(U)))
			{
				Debug.LogError($"Link from {typeof(T).FullName}->{typeof(U).FullName} already exists!");
				return this;
			}
			return AddLinkToDictionary<T, U>();
		}

		public bool HasLink(Type fromType, Type toType)
		{
			var typeKeyFrom = fromType;
			var typeKeyTo = toType;
			if (!_linksLookUp.ContainsKey(typeKeyFrom))
				return false;
			return _linksLookUp[typeKeyFrom].Contains(typeKeyTo);
		}

		private GFUIGraphBuilder AddLinkToDictionary<T, U>()
		where T : GFUIView
		where U : GFUIView
		{
			var typeKeyFrom = typeof(T);
			var typeKeyTo = typeof(U);
			if (!_linksLookUp.ContainsKey(typeKeyFrom))
				_linksLookUp.Add(typeKeyFrom, new HashSet<Type>() { typeKeyTo });
			else
			_linksLookUp[typeKeyFrom].Add(typeKeyTo);

			return this;
		}

		private GFUIGraphBuilder AddView<T>() where T : GFUIView
		{
			var typeKey = typeof(T);
			if (ContainsView<T>())
			{
				Debug.LogError($"Failed to add view of type {typeKey} \n Reson: Duplicate");
				return this;
			}
			_viewsLookup.Add(typeKey);
			return this;
		}
		public bool ContainsView<T>() where T: GFUIView
		{
			var typeKey = typeof(T);
			return _viewsLookup.Contains(typeKey);
		}

		public GFUIFlowController Build(IGFUICanvasProvider canvasProvider,IGFUIViewsProvider viewsProvider)
		{
			return new GFUIFlowController(canvasProvider,viewsProvider,this);
		}

	}
}