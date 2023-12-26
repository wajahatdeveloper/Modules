using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class LootTable<T,V> where T:LootContent<V>
{
	/// the list of objects that have a chance of being returned by the table
	[SerializeField]
	public List<T> ObjectsToLoot;

	/// the total amount of weights, for debug purposes only
	[Header("Debug")]
	[ReadOnly]
	public float WeightsTotal;

	protected float _maximumWeightSoFar = 0f;
	protected bool _weightsComputed = false;

	/// <summary>
	/// Determines, for each object in the table, its chance percentage, based on the specified weights
	/// </summary>
	public virtual void ComputeWeights()
	{
		if (ObjectsToLoot == null)
		{
			return;
		}

		if (ObjectsToLoot.Count == 0)
		{
			return;
		}

		_maximumWeightSoFar = 0f;

		foreach(T lootDropItem in ObjectsToLoot)
		{
			if(lootDropItem.Weight >= 0f)
			{
				lootDropItem.RangeFrom = _maximumWeightSoFar;
				_maximumWeightSoFar += lootDropItem.Weight;
				lootDropItem.RangeTo = _maximumWeightSoFar;
			}
			else
			{
				lootDropItem.Weight =  0f;
			}
		}

		WeightsTotal = _maximumWeightSoFar;

		foreach(T lootDropItem in ObjectsToLoot)
		{
			lootDropItem.ChancePercentage = ((lootDropItem.Weight) / WeightsTotal) * 100;
		}

		_weightsComputed = true;
	}

	/// <summary>
	/// Returns one object from the table, picked randomly
	/// </summary>
	/// <returns></returns>
	public virtual T GetLoot()
	{
		if (ObjectsToLoot == null)
		{
			return null;
		}

		if (ObjectsToLoot.Count == 0)
		{
			return null;
		}

		if (!_weightsComputed)
		{
			ComputeWeights();
		}

		float index = Random.Range(0, WeightsTotal);

		foreach (T lootDropItem in ObjectsToLoot)
		{
			if ((index > lootDropItem.RangeFrom) && (index < lootDropItem.RangeTo))
			{
				return lootDropItem;
			}
		}

		return null;
	}
}

/// <summary>
/// A MMLootTable implementation for GameObjects
/// </summary>
[System.Serializable]
public class LootTableGameObject : LootTable<LootContentGameObject, GameObject> { }

/// <summary>
/// A MMLootTable implementation for floats
/// </summary>
[System.Serializable]
public class LootTableFloat : LootTable<LootContentFloat, float> { }

/// <summary>
/// A MMLootTable implementation for strings
/// </summary>
[System.Serializable]
public class LootTableString : LootTable<LootContentString, string> { }