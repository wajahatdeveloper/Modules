using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;

/* *****************************************************************************
 * File:    UnityGoExtensions.cs
 * Author:  Philip Pierce - Friday, September 26, 2014
 * Description:
 *  Unity extensions on GameObjects
 *  
 * History:
 *  Friday, September 26, 2014 - Created
 * ****************************************************************************/

/// <summary>
/// Unity extensions on GameObjects
/// </summary>
public static class GameObjectExtensions
{
	/// <summary>
	/// Returns the Vector3 distance between these two GameObjects
	/// </summary>
	/// <param name="go"></param>
	/// <param name="otherGO"></param>
	/// <returns></returns>
	public static float DistanceTo(this GameObject go, GameObject otherGO)
	{
		return Vector3.Distance(go.transform.position, otherGO.transform.position);
	}

	/// <summary>
	/// Returns the Vecto3 distance between these two points
	/// </summary>
	/// <param name="go"></param>
	/// <param name="pos"></param>
	/// <returns></returns>
	public static float DistanceTo(this GameObject go, Vector3 pos)
	{
		return Vector3.Distance(go.transform.position, pos);
	}
	
	#region GetComponentOnObject

	/// <summary>
	/// Returns a component attached to the game object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <param name="showErrorInConsole">when true, logs an error in the console if nothing found</param>
	/// <returns></returns>
	public static T GetComponentOnObject<T>(this GameObject go, bool showErrorInConsole) where T : Component
	{
		// get the component
		T component = go.GetComponent<T>();
		if ((showErrorInConsole) && (component == null))
			Debug.LogError(string.Format("Unable to find component '{0}' on object '{1}'", typeof(T).Name, go.name));

		return component;
	}

	/// <summary>
	/// Returns a component attached to the game object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="trans"></param>
	/// <param name="showErrorInConsole">when true, logs an error in the console if nothing found</param>
	/// <returns></returns>
	public static T GetComponentOnObject<T>(this Transform trans, bool showErrorInConsole) where T : Component
	{
		// get the component
		T component = trans.GetComponent<T>();
		if ((showErrorInConsole) && (component == null))
			Debug.LogError(string.Format("Unable to find component '{0}' on object '{1}'", typeof(T).Name, trans.name));

		return component;
	}

	// GetComponentOnObject
	#endregion

	#region GetComponentOnObjectOrParent

	/// <summary>
	/// Returns a component attached to the game object, or its parent
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <param name="showErrorInConsole">when true, logs an error in the console if nothing found</param>
	/// <returns></returns>
	public static T GetComponentOnObjectOrParent<T>(this GameObject go, bool showErrorInConsole) where T : Component
	{
		// get the component
		T component = go.GetComponentInParent<T>();
		if ((showErrorInConsole) && (component == null))
			Debug.LogError(string.Format("Unable to find component '{0}' on object (or parent) '{1}'", typeof(T).Name, go.name));

		return component;
	}

	// GetComponentOnObjectOrParent
	#endregion

	#region IsNullOrInactive

	/// <summary>
	/// Returns true if the GO is null or inactive
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool IsNullOrInactive(this GameObject go)
	{
		return ((go == null) || (!go.activeSelf));
	}

	// IsNullOrInactive
	#endregion

	#region IsActive

	/// <summary>
	/// Returns true if the GO is not null and is active
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool IsActive(this GameObject go)
	{
		return ((go != null) && (go.activeSelf));
	}

	// IsActive
	#endregion

	#region ActivateAndParent

	/// <summary>
	/// Activates this gameobject, starting with its parent
	/// </summary>
	/// <param name="go"></param>
	public static void ActivateAndParent(this GameObject go)
	{
		// exit if go is null
		if (go == null)
			return;

		// if this object has a parent, activate each parent first
		if (go.transform.parent != null)
			go.transform.parent.gameObject.ActivateAndParent();

		// activate this object
		go.SetActive(true);
	}

	// ActivateAndParent
	#endregion

	#region HasRigidbody

	/// <summary>
	/// Returns true if the object has a rigid body
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool HasRigidbody(this GameObject go)
	{
		return (go.GetComponent<Rigidbody>() != null);
	}

	// HasRigidbody
	#endregion

	#region HasCharacterController

	/// <summary>
	/// Returns true if the object has a character controller
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool HasCharacterController(this GameObject go)
	{
		return (go.GetComponent<CharacterController>() != null);
	}

	// HasCharacterController
	#endregion

	#region HasAnimation

	/// <summary>
	/// Returns true if the object has an animation
	/// </summary>
	/// <param name="go"></param>
	/// <returns></returns>
	public static bool HasAnimation(this GameObject go)
	{
		return (go.GetComponent<Animation>() != null);
	}

	// HasAnimation
	#endregion

	#region HasComponent

	/// <summary>
	/// Returns true if the game object has this component
	/// </summary>
	/// <param name="go"></param>
	public static bool HasComponent<T>(this GameObject go) where T : Component
	{
		return (go.GetComponent<T>() != null);
	}

	// HasComponent
	#endregion

	#region SetLayerRecursively

	/// <summary>
	/// Sets the layer for the game object and all its children
	/// </summary>
	/// <param name="go"></param>
	/// <param name="layer"></param>
	public static void SetLayerRecursively(this GameObject go, int layer)
	{
		go.layer = layer;
		foreach (Transform t in go.transform)
			t.gameObject.SetLayerRecursively(layer);
	}

	// SetLayerRecursively
	#endregion

	#region SetCollisionRecursively

	/// <summary>
	/// Enables or disables colliders on the game object and all its children
	/// </summary>
	/// <param name="go"></param>
	/// <param name="enabled"></param>
	public static void SetCollisionRecursively(this GameObject go, bool enabled)
	{
		Collider GCollide = go.GetComponent<Collider>();
		if (GCollide != null)
			GCollide.enabled = enabled;

		foreach (Transform t in go.transform)
			t.gameObject.SetCollisionRecursively(enabled);
	}

	// SetCollisionRecursively
	#endregion

	#region AddOrGetComponent

	public static T AddOrGetComponent<T>(this GameObject go) where T : Component
	{
		var component = go.GetComponent<T>();
		if (component == null)
		{
			component = go.AddComponent<T>();
		}
		return component;
	}

	// GetComponentsInChildrenWithTag
	#endregion

	#region GetComponentsInChildrenWithTag

	/// <summary>
	/// Returns all components in the game object and children with the specified tag
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <param name="tag"></param>
	/// <returns></returns>
	public static List<T> GetComponentsInChildrenWithTag<T>(this GameObject go, string tag) where T : Component
	{
		List<T> results = new List<T>();

		// check if the object has this tag
		if (go.CompareTag(tag))
			results.Add(go.GetComponent<T>());

		// loop through all children with this tag
		foreach (Transform t in go.transform)
			results.AddRange(t.gameObject.GetComponentsInChildrenWithTag<T>(tag));

		return results;
	}

	// GetComponentsInChildrenWithTag
	#endregion

	#region GetCollisionMask

