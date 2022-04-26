using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class GameObjectX
{
	/// <summary>
	/// Sets the active state of this <paramref name="gameObject"/> and it's first level parent.
	/// </summary>
	public static void SetActiveWithParent(this GameObject gameObject, bool value)
	{
		gameObject.SetActive(value);
		gameObject.transform.parent.gameObject.SetActive(value);
	}

	/// <summary>
	/// Sets the active state of this <paramref name="gameObject"/> and it's first level children.
	/// </summary>
	public static void SetActiveWithChildren(this GameObject gameObject, bool value)
	{
		gameObject.SetActive(value);
		foreach (Transform child in gameObject.transform)
		{
			child.gameObject.SetActive(value);
		}
	}

	/// <summary>
	/// Sets the active state of this <paramref name="gameObject"/> and all of ancestors (parent, grandparent, parent of grandparent... etc).
	/// </summary>
	/// <remarks>
	/// This method loops through the hierarchy, thus eliminating recursive calls.
	/// </remarks>
	public static void SetActiveWithAncestors(this GameObject gameObject, bool value)
	{
		var t = gameObject.transform;
		while (t != null)
		{
			t.gameObject.SetActive(value);
			t = t.parent;
		}
	}

	/// <summary>
	/// Sets the active state of this <paramref name="gameObject"/> and all of it's children hierarchy (children, grandchildren, children of grandchildren... etc).
	/// </summary>
	/// <remarks>
	/// This method keeps a list of all children hierarchy as it loops through, thus eliminating recursive calls.
	/// </remarks>
	public static void SetActiveWithDescendants(this GameObject gameObject, bool value)
	{
		Transform firstLevel = SetActiveTSCH(gameObject.transform, value);
		if (firstLevel.childCount == 0) return;

		var queue = new List<Transform> { firstLevel };
		while (queue.Count > 0)
		{
			for (int i = queue.Count - 1; i >= 0; i--)
			{
				Transform t = SetActiveTSCH(queue[i], value);
				queue.RemoveAt(i);

				if (t.childCount > 0) queue.AddRange(t.Cast<Transform>());
			}
		}
	}

	/// <summary>
	/// SetActive Through Single Child Hierarchy <para/>
	/// 'SetActive's transform; if transform has only one child, switches to child; repeats the process. <para/>
	/// Continues until switching to a transform with no child or more than one child. <para/>
	/// Returns the transform it stopped.
	/// </summary>
	private static Transform SetActiveTSCH(Transform beginWith, bool value)
	{
		Transform t = beginWith;
		t.gameObject.SetActive(value);

		while (t.childCount == 1)
		{
			t = t.GetChild(0);
			t.gameObject.SetActive(value);
		}

		return t;
	}

	public static void SetTransformX(this GameObject obj, float n)
	{
		obj.transform.position = new Vector3(n, obj.transform.position.y, obj.transform.position.z);
	}

	public static void SetTransformY(this GameObject obj, float n)
	{
		obj.transform.position = new Vector3(obj.transform.position.x, n, obj.transform.position.z);
	}

	public static void SetTransformZ(this GameObject obj, float n)
	{
		obj.transform.position = new Vector3(obj.transform.position.x, obj.transform.position.y, n);
	}

	//recursive calls
	private static void InternalMoveToLayer(Transform root, int layer)
	{
		root.gameObject.layer = layer;
		foreach (Transform child in root)
			InternalMoveToLayer(child, layer);
	}

	/// <summary>
	/// Move root and all children to the specified layer
	/// </summary>
	/// <param name="root"></param>
	/// <param name="layer"></param>
	public static void MoveToLayer(this GameObject root, int layer)
	{
		InternalMoveToLayer(root.transform, layer);
	}

	/// <summary>
	/// is the object's layer in the specified layermask
	/// </summary>
	/// <param name="gameObject"></param>
	/// <param name="mask"></param>
	/// <returns></returns>
	public static bool IsInLayerMask(this GameObject gameObject, LayerMask mask)
	{
		return ((mask.value & (1 << gameObject.layer)) > 0);
	}

	/// <summary>
	/// Returns all monobehaviours that are of type T, as T. Works for interfaces
	/// </summary>
	/// <typeparam name="T">class type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClasses<T>(this GameObject gObj) where T : class
	{
		var ts = gObj.GetComponents(typeof(T));

		var ret = new T[ts.Length];
		for (int i = 0; i < ts.Length; i++)
		{
			ret[i] = ts[i] as T;
		}
		return ret;
	}

	/// <summary>
	/// Returns all classes of type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T">interface type</typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClasses<T>(this Transform gObj) where T : class
	{
		return gObj.gameObject.GetClasses<T>();
	}

	/// <summary>
	/// Returns the first monobehaviour that is of the class Type, as T
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetClass<T>(this GameObject gObj) where T : class
	{
		return gObj.GetComponent(typeof(T)) as T;
	}

	/// <summary>
	/// Gets all monobehaviours in children that implement the class of type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T[] GetClassesInChildren<T>(this GameObject gObj) where T : class
	{
		var ts = gObj.GetComponentsInChildren(typeof(T));

		var ret = new T[ts.Length];
		for (int i = 0; i < ts.Length; i++)
		{
			ret[i] = ts[i] as T;
		}
		return ret;
	}

	/// <summary>
	///
	/// Returns the first instance of the monobehaviour that is of the class type T (casted to T)
	/// works with interfaces
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="gObj"></param>
	/// <returns></returns>
	public static T GetClassInChildren<T>(this GameObject gObj) where T : class
	{
		return gObj.GetComponentInChildren(typeof(T)) as T;
	}

	/// <summary>
	/// executes message with the component of type TI if it exists in gameobject's heirarchy. this executes on all behaviours that implement TI.
	/// parm is included in the action, to help reduce closures
	/// </summary>
	/// <typeparam name="TI">component type to get</typeparam>
	/// <typeparam name="TParm">type of the parameter to pass into the message</typeparam>
	/// <param name="gobj"></param>
	/// <param name="message">action to run on each component that matches TI</param>
	/// <param name="parm">the object to pass into the message. this reduces closures.</param>
	public static void DoMessage<TI, TParm>(this GameObject gobj, Action<TI, TParm> message, TParm parm) where TI : class
	{
		var ts = gobj.GetComponentsInChildren(typeof(TI));
		for (int i = 0; i < ts.Length; i++)
		{
			var comp = ts[i] as TI;
			if (comp != null)
			{
				message(comp, parm);
			}
		}
	}

	/// <summary>
	/// executes message with the component of type TI if it exists in gameobject's heirarchy. this executes for all behaviours that implement TI.
	/// It is recommended that you use the other DoMessage if you need to pass a variable into the message, to reduce garbage pressure due to lambdas.
	/// </summary>
	/// <typeparam name="TI"></typeparam>
	/// <param name="gobj"></param>
	/// <param name="message"></param>
	public static void DoMessage<TI>(this GameObject gobj, Action<TI> message) where TI : class
	{
		var ts = gobj.GetComponentsInChildren(typeof(TI));
		for (int i = 0; i < ts.Length; i++)
		{
			var comp = ts[i] as TI;
			if (comp != null)
			{
				message(comp);
			}
		}
	}

	public static void ReverseActiveState(this GameObject go)
	{
		go.SetActive(!go.activeSelf);
	}
}