using Sirenix.OdinInspector;
using UnityEngine;

/// <summary>
/// A class defining the contents of a MMLootTable
/// </summary>
/// <typeparam name="T"></typeparam>
public class LootContent<T>
{
	/// the object to return
	public T Loot;
	/// the weight attributed to this specific object in the table
	public float Weight = 1f;
	/// the chance percentage to display for this object to be looted. ChancePercentages are meant to be computed by the MMLootTable class
	[ReadOnly]
	public float ChancePercentage;

	/// the computed low bound of this object's range
	public float RangeFrom { get; set; }
	/// the computed high bound of this object's range
	public float RangeTo { get; set; }
}


/// <summary>
/// a MMLoot implementation for gameobjects
/// </summary>
[System.Serializable]
public class LootContentGameObject : LootContent<GameObject> { }

/// <summary>
/// a MMLoot implementation for strings
/// </summary>
[System.Serializable]
public class LootContentString : LootContent<string> { }

/// <summary>
/// a MMLoot implementation for floats
/// </summary>
[System.Serializable]
public class LootContentFloat : LootContent<float> { }