	/// <summary>
	/// Returns the collision mask of the game object (all layers which this object can collide with)
	/// </summary>
	/// <param name="go"></param>
	/// <param name="layer">OPTIONAL. If omitted, it uses the layer of the calling GameObject, which is the most common/intuitive case (for me, at least). But you can specify a layer and it’ll hand you the collision mask for that layer instead.</param>
	/// <returns></returns>
	public static int GetCollisionMask(this GameObject go, int layer = -1)
	{
		// get the layer if one was not sent
		if (layer == -1)
			layer = go.layer;

		// get the mask on this layer
		int mask = 0;
		for (int i = 0; i < 32; i++)
			mask |= (Physics.GetIgnoreLayerCollision(layer, i) ? 0 : 1) << i;

		return mask;
	}

	// GetCollisionMask
	#endregion

	#region GetChildrenWithName

	/// <summary>
	/// Returns all children of the game object with the specified name
	/// </summary>
	/// <param name="go"></param>
	/// <param name="name"></param>
	/// <remarks>
	/// Suggested by: Vipsu
	/// Link: http://forum.unity3d.com/members/vipsu.138664/
	/// </remarks>
	public static Transform[] GetChildrenWithName(this GameObject go, string name)
	{
		// loop through and add matching children
		return go.transform.Cast<Transform>()
			.Where(w => w.name.Equals(name, StringComparison.CurrentCultureIgnoreCase))
			.Return(r => r.ToArray(), (Transform[])null);
	}

	// GetChildrenWithName
	#endregion

	#region GetChildrenComponent

	/// <summary>
	/// Returns a list of components found on all children matching this type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <remarks>
	/// Suggested by: Vipsu
	/// Link: http://forum.unity3d.com/members/vipsu.138664/
	/// </remarks>
	public static T[] GetChildrenComponent<T>(this GameObject go) where T : Component
	{
		// loop through and add matching children
		return go.transform.Cast<Transform>()
			.Where(w => w.gameObject.HasComponent<T>())
			.Return(r => r.ToList().UnityConvertAll(x => x.gameObject.GetComponent<T>()).ToArray(), new List<T>().ToArray());
	}

	// GetChildrenComponent
	#endregion

	#region GetInterface

	/// <summary>
	/// Returns the interface on this game object
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <remarks>
	/// Suggested by: A.Killingbeck
	/// Link: http://forum.unity3d.com/members/a-killingbeck.560711/
	/// </remarks>
	public static T GetInterface<T>(this GameObject go) where T : class
	{

		if (!typeof(T).IsInterface)
		{
			Debug.LogError(typeof(T).ToString() + " is not an interface");
			return null;
		}

		return go.GetComponents<Component>().OfType<T>().FirstOrDefault();
	}

	// GetInterface
	#endregion

	#region GetInterfaceInChildren

	/// <summary>
	/// Returns the first matching interface on this game object's children
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <remarks>
	/// Suggested by: A.Killingbeck
	/// Link: http://forum.unity3d.com/members/a-killingbeck.560711/
	/// </remarks>
	public static T GetInterfaceInChildren<T>(this GameObject go) where T : class
	{

		if (!typeof(T).IsInterface)
		{
			Debug.LogError(typeof(T).ToString() + " is not an interface");
			return null;
		}

		return go.GetComponentsInChildren<Component>().OfType<T>().FirstOrDefault();
	}

	// GetInterfaceInChildren
	#endregion

	#region GetInterfaces

	/// <summary>
	/// Returns all interfaces on this game object matching this type
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <remarks>
	/// Suggested by: A.Killingbeck
	/// Link: http://forum.unity3d.com/members/a-killingbeck.560711/
	/// </remarks>
	public static IEnumerable<T> GetInterfaces<T>(this GameObject go) where T : class
	{

		if (!typeof(T).IsInterface)
		{
			Debug.LogError(typeof(T).ToString() + " is not an interface");
			return Enumerable.Empty<T>();
		}

		return go.GetComponents<Component>().OfType<T>();
	}

	// GetInterfaces
	#endregion

	#region GetInterfacesInChildren

	/// <summary>
	/// Returns all matching interfaces on this game object's children
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <param name="go"></param>
	/// <remarks>
	/// Suggested by: A.Killingbeck
	/// Link: http://forum.unity3d.com/members/a-killingbeck.560711/
	/// </remarks>
	public static IEnumerable<T> GetInterfacesInChildren<T>(this GameObject go) where T : class
	{
		if (!typeof(T).IsInterface)
		{
			Debug.LogError(typeof(T).ToString() + " is not an interface");
			return Enumerable.Empty<T>();
		}

		return go.GetComponentsInChildren<Component>(true).OfType<T>();
	}

	// GetInterfacesInChildren
	#endregion


	// Helper function to access all components of a type on the game object.
	public static void ForEachChildOfType<T>(this GameObject gameObject, Action<T> callback)
	{
		foreach (T t in gameObject.GetComponentsInChildren<T>())
		{
			callback(t);
		}
	}

	// Helper function to access all components of a type on root of the given the game object.
	public static void ForEachRootChildOfType<T>(this GameObject gameObject, Action<T> callback)
	{
		gameObject.transform.root.gameObject.ForEachChildOfType<T>(callback);
	}

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

	#region IEnumerables

