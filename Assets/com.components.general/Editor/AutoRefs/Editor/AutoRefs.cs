﻿using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEditor.Callbacks;

public class AutoRefs : Editor
{
	[MenuItem("Hub/Editor/Set AutoRefs")]
	static void GetAutoRefs()
	{
		// Get all game objects.
		GameObject[] acGameObjects = FindObjectsOfType<GameObject>();

		Undo.SetCurrentGroupName("Undo AutoRefs");
		int nUndoGroup = Undo.GetCurrentGroup();

		for (int nGameObject = 0; nGameObject < acGameObjects.Length; nGameObject++)
		{
			GameObject cGameObject = acGameObjects[nGameObject];

			// Get all MonoBehaviours attached to the current GameObject.
			MonoBehaviour[] acMonoBehaviours = cGameObject.GetComponents<MonoBehaviour>();

			// Register the MonoBehaviours with the Undo system.
			Undo.RecordObjects(acMonoBehaviours, "AutoRefs MonoBehaviours");

			for (int nMonoBehaviour = 0; nMonoBehaviour < acMonoBehaviours.Length; nMonoBehaviour++)
			{
				MonoBehaviour cMonoBehaviour = acMonoBehaviours[nMonoBehaviour];

				// Get the type of the MonoBehaviour.
				Type cType = cMonoBehaviour.GetType();

				// Get each of the fields.
				FieldInfo[] acFieldInfos = cType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

				for (int nFieldInfo = 0; nFieldInfo < acFieldInfos.Length; nFieldInfo++)
				{
					FieldInfo cFieldInfo = acFieldInfos[nFieldInfo];

					// Check that field for the AutoRef attribute.
					AutoRefAttribute cAutoRef = Attribute.GetCustomAttribute(cFieldInfo, typeof(AutoRefAttribute)) as AutoRefAttribute;

					// If it is marked to be set up as an automatic reference.
					if (cAutoRef != null)
					{
						// Get the targets to be searched.
						List<GameObject> lstAutoRefTargets = GetAutoRefTargets(cGameObject, cAutoRef.m_eTargetType, cAutoRef.m_astrGameObjectNames);

						if (lstAutoRefTargets.Count > 0)
						{
							// Get the field type.
							Type cFieldType = cFieldInfo.FieldType;

							// Get the component on the object which matches the required type.
							try
							{
								// Check to see if the field is an array.
								if (cFieldType.IsArray)
								{
									// Get the element type of the array (i.e. int[] the element type is int).
									Type cFieldElementType = cFieldType.GetElementType();

									List<Component> lstComponents = new List<Component>();

									for (int nTarget = 0; nTarget < lstAutoRefTargets.Count; nTarget++)
									{
										lstComponents.AddRange(lstAutoRefTargets[nTarget].GetComponents(cFieldElementType));
									}

									if (lstComponents.Count > 0)
									{
										// Find all components which match that element type (may be multiple components).
										Component[] acComponents = lstComponents.ToArray();

										// Create an array of the element type with the same length as the array of components which were found.
										// This is necessary to convert the Component[] type array into an array of the type specified in the FieldInfo.
										// i.e. The FieldInfo may be an array MyClass[]. You cannot set the value of a MyClass[] to a Component[].
										Array cArrayObject = Array.CreateInstance(cFieldElementType, acComponents.Length);

										// For each component which was found, copy it into the correctly typed array.
										for (int nComponent = 0; nComponent < acComponents.Length; nComponent++)
										{
											cArrayObject.SetValue(acComponents[nComponent], nComponent);
										}

										// Set the value of the FieldInfo to the newly created array.
										cFieldInfo.SetValue(cMonoBehaviour, cArrayObject);
									}
									else
									{
										LogErrorAutoRefFailedToFindReference(cFieldType, cMonoBehaviour, cFieldInfo);
									}
								}
								// Check to see if the field is a generic type.
								else if (cFieldType.IsGenericType)
								{
									// Check to see if the field is a list.
									if (cFieldType.GetGenericTypeDefinition() == typeof(List<>))
									{
										// Get the element type of the list (only 1 argument for a list, so always [0]).
										Type cFieldElementType = cFieldType.GetGenericArguments()[0];

										List<Component> lstComponents = new List<Component>();

										for (int nTarget = 0; nTarget < lstAutoRefTargets.Count; nTarget++)
										{
											lstComponents.AddRange(lstAutoRefTargets[nTarget].GetComponents(cFieldElementType));
										}

										if (lstComponents.Count > 0)
										{
											// Find all components which match that element type (may be multiple components).
											Component[] acComponents = lstComponents.ToArray();

											// Get the type of a list of the field element type.
											Type cListOfElementsType = typeof(List<>).MakeGenericType(cFieldElementType);

											// Create an IList of the correct type.
											IList cIList = Activator.CreateInstance(cListOfElementsType) as IList;

											// For each component which was found, copy it into the IList.
											for (int nComponent = 0; nComponent < acComponents.Length; nComponent++)
											{
												cIList.Add(acComponents[nComponent]);
											}

											// Set the value of the FieldInfo to the value of the IList, which fills the original list.
											cFieldInfo.SetValue(cMonoBehaviour, cIList);
										}
										else
										{
											LogErrorAutoRefFailedToFindReference(cFieldType, cMonoBehaviour, cFieldInfo);
										}
									}
								}
								else
								{
									// Find a component with a matching type to the field.
									Component cComponent = lstAutoRefTargets[0].GetComponent(cFieldType);

									// If that component exists.
									if (cComponent != null)
									{
										// Set the value of that field on the monobehaviour to match the found component.
										// This is where the reference is set.
										cFieldInfo.SetValue(cMonoBehaviour, cComponent);
									}
									else
									{
										LogErrorAutoRefFailedToFindReference(cFieldType, cMonoBehaviour, cFieldInfo);
									}
								}
							}
							catch (ArgumentException)
							{
								Debug.LogError("AutoRefs: An incompatible type has been annotated with the [AutoRef] attribute.\nType: [" + cFieldType.Name + "]	MonoBehaviour: [" + cMonoBehaviour.name + "]	Field: [" + cFieldInfo.Name + "]");
							}
						}
						else
						{
							Debug.LogError("AutoRefs: No targets were found for AutoRef. Please ensure the [AutoRef] attribute applied includes a valid target type.\nMonoBehaviour: [" + cMonoBehaviour.name + "]	Field: [" + cFieldInfo.Name + "]");
						}
					}
				}
			}
		}

		Undo.CollapseUndoOperations(nUndoGroup);
	}