	public static IEnumerable<GameObject> Ancestors(this IEnumerable<GameObject> source)
	{
		foreach (var item in source)
		{
			var e = item.Ancestors().GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Returns a collection of GameObjects that contains every GameObject in the source collection, and the ancestors of every GameObject in the source collection.</summary>
	public static IEnumerable<GameObject> AncestorsAndSelf(this IEnumerable<GameObject> source)
	{
		foreach (var item in source)
		{
			var e = item.AncestorsAndSelf().GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Returns a collection of GameObjects that contains the descendant GameObjects of every GameObject in the source collection.</summary>
	public static IEnumerable<GameObject> Descendants(this IEnumerable<GameObject> source, Func<Transform, bool> descendIntoChildren = null)
	{
		foreach (var item in source)
		{
			var e = item.Descendants(descendIntoChildren).GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Returns a collection of GameObjects that contains every GameObject in the source collection, and the descendent GameObjects of every GameObject in the source collection.</summary>
	public static IEnumerable<GameObject> DescendantsAndSelf(this IEnumerable<GameObject> source, Func<Transform, bool> descendIntoChildren = null)
	{
		foreach (var item in source)
		{
			var e = item.DescendantsAndSelf(descendIntoChildren).GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Returns a collection of the child GameObjects of every GameObject in the source collection.</summary>
	public static IEnumerable<GameObject> Children(this IEnumerable<GameObject> source)
	{
		foreach (var item in source)
		{
			var e = item.Children().GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Returns a collection of GameObjects that contains every GameObject in the source collection, and the child GameObjects of every GameObject in the source collection.</summary>
	public static IEnumerable<GameObject> ChildrenAndSelf(this IEnumerable<GameObject> source)
	{
		foreach (var item in source)
		{
			var e = item.ChildrenAndSelf().GetEnumerator();
			while (e.MoveNext())
			{
				yield return e.Current;
			}
		}
	}

	/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
	/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
	/// <param name="detachParent">set to parent = null.</param>
	public static void Destroy(this IEnumerable<GameObject> source, bool useDestroyImmediate = false, bool detachParent = false)
	{
		if (detachParent)
		{
			var l = new List<GameObject>(source); // avoid halloween problem
			var e = l.GetEnumerator(); // get struct enumerator for avoid unity's compiler bug(avoid boxing)
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, true);
			}
		}
		else
		{
			foreach (var item in source)
			{
				item.Destroy(useDestroyImmediate, false); // doesn't detach.
			}
		}
	}

	#endregion

	/// <summary>Destroy this GameObject safety(check null).</summary>
	/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
	/// <param name="detachParent">set to parent = null.</param>
	public static void Destroy(this GameObject self, bool useDestroyImmediate = false, bool detachParent = false)
	{
		if (self == null) return;

		if (detachParent)
		{
			self.transform.SetParent(null);
		}

		if (useDestroyImmediate)
		{
			GameObject.DestroyImmediate(self);
		}
		else
		{
			GameObject.Destroy(self);
		}
	}

	#region Traverse

	public static GameObject Parent(this GameObject origin)
	{
		if (origin == null) return null;

		var parentTransform = origin.transform.parent;
		if (parentTransform == null) return null;

		return parentTransform.gameObject;
	}

	/// <summary>Gets the first child GameObject with the specified name. If there is no GameObject with the speficided name, returns null.</summary>
	public static GameObject Child(this GameObject origin, string name)
	{
		if (origin == null) return null;

		var child = origin.transform.Find(name); // transform.find can get inactive object
		if (child == null) return null;
		return child.gameObject;
	}

	/// <summary>Returns a collection of the child GameObjects.</summary>
	public static ChildrenEnumerable Children(this GameObject origin)
	{
		return new ChildrenEnumerable(origin, false);
	}

	/// <summary>Returns a collection of GameObjects that contain this GameObject, and the child GameObjects.</summary>
	public static ChildrenEnumerable ChildrenAndSelf(this GameObject origin)
	{
		return new ChildrenEnumerable(origin, true);
	}

	/// <summary>Returns a collection of the ancestor GameObjects of this GameObject.</summary>
	public static AncestorsEnumerable Ancestors(this GameObject origin)
	{
		return new AncestorsEnumerable(origin, false);
	}

	/// <summary>Returns a collection of GameObjects that contain this element, and the ancestors of this GameObject.</summary>
	public static AncestorsEnumerable AncestorsAndSelf(this GameObject origin)
	{
		return new AncestorsEnumerable(origin, true);
	}

	/// <summary>Returns a collection of the descendant GameObjects.</summary>
	public static DescendantsEnumerable Descendants(this GameObject origin, Func<Transform, bool> descendIntoChildren = null)
	{
		return new DescendantsEnumerable(origin, false, descendIntoChildren);
	}

	/// <summary>Returns a collection of GameObjects that contain this GameObject, and all descendant GameObjects of this GameObject.</summary>
	public static DescendantsEnumerable DescendantsAndSelf(this GameObject origin, Func<Transform, bool> descendIntoChildren = null)
	{
		return new DescendantsEnumerable(origin, true, descendIntoChildren);
	}

	/// <summary>Returns a collection of the sibling GameObjects before this GameObject.</summary>
	public static BeforeSelfEnumerable BeforeSelf(this GameObject origin)
	{
		return new BeforeSelfEnumerable(origin, false);
	}

	/// <summary>Returns a collection of GameObjects that contain this GameObject, and the sibling GameObjects before this GameObject.</summary>
	public static BeforeSelfEnumerable BeforeSelfAndSelf(this GameObject origin)
	{
		return new BeforeSelfEnumerable(origin, true);
	}

	/// <summary>Returns a collection of the sibling GameObjects after this GameObject.</summary>
	public static AfterSelfEnumerable AfterSelf(this GameObject origin)
	{
		return new AfterSelfEnumerable(origin, false);
	}

	/// <summary>Returns a collection of GameObjects that contain this GameObject, and the sibling GameObjects after this GameObject.</summary>
	public static AfterSelfEnumerable AfterSelfAndSelf(this GameObject origin)
	{
		return new AfterSelfEnumerable(origin, true);
	}

	// Implements hand struct enumerator.

	public struct ChildrenEnumerable : IEnumerable<GameObject>
	{
		readonly GameObject origin;
		readonly bool withSelf;

		public ChildrenEnumerable(GameObject origin, bool withSelf)
		{
			this.origin = origin;
			this.withSelf = withSelf;
		}

		/// <summary>Returns a collection of specified component in the source collection.</summary>
		public OfComponentEnumerable<T> OfComponent<T>()
			where T : Component
		{
			return new OfComponentEnumerable<T>(ref this);
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		/// <param name="detachParent">set to parent = null.</param>
		public void Destroy(bool useDestroyImmediate = false, bool detachParent = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, false);
			}
			if (detachParent)
			{
				origin.transform.DetachChildren();
				if (withSelf)
				{
#if !(UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_4 || UNITY_4_5)
					origin.transform.SetParent(null);
#else
                        origin.transform.parent = null;
#endif
				}
			}
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(Func<GameObject, bool> predicate, bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				var item = e.Current;
				if (predicate(item))
				{
					item.Destroy(useDestroyImmediate, false);
				}
			}
		}

		public Enumerator GetEnumerator()
		{
			// check GameObject is destroyed only on GetEnumerator timing
			return (origin == null)
				? new Enumerator(null, withSelf, false)
				: new Enumerator(origin.transform, withSelf, true);
		}

		IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region LINQ

		int GetChildrenSize()
		{
			return origin.transform.childCount + (withSelf ? 1 : 0);
		}

		public void ForEach(Action<GameObject> action)
		{
			var e = this.GetEnumerator();
			while (e.MoveNext())
			{
				action(e.Current);
			}
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(ref GameObject[] array)
		{
			var index = 0;

			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? GetChildrenSize() : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(Func<GameObject, bool> filter, ref GameObject[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? GetChildrenSize() : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? GetChildrenSize() : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? GetChildrenSize() : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				var state = let(item);

				if (!filter(state)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? GetChildrenSize() : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(state);
			}

			return index;
		}

		public GameObject[] ToArray()
		{
			var array = new GameObject[GetChildrenSize()];
			var len = ToArrayNonAlloc(ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject[] ToArray(Func<GameObject, bool> filter)
		{
			var array = new GameObject[GetChildrenSize()];
			var len = ToArrayNonAlloc(filter, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, T> selector)
		{
			var array = new T[GetChildrenSize()];
			var len = ToArrayNonAlloc<T>(selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector)
		{
			var array = new T[GetChildrenSize()];
			var len = ToArrayNonAlloc(filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector)
		{
			var array = new T[GetChildrenSize()];
			var len = ToArrayNonAlloc(let, filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject First()
		{
			var e = this.GetEnumerator();
			if (e.MoveNext())
			{
				return e.Current;
			}
			else
			{
				throw new InvalidOperationException("sequence is empty.");
			}
		}

		public GameObject FirstOrDefault()
		{
			var e = this.GetEnumerator();
			return (e.MoveNext())
				? e.Current
				: null;
		}

		#endregion

		public struct Enumerator : IEnumerator<GameObject>
		{
			readonly int childCount; // childCount is fixed when GetEnumerator is called.

			readonly Transform originTransform;
			readonly bool canRun;

			bool withSelf;
			int currentIndex;
			GameObject current;

			internal Enumerator(Transform originTransform, bool withSelf, bool canRun)
			{
				this.originTransform = originTransform;
				this.withSelf = withSelf;
				this.childCount = canRun ? originTransform.childCount : 0;
				this.currentIndex = -1;
				this.canRun = canRun;
				this.current = null;
			}

			public bool MoveNext()
			{
				if (!canRun) return false;

				if (withSelf)
				{
					current = originTransform.gameObject;
					withSelf = false;
					return true;
				}

				currentIndex++;
				if (currentIndex < childCount)
				{
					var child = originTransform.GetChild(currentIndex);
					current = child.gameObject;
					return true;
				}

				return false;
			}

			public GameObject Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}

		public struct OfComponentEnumerable<T> : IEnumerable<T>
			where T : Component
		{
			ChildrenEnumerable parent;

			public OfComponentEnumerable(ref ChildrenEnumerable parent)
			{
				this.parent = parent;
			}

			public OfComponentEnumerator<T> GetEnumerator()
			{
				return new OfComponentEnumerator<T>(ref this.parent);
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#region LINQ

			public void ForEach(Action<T> action)
			{
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					action(e.Current);
				}
			}

			public T First()
			{
				var e = this.GetEnumerator();
				if (e.MoveNext())
				{
					return e.Current;
				}
				else
				{
					throw new InvalidOperationException("sequence is empty.");
				}
			}

			public T FirstOrDefault()
			{
				var e = this.GetEnumerator();
				return (e.MoveNext())
					? e.Current
					: null;
			}

			public T[] ToArray()
			{
				var array = new T[parent.GetChildrenSize()];
				var len = ToArrayNonAlloc(ref array);
				if (array.Length != len)
				{
					Array.Resize(ref array, len);
				}
				return array;
			}

			/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
			public int ToArrayNonAlloc(ref T[] array)
			{
				var index = 0;
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					if (array.Length == index)
					{
						var newSize = (index == 0) ? parent.GetChildrenSize() : index * 2;
						Array.Resize(ref array, newSize);
					}
					array[index++] = e.Current;
				}

				return index;
			}

			#endregion
		}

		public struct OfComponentEnumerator<T> : IEnumerator<T>
			where T : Component
		{
			Enumerator enumerator; // enumerator is mutable
			T current;

#if UNITY_EDITOR
			static List<T> componentCache = new List<T>(); // for no allocate on UNITY_EDITOR
#endif

			public OfComponentEnumerator(ref ChildrenEnumerable parent)
			{
				this.enumerator = parent.GetEnumerator();
				this.current = default(T);
			}

			public bool MoveNext()
			{
				while (enumerator.MoveNext())
				{
#if UNITY_EDITOR
					enumerator.Current.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						current = componentCache[0];
						componentCache.Clear();
						return true;
					}
#else
                        
                        var component = enumerator.Current.GetComponent<T>();
                        if (component != null)
                        {
                            current = component;
                            return true;
                        }
#endif
				}

				return false;
			}

			public T Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}
	}

	public struct AncestorsEnumerable : IEnumerable<GameObject>
	{
		readonly GameObject origin;
		readonly bool withSelf;

		public AncestorsEnumerable(GameObject origin, bool withSelf)
		{
			this.origin = origin;
			this.withSelf = withSelf;
		}

		/// <summary>Returns a collection of specified component in the source collection.</summary>
		public OfComponentEnumerable<T> OfComponent<T>()
			where T : Component
		{
			return new OfComponentEnumerable<T>(ref this);
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, false);
			}
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(Func<GameObject, bool> predicate, bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				var item = e.Current;
				if (predicate(item))
				{
					item.Destroy(useDestroyImmediate, false);
				}
			}
		}

		public Enumerator GetEnumerator()
		{
			// check GameObject is destroyed only on GetEnumerator timing
			return (origin == null)
				? new Enumerator(null, null, withSelf, false)
				: new Enumerator(origin, origin.transform, withSelf, true);
		}

		IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region LINQ

		public void ForEach(Action<GameObject> action)
		{
			var e = this.GetEnumerator();
			while (e.MoveNext())
			{
				action(e.Current);
			}
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(ref GameObject[] array)
		{
			var index = 0;

			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(Func<GameObject, bool> filter, ref GameObject[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				var state = let(item);

				if (!filter(state)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(state);
			}

			return index;
		}

		public GameObject[] ToArray()
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject[] ToArray(Func<GameObject, bool> filter)
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(filter, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc<T>(selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(let, filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject First()
		{
			var e = this.GetEnumerator();
			if (e.MoveNext())
			{
				return e.Current;
			}
			else
			{
				throw new InvalidOperationException("sequence is empty.");
			}
		}

		public GameObject FirstOrDefault()
		{
			var e = this.GetEnumerator();
			return (e.MoveNext())
				? e.Current
				: null;
		}

		#endregion

		public struct Enumerator : IEnumerator<GameObject>
		{
			readonly bool canRun;

			GameObject current;
			Transform currentTransform;
			bool withSelf;

			internal Enumerator(GameObject origin, Transform originTransform, bool withSelf, bool canRun)
			{
				this.current = origin;
				this.currentTransform = originTransform;
				this.withSelf = withSelf;
				this.canRun = canRun;
			}

			public bool MoveNext()
			{
				if (!canRun) return false;

				if (withSelf)
				{
					// withSelf, use origin and originTransform
					withSelf = false;
					return true;
				}

				var parentTransform = currentTransform.parent;
				if (parentTransform != null)
				{
					current = parentTransform.gameObject;
					currentTransform = parentTransform;
					return true;
				}

				return false;
			}

			public GameObject Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}

		public struct OfComponentEnumerable<T> : IEnumerable<T>
			where T : Component
		{
			AncestorsEnumerable parent;

			public OfComponentEnumerable(ref AncestorsEnumerable parent)
			{
				this.parent = parent;
			}

			public OfComponentEnumerator<T> GetEnumerator()
			{
				return new OfComponentEnumerator<T>(ref parent);
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#region LINQ

			public void ForEach(Action<T> action)
			{
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					action(e.Current);
				}
			}

			public T First()
			{
				var e = this.GetEnumerator();
				if (e.MoveNext())
				{
					return e.Current;
				}
				else
				{
					throw new InvalidOperationException("sequence is empty.");
				}
			}

			public T FirstOrDefault()
			{
				var e = this.GetEnumerator();
				return (e.MoveNext())
					? e.Current
					: null;
			}

			public T[] ToArray()
			{
				var array = new T[4];
				var len = ToArrayNonAlloc(ref array);
				if (array.Length != len)
				{
					Array.Resize(ref array, len);
				}
				return array;
			}

			/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
			public int ToArrayNonAlloc(ref T[] array)
			{
				var index = 0;
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					if (array.Length == index)
					{
						var newSize = (index == 0) ? 4 : index * 2;
						Array.Resize(ref array, newSize);
					}
					array[index++] = e.Current;
				}

				return index;
			}

			#endregion
		}

		public struct OfComponentEnumerator<T> : IEnumerator<T>
			where T : Component
		{
			Enumerator enumerator; // enumerator is mutable
			T current;

#if UNITY_EDITOR
			static List<T> componentCache = new List<T>(); // for no allocate on UNITY_EDITOR
#endif

			public OfComponentEnumerator(ref AncestorsEnumerable parent)
			{
				this.enumerator = parent.GetEnumerator();
				this.current = default(T);
			}

			public bool MoveNext()
			{
				while (enumerator.MoveNext())
				{
#if UNITY_EDITOR
					enumerator.Current.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						current = componentCache[0];
						componentCache.Clear();
						return true;
					}
#else
                        
                        var component = enumerator.Current.GetComponent<T>();
                        if (component != null)
                        {
                            current = component;
                            return true;
                        }
#endif
				}

				return false;
			}

			public T Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}
	}

	public struct DescendantsEnumerable : IEnumerable<GameObject>
	{
		static readonly Func<Transform, bool> alwaysTrue = _ => true;

		readonly GameObject origin;
		readonly bool withSelf;
		readonly Func<Transform, bool> descendIntoChildren;

		public DescendantsEnumerable(GameObject origin, bool withSelf, Func<Transform, bool> descendIntoChildren)
		{
			this.origin = origin;
			this.withSelf = withSelf;
			this.descendIntoChildren = descendIntoChildren ?? alwaysTrue;
		}

		/// <summary>Returns a collection of specified component in the source collection.</summary>
		public OfComponentEnumerable<T> OfComponent<T>()
			where T : Component
		{
			return new OfComponentEnumerable<T>(ref this);
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, false);
			}
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(Func<GameObject, bool> predicate, bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				var item = e.Current;
				if (predicate(item))
				{
					item.Destroy(useDestroyImmediate, false);
				}
			}
		}

		public Enumerator GetEnumerator()
		{
			// check GameObject is destroyed only on GetEnumerator timing
			if (origin == null)
			{
				return new Enumerator(null, withSelf, false, null, descendIntoChildren);
			}

			InternalUnsafeRefStack refStack;
			if (InternalUnsafeRefStack.RefStackPool.Count != 0)
			{
				refStack = InternalUnsafeRefStack.RefStackPool.Dequeue();
				refStack.Reset();
			}
			else
			{
				refStack = new InternalUnsafeRefStack(6);
			}

			return new Enumerator(origin.transform, withSelf, true, refStack, descendIntoChildren);
		}

		IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region LINQ

		void ResizeArray<T>(ref int index, ref T[] array)
		{
			if (array.Length == index)
			{
				var newSize = (index == 0) ? 4 : index * 2;
				Array.Resize(ref array, newSize);
			}
		}

		void DescendantsCore(ref Transform transform, ref Action<GameObject> action)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				action(child.gameObject);
				DescendantsCore(ref child, ref action);
			}
		}

		void DescendantsCore(ref Transform transform, ref int index, ref GameObject[] array)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				ResizeArray(ref index, ref array);
				array[index++] = child.gameObject;
				DescendantsCore(ref child, ref index, ref array);
			}
		}

		void DescendantsCore(ref Func<GameObject, bool> filter, ref Transform transform, ref int index, ref GameObject[] array)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				var childGameObject = child.gameObject;
				if (filter(childGameObject))
				{
					ResizeArray(ref index, ref array);
					array[index++] = childGameObject;
				}
				DescendantsCore(ref filter, ref child, ref index, ref array);
			}
		}

		void DescendantsCore<T>(ref Func<GameObject, T> selector, ref Transform transform, ref int index, ref T[] array)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				ResizeArray(ref index, ref array);
				array[index++] = selector(child.gameObject);
				DescendantsCore(ref selector, ref child, ref index, ref array);
			}
		}

		void DescendantsCore<T>(ref Func<GameObject, bool> filter, ref Func<GameObject, T> selector, ref Transform transform, ref int index, ref T[] array)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				var childGameObject = child.gameObject;
				if (filter(childGameObject))
				{
					ResizeArray(ref index, ref array);
					array[index++] = selector(childGameObject);
				}
				DescendantsCore(ref filter, ref selector, ref child, ref index, ref array);
			}
		}

		void DescendantsCore<TState, T>(ref Func<GameObject, TState> let, ref Func<TState, bool> filter, ref Func<TState, T> selector, ref Transform transform, ref int index, ref T[] array)
		{
			if (!descendIntoChildren(transform)) return;

			var childCount = transform.childCount;
			for (int i = 0; i < childCount; i++)
			{
				var child = transform.GetChild(i);
				var state = let(child.gameObject);
				if (filter(state))
				{
					ResizeArray(ref index, ref array);
					array[index++] = selector(state);
				}
				DescendantsCore(ref let, ref filter, ref selector, ref child, ref index, ref array);
			}
		}

		/// <summary>Use internal iterator for performance optimization.</summary>
		/// <param name="action"></param>
		public void ForEach(Action<GameObject> action)
		{
			if (withSelf)
			{
				action(origin);
			}
			var originTransform = origin.transform;
			DescendantsCore(ref originTransform, ref action);
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(ref GameObject[] array)
		{
			var index = 0;
			if (withSelf)
			{
				ResizeArray(ref index, ref array);
				array[index++] = origin;
			}

			var originTransform = origin.transform;
			DescendantsCore(ref originTransform, ref index, ref array);

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(Func<GameObject, bool> filter, ref GameObject[] array)
		{
			var index = 0;
			if (withSelf && filter(origin))
			{
				ResizeArray(ref index, ref array);
				array[index++] = origin;
			}
			var originTransform = origin.transform;
			DescendantsCore(ref filter, ref originTransform, ref index, ref array);

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			if (withSelf)
			{
				ResizeArray(ref index, ref array);
				array[index++] = selector(origin);
			}
			var originTransform = origin.transform;
			DescendantsCore(ref selector, ref originTransform, ref index, ref array);

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			if (withSelf && filter(origin))
			{
				ResizeArray(ref index, ref array);
				array[index++] = selector(origin);
			}
			var originTransform = origin.transform;
			DescendantsCore(ref filter, ref selector, ref originTransform, ref index, ref array);

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector, ref T[] array)
		{
			var index = 0;
			if (withSelf)
			{
				var state = let(origin);
				if (filter(state))
				{
					ResizeArray(ref index, ref array);
					array[index++] = selector(state);
				}
			}

			var originTransform = origin.transform;
			DescendantsCore(ref let, ref filter, ref selector, ref originTransform, ref index, ref array);

			return index;
		}

		public GameObject[] ToArray()
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject[] ToArray(Func<GameObject, bool> filter)
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(filter, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc<T>(selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(let, filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject First()
		{
			var e = this.GetEnumerator();
			try
			{
				if (e.MoveNext())
				{
					return e.Current;
				}
				else
				{
					throw new InvalidOperationException("sequence is empty.");
				}
			}
			finally
			{
				e.Dispose();
			}
		}

		public GameObject FirstOrDefault()
		{
			var e = this.GetEnumerator();
			try
			{
				return (e.MoveNext())
					? e.Current
					: null;
			}
			finally
			{
				e.Dispose();
			}
		}

		#endregion

		internal class InternalUnsafeRefStack
		{
			public static Queue<InternalUnsafeRefStack> RefStackPool = new Queue<InternalUnsafeRefStack>();

			public int size = 0;
			public Enumerator[] array; // Pop = this.array[--size];

			public InternalUnsafeRefStack(int initialStackDepth)
			{
				array = new GameObjectExtensions.DescendantsEnumerable.Enumerator[initialStackDepth];
			}

			public void Push(ref Enumerator e)
			{
				if (size == array.Length)
				{
					Array.Resize(ref array, array.Length * 2);
				}
				array[size++] = e;
			}

			public void Reset()
			{
				size = 0;
			}
		}

		public struct Enumerator : IEnumerator<GameObject>
		{
			readonly int childCount; // childCount is fixed when GetEnumerator is called.

			readonly Transform originTransform;
			bool canRun;

			bool withSelf;
			int currentIndex;
			GameObject current;
			InternalUnsafeRefStack sharedStack;
			Func<Transform, bool> descendIntoChildren;

			internal Enumerator(Transform originTransform, bool withSelf, bool canRun, InternalUnsafeRefStack sharedStack, Func<Transform, bool> descendIntoChildren)
			{
				this.originTransform = originTransform;
				this.withSelf = withSelf;
				this.childCount = canRun ? originTransform.childCount : 0;
				this.currentIndex = -1;
				this.canRun = canRun;
				this.current = null;
				this.sharedStack = sharedStack;
				this.descendIntoChildren = descendIntoChildren;
			}

			public bool MoveNext()
			{
				if (!canRun) return false;

				while (sharedStack.size != 0)
				{
					if (sharedStack.array[sharedStack.size - 1].MoveNextCore(true, out current))
					{
						return true;
					}
				}

				if (!withSelf && !descendIntoChildren(originTransform))
				{
					// reuse
					canRun = false;
					InternalUnsafeRefStack.RefStackPool.Enqueue(sharedStack);
					return false;
				}

				if (MoveNextCore(false, out current))
				{
					return true;
				}
				else
				{
					// reuse
					canRun = false;
					InternalUnsafeRefStack.RefStackPool.Enqueue(sharedStack);
					return false;
				}
			}

			bool MoveNextCore(bool peek, out GameObject current)
			{
				if (withSelf)
				{
					current = originTransform.gameObject;
					withSelf = false;
					return true;
				}

				++currentIndex;
				if (currentIndex < childCount)
				{
					var item = originTransform.GetChild(currentIndex);
					if (descendIntoChildren(item))
					{
						var childEnumerator = new Enumerator(item, true, true, sharedStack, descendIntoChildren);
						sharedStack.Push(ref childEnumerator);
						return sharedStack.array[sharedStack.size - 1].MoveNextCore(true, out current);
					}
					else
					{
						current = item.gameObject;
						return true;
					}
				}

				if (peek)
				{
					sharedStack.size--; // Pop
				}

				current = null;
				return false;
			}

			public GameObject Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }

			public void Dispose()
			{
				if (canRun)
				{
					canRun = false;
					InternalUnsafeRefStack.RefStackPool.Enqueue(sharedStack);
				}
			}

			public void Reset() { throw new NotSupportedException(); }
		}

		public struct OfComponentEnumerable<T> : IEnumerable<T>
			where T : Component
		{
			DescendantsEnumerable parent;

			public OfComponentEnumerable(ref DescendantsEnumerable parent)
			{
				this.parent = parent;
			}

			public OfComponentEnumerator<T> GetEnumerator()
			{
				return new OfComponentEnumerator<T>(ref parent);
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#region LINQ

			public T First()
			{
				var e = this.GetEnumerator();
				try
				{
					if (e.MoveNext())
					{
						return e.Current;
					}
					else
					{
						throw new InvalidOperationException("sequence is empty.");
					}
				}
				finally
				{
					e.Dispose();
				}
			}

			public T FirstOrDefault()
			{
				var e = this.GetEnumerator();
				try
				{
					return (e.MoveNext())
						? e.Current
						: null;
				}
				finally
				{
					e.Dispose();
				}
			}

			/// <summary>Use internal iterator for performance optimization.</summary>
			public void ForEach(Action<T> action)
			{
				if (parent.withSelf)
				{
					T component = default(T);
#if UNITY_EDITOR
					parent.origin.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						component = componentCache[0];
						componentCache.Clear();
					}
#else
                        component = parent.origin.GetComponent<T>();
#endif

					if (component != null)
					{
						action(component);
					}
				}

				var originTransform = parent.origin.transform;
				OfComponentDescendantsCore(ref originTransform, ref action);
			}


			public T[] ToArray()
			{
				var array = new T[4];
				var len = ToArrayNonAlloc(ref array);
				if (array.Length != len)
				{
					Array.Resize(ref array, len);
				}
				return array;
			}

#if UNITY_EDITOR
			static List<T> componentCache = new List<T>(); // for no allocate on UNITY_EDITOR
#endif

			void OfComponentDescendantsCore(ref Transform transform, ref Action<T> action)
			{
				if (!parent.descendIntoChildren(transform)) return;

				var childCount = transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					var child = transform.GetChild(i);

					T component = default(T);
#if UNITY_EDITOR
					child.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						component = componentCache[0];
						componentCache.Clear();
					}
#else
                        component = child.GetComponent<T>();
#endif

					if (component != null)
					{
						action(component);
					}
					OfComponentDescendantsCore(ref child, ref action);
				}
			}

			void OfComponentDescendantsCore(ref Transform transform, ref int index, ref T[] array)
			{
				if (!parent.descendIntoChildren(transform)) return;

				var childCount = transform.childCount;
				for (int i = 0; i < childCount; i++)
				{
					var child = transform.GetChild(i);
					T component = default(T);
#if UNITY_EDITOR
					child.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						component = componentCache[0];
						componentCache.Clear();
					}
#else
                        component = child.GetComponent<T>();
#endif

					if (component != null)
					{
						if (array.Length == index)
						{
							var newSize = (index == 0) ? 4 : index * 2;
							Array.Resize(ref array, newSize);
						}

						array[index++] = component;
					}
					OfComponentDescendantsCore(ref child, ref index, ref array);
				}
			}

			/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
			public int ToArrayNonAlloc(ref T[] array)
			{
				var index = 0;
				if (parent.withSelf)
				{
					T component = default(T);
#if UNITY_EDITOR
					parent.origin.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						component = componentCache[0];
						componentCache.Clear();
					}
#else
                        component = parent.origin.GetComponent<T>();
#endif

					if (component != null)
					{
						if (array.Length == index)
						{
							var newSize = (index == 0) ? 4 : index * 2;
							Array.Resize(ref array, newSize);
						}

						array[index++] = component;
					}
				}

				var originTransform = parent.origin.transform;
				OfComponentDescendantsCore(ref originTransform, ref index, ref array);

				return index;
			}

			#endregion
		}

		public struct OfComponentEnumerator<T> : IEnumerator<T>
			where T : Component
		{
			Enumerator enumerator; // enumerator is mutable
			T current;

#if UNITY_EDITOR
			static List<T> componentCache = new List<T>(); // for no allocate on UNITY_EDITOR
#endif

			public OfComponentEnumerator(ref DescendantsEnumerable parent)
			{
				this.enumerator = parent.GetEnumerator();
				this.current = default(T);
			}

			public bool MoveNext()
			{
				while (enumerator.MoveNext())
				{
#if UNITY_EDITOR
					enumerator.Current.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						current = componentCache[0];
						componentCache.Clear();
						return true;
					}
#else
                        
                        var component = enumerator.Current.GetComponent<T>();
                        if (component != null)
                        {
                            current = component;
                            return true;
                        }
#endif
				}

				return false;
			}

			public T Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }

			public void Dispose()
			{
				enumerator.Dispose();
			}

			public void Reset() { throw new NotSupportedException(); }
		}
	}

	public struct BeforeSelfEnumerable : IEnumerable<GameObject>
	{
		readonly GameObject origin;
		readonly bool withSelf;

		public BeforeSelfEnumerable(GameObject origin, bool withSelf)
		{
			this.origin = origin;
			this.withSelf = withSelf;
		}

		/// <summary>Returns a collection of specified component in the source collection.</summary>
		public OfComponentEnumerable<T> OfComponent<T>()
			where T : Component
		{
			return new OfComponentEnumerable<T>(ref this);
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, false);
			}
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(Func<GameObject, bool> predicate, bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				var item = e.Current;
				if (predicate(item))
				{
					item.Destroy(useDestroyImmediate, false);
				}
			}
		}

		public Enumerator GetEnumerator()
		{
			// check GameObject is destroyed only on GetEnumerator timing
			return (origin == null)
				? new Enumerator(null, withSelf, false)
				: new Enumerator(origin.transform, withSelf, true);
		}

		IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region LINQ

		public void ForEach(Action<GameObject> action)
		{
			var e = this.GetEnumerator();
			while (e.MoveNext())
			{
				action(e.Current);
			}
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(ref GameObject[] array)
		{
			var index = 0;

			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(Func<GameObject, bool> filter, ref GameObject[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				var state = let(item);

				if (!filter(state)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(state);
			}

			return index;
		}

		public GameObject[] ToArray()
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject[] ToArray(Func<GameObject, bool> filter)
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(filter, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc<T>(selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(let, filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject First()
		{
			var e = this.GetEnumerator();
			if (e.MoveNext())
			{
				return e.Current;
			}
			else
			{
				throw new InvalidOperationException("sequence is empty.");
			}
		}

		public GameObject FirstOrDefault()
		{
			var e = this.GetEnumerator();
			return (e.MoveNext())
				? e.Current
				: null;
		}

		#endregion

		public struct Enumerator : IEnumerator<GameObject>
		{
			readonly int childCount; // childCount is fixed when GetEnumerator is called.
			readonly Transform originTransform;
			bool canRun;

			bool withSelf;
			int currentIndex;
			GameObject current;
			Transform parent;

			internal Enumerator(Transform originTransform, bool withSelf, bool canRun)
			{
				this.originTransform = originTransform;
				this.withSelf = withSelf;
				this.currentIndex = -1;
				this.canRun = canRun;
				this.current = null;
				this.parent = originTransform.parent;
				this.childCount = (parent != null) ? parent.childCount : 0;
			}

			public bool MoveNext()
			{
				if (!canRun) return false;

				if (parent == null) goto RETURN_SELF;

				currentIndex++;
				if (currentIndex < childCount)
				{
					var item = parent.GetChild(currentIndex);

					if (item == originTransform)
					{
						goto RETURN_SELF;
					}

					current = item.gameObject;
					return true;
				}

			RETURN_SELF:
				if (withSelf)
				{
					current = originTransform.gameObject;
					withSelf = false;
					canRun = false; // reached self, run complete.
					return true;
				}

				return false;
			}

			public GameObject Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}

		public struct OfComponentEnumerable<T> : IEnumerable<T>
			where T : Component
		{
			BeforeSelfEnumerable parent;

			public OfComponentEnumerable(ref BeforeSelfEnumerable parent)
			{
				this.parent = parent;
			}

			public OfComponentEnumerator<T> GetEnumerator()
			{
				return new OfComponentEnumerator<T>(ref parent);
			}

			IEnumerator<T> IEnumerable<T>.GetEnumerator()
			{
				return GetEnumerator();
			}

			IEnumerator IEnumerable.GetEnumerator()
			{
				return GetEnumerator();
			}

			#region LINQ

			public void ForEach(Action<T> action)
			{
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					action(e.Current);
				}
			}

			public T First()
			{
				var e = this.GetEnumerator();
				if (e.MoveNext())
				{
					return e.Current;
				}
				else
				{
					throw new InvalidOperationException("sequence is empty.");
				}
			}

			public T FirstOrDefault()
			{
				var e = this.GetEnumerator();
				return (e.MoveNext())
					? e.Current
					: null;
			}

			public T[] ToArray()
			{
				var array = new T[4];
				var len = ToArrayNonAlloc(ref array);
				if (array.Length != len)
				{
					Array.Resize(ref array, len);
				}
				return array;
			}

			/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
			public int ToArrayNonAlloc(ref T[] array)
			{
				var index = 0;
				var e = this.GetEnumerator();
				while (e.MoveNext())
				{
					if (array.Length == index)
					{
						var newSize = (index == 0) ? 4 : index * 2;
						Array.Resize(ref array, newSize);
					}
					array[index++] = e.Current;
				}

				return index;
			}

			#endregion
		}

		public struct OfComponentEnumerator<T> : IEnumerator<T>
			where T : Component
		{
			Enumerator enumerator; // enumerator is mutable
			T current;

#if UNITY_EDITOR
			static List<T> componentCache = new List<T>(); // for no allocate on UNITY_EDITOR
#endif

			public OfComponentEnumerator(ref BeforeSelfEnumerable parent)
			{
				this.enumerator = parent.GetEnumerator();
				this.current = default(T);
			}

			public bool MoveNext()
			{
				while (enumerator.MoveNext())
				{
#if UNITY_EDITOR
					enumerator.Current.GetComponents<T>(componentCache);
					if (componentCache.Count != 0)
					{
						current = componentCache[0];
						componentCache.Clear();
						return true;
					}
#else
                        
                        var component = enumerator.Current.GetComponent<T>();
                        if (component != null)
                        {
                            current = component;
                            return true;
                        }
#endif
				}

				return false;
			}

			public T Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}
	}

	public struct AfterSelfEnumerable : IEnumerable<GameObject>
	{
		readonly GameObject origin;
		readonly bool withSelf;

		public AfterSelfEnumerable(GameObject origin, bool withSelf)
		{
			this.origin = origin;
			this.withSelf = withSelf;
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				e.Current.Destroy(useDestroyImmediate, false);
			}
		}

		/// <summary>Destroy every GameObject in the source collection safety(check null).</summary>
		/// <param name="useDestroyImmediate">If in EditMode, should be true or pass !Application.isPlaying.</param>
		public void Destroy(Func<GameObject, bool> predicate, bool useDestroyImmediate = false)
		{
			var e = GetEnumerator();
			while (e.MoveNext())
			{
				var item = e.Current;
				if (predicate(item))
				{
					item.Destroy(useDestroyImmediate, false);
				}
			}
		}

		public Enumerator GetEnumerator()
		{
			// check GameObject is destroyed only on GetEnumerator timing
			return (origin == null)
				? new Enumerator(null, withSelf, false)
				: new Enumerator(origin.transform, withSelf, true);
		}

		IEnumerator<GameObject> IEnumerable<GameObject>.GetEnumerator()
		{
			return GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#region LINQ

		public void ForEach(Action<GameObject> action)
		{
			var e = this.GetEnumerator();
			while (e.MoveNext())
			{
				action(e.Current);
			}
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(ref GameObject[] array)
		{
			var index = 0;

			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc(Func<GameObject, bool> filter, ref GameObject[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = item;
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				if (!filter(item)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(item);
			}

			return index;
		}

		/// <summary>Store element into the buffer, return number is size. array is automaticaly expanded.</summary>
		public int ToArrayNonAlloc<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector, ref T[] array)
		{
			var index = 0;
			var e = this.GetEnumerator(); // does not need to call Dispose.
			while (e.MoveNext())
			{
				var item = e.Current;
				var state = let(item);

				if (!filter(state)) continue;

				if (array.Length == index)
				{
					var newSize = (index == 0) ? 4 : index * 2;
					Array.Resize(ref array, newSize);
				}
				array[index++] = selector(state);
			}

			return index;
		}

		public GameObject[] ToArray()
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject[] ToArray(Func<GameObject, bool> filter)
		{
			var array = new GameObject[4];
			var len = ToArrayNonAlloc(filter, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc<T>(selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<T>(Func<GameObject, bool> filter, Func<GameObject, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public T[] ToArray<TState, T>(Func<GameObject, TState> let, Func<TState, bool> filter, Func<TState, T> selector)
		{
			var array = new T[4];
			var len = ToArrayNonAlloc(let, filter, selector, ref array);
			if (array.Length != len)
			{
				Array.Resize(ref array, len);
			}
			return array;
		}

		public GameObject First()
		{
			var e = this.GetEnumerator();
			if (e.MoveNext())
			{
				return e.Current;
			}
			else
			{
				throw new InvalidOperationException("sequence is empty.");
			}
		}

		public GameObject FirstOrDefault()
		{
			var e = this.GetEnumerator();
			return (e.MoveNext())
				? e.Current
				: null;
		}

		#endregion

		public struct Enumerator : IEnumerator<GameObject>
		{
			readonly int childCount; // childCount is fixed when GetEnumerator is called.
			readonly Transform originTransform;
			readonly bool canRun;

			bool withSelf;
			int currentIndex;
			GameObject current;
			Transform parent;

			internal Enumerator(Transform originTransform, bool withSelf, bool canRun)
			{
				this.originTransform = originTransform;
				this.withSelf = withSelf;
				this.currentIndex = (originTransform != null) ? originTransform.GetSiblingIndex() + 1 : 0;
				this.canRun = canRun;
				this.current = null;
				this.parent = originTransform.parent;
				this.childCount = (parent != null) ? parent.childCount : 0;
			}

			public bool MoveNext()
			{
				if (!canRun) return false;

				if (withSelf)
				{
					current = originTransform.gameObject;
					withSelf = false;
					return true;
				}

				if (currentIndex < childCount)
				{
					current = parent.GetChild(currentIndex).gameObject;
					currentIndex++;
					return true;
				}

				return false;
			}

			public GameObject Current { get { return current; } }
			object IEnumerator.Current { get { return current; } }
			public void Dispose() { }
			public void Reset() { throw new NotSupportedException(); }
		}
	}


	#endregion

	  /// <summary>
        /// Finde the closest gameobject of the current one based on it's tag
        /// </summary>
        /// <typeparam name="T">Type of object to find</typeparam>
        /// <param name="obj">Object wich is searching</param>
        /// <param name="tag">Tag of the searched object</param>
        /// <param name="maxDistance">Max distance that will be searched</param>
        /// <returns>Returns a single GameObject or null</returns>
        public static T FindNearestByTag<T>(this GameObject obj, string tag, float maxDistance = float.PositiveInfinity) where T : MonoBehaviour
        {
            var objects = GameObject.FindGameObjectsWithTag(tag);

            if (objects != null)
            {
                List<T> selectedObjects = new List<T>();
                foreach (var item in objects)
                {
                    if (item.TryGetComponent<T>(out var component))
                        selectedObjects.Add(component);
                }
                return FindNearests<T>(obj,selectedObjects, maxDistance);
            }
            return null;
        }

        /// <summary>
        /// Finde the closest gameobject of the current one based on it's Type
        /// </summary>
        /// <typeparam name="T">Type of object to find</typeparam>
        /// <param name="obj">Object wich is searching</param>
        /// <param name="maxDistance">Max distance that will be searched</param>
        /// <returns>Returns a single GameObject or null</returns>
        public static T FindNearestByType<T>(this GameObject obj,float maxDistance = float.PositiveInfinity) where T : MonoBehaviour
        {
            var objects = GameObject.FindObjectsOfType<T>();
            if (objects != null)
            {
                List<T> selectedObjects = new List<T>();
                foreach (var item in objects)
                {
                    if (item.TryGetComponent<T>(out var component))
                        selectedObjects.Add(component);
                }
                return FindNearests<T>(obj, selectedObjects, maxDistance);
            }
            return null;
        }

        /// <summary>
        /// Searchs on a list of GameObjects wich one it's closest to <see cref="obj"/>
        /// </summary>
        /// <typeparam name="T">Type of object to find</typeparam>
        /// <param name="obj">Object wich is searching</param>
        /// <param name="objects">Object list to be filtered</param>
        /// <param name="maxDistance">Max distance that will be searched</param>
        /// <returns>Returns the closests GameObject of obj or null</returns>
        public static T FindNearests<T>(this GameObject obj, List<T> objects, float maxDistance = float.PositiveInfinity) where T : MonoBehaviour
        {
            if (objects.Count == 0)
                return null;

            T nearestObject = null;
            foreach (T item in objects)
            {
                var dist = Vector3.Distance(obj.transform.position, item.transform.position);

                if (dist <= maxDistance)
                {
                    if (nearestObject == null)
                    {
                        nearestObject = item;
                    }
                    else
                    {
                        var dist2 = Vector3.Distance(obj.transform.position, nearestObject.transform.position);
                        nearestObject = dist < dist2 ? item : nearestObject;
                    }
                }
            }
            return nearestObject;
        }

        /// <summary>
        /// Finds a child insides the gameobject that match with the name parameter
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name">The name of the child you want to search</param>
        /// <returns>returns a game object or null</returns>
        public static GameObject GetChildByName(this GameObject obj,string name)
        {
            var childCount = obj.transform.childCount;
            for(int i = 0; i < childCount; i++)
            {
                var child = obj.transform.GetChild(i).gameObject;
                if(child.name == name) return child;
            }
            return null;
        }
}