	static void LogErrorAutoRefFailedToFindReference(Type cFieldType, MonoBehaviour cMonoBehaviour, FieldInfo cFieldInfo)
	{
		Debug.LogError("AutoRefs: AutoRef failed to find a matching reference. Please ensure the [AutoRef] attribute applied includes a valid target type.\nType: [" + cFieldType.Name + "]	MonoBehaviour: [" + cMonoBehaviour.name + "]	Field: [" + cFieldInfo.Name + "]");
	}

	static List<GameObject> GetAutoRefTargets(GameObject cGameObject, AutoRefTargetType eTargetType, string[] astrNamedGameObjects)
	{
		List<GameObject> lstAutoRefTargets = new List<GameObject>();

		// Add origin GameObject.
		if ((eTargetType & AutoRefTargetType.Self) != AutoRefTargetType.Undefined)
		{
			lstAutoRefTargets.Add(cGameObject);
		}

		// Add parent GameObject.
		if ((eTargetType & AutoRefTargetType.Parent) != AutoRefTargetType.Undefined)
		{
			lstAutoRefTargets.Add(cGameObject.transform.parent.gameObject);
		}

		// Add child GameObjects.
		if ((eTargetType & AutoRefTargetType.Children) != AutoRefTargetType.Undefined)
		{
			lstAutoRefTargets.AddRange(GetChildGameObjects(cGameObject));
		}

		// Add sibling GameObjects.
		if ((eTargetType & AutoRefTargetType.Siblings) != AutoRefTargetType.Undefined)
		{
			List<GameObject> lstSiblings = GetChildGameObjects(cGameObject.transform.parent.gameObject);

			for (int nSibling = 0; nSibling < lstSiblings.Count; nSibling++)
			{
				GameObject cSibling = lstSiblings[nSibling];

				if (cSibling != cGameObject)
				{
					lstAutoRefTargets.Add(cSibling);
				}
			}
		}

		// Add all GameObjects in scene.
		if ((eTargetType & AutoRefTargetType.Scene) != AutoRefTargetType.Undefined)
		{
			GameObject[] acSceneGameObjects = FindObjectsOfType<GameObject>();

			lstAutoRefTargets.AddRange(acSceneGameObjects);
		}

		// Add named GameObjects.
		if ((eTargetType & AutoRefTargetType.NamedGameObjects) != AutoRefTargetType.Undefined)
		{
			if (astrNamedGameObjects != null)
			{
				// For each GameObject name.
				for (int nNamedGameObject = 0; nNamedGameObject < astrNamedGameObjects.Length; nNamedGameObject++)
				{
					// Get the current name being checked.
					string strNamedGameObject = astrNamedGameObjects[nNamedGameObject];

					// Use scene roots to iterate and check each GameObject.
					GameObject[] acSceneRoots = EditorSceneManager.GetActiveScene().GetRootGameObjects();

					// For each scene root GameObject.
					for (int nSceneRoot = 0; nSceneRoot < acSceneRoots.Length; nSceneRoot++)
					{
						// Get the scene root GameObject.
						GameObject cSceneRoot = acSceneRoots[nSceneRoot];

						// If the name matches, add the root GameObject to the target list.
						if (cSceneRoot.name == strNamedGameObject)
						{
							lstAutoRefTargets.Add(cSceneRoot);
						}

						// Get the number of children of the scene root GameObject.
						int nChildCount = cSceneRoot.transform.childCount;

						// For each child GameObject.
						for (int nChild = 0; nChild < nChildCount; nChild++)
						{
							// Get the GameObject.
							GameObject cChild = cSceneRoot.transform.GetChild(nChild).gameObject;

							// If the name matches, add the child GameObject to the target list.
							if (cChild.name == strNamedGameObject)
							{
								lstAutoRefTargets.Add(cChild);
							}
						}
					}
				}
			}
			else
			{
				Debug.LogError("AutoRefs: The target type of NamedGameObjects requires the names of the GameObjects to be set in the AutoRefs attribute.");
			}
	}

		return lstAutoRefTargets;
	}

	static List<GameObject> GetChildGameObjects(GameObject cGameObject)
	{
		List<GameObject> lstChildGameObjects = new List<GameObject>();

		for (int nChildTransform = 0; nChildTransform < cGameObject.transform.childCount; nChildTransform++)
		{
			GameObject cChild = cGameObject.transform.GetChild(nChildTransform).gameObject;

			lstChildGameObjects.Add(cChild);
		}

		return lstChildGameObjects;
	}

	[PostProcessScene]
	static void OnPostProcessScene()
	{
		GetAutoRefs();
	}
}
