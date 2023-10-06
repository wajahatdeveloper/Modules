using UnityEngine;

public static class AnimatorExtensions
{
	/// <summary>
	/// 获取动画组件切换进度
	/// </summary>
	public static float GetCrossFadeProgress(this Animator @this, int layer = 0)
	{
		if (@this.GetNextAnimatorStateInfo(layer).shortNameHash == 0)
		{
			return 1;
		}
		return @this.GetCurrentAnimatorStateInfo(layer).normalizedTime % 1;
	}

	public static bool HasParameter(this Animator animator, string name)
	{
		var allParameters = animator.parameters;
		foreach (var param in allParameters)
		{
			if (param.name == name) return true;
		}

		return false;
	}

	public static bool HasParameter(this Animator animator, int nameHash)
	{
		var allParameters = animator.parameters;
		foreach (var param in allParameters)
		{
			if (param.nameHash == nameHash) return true;
		}

		return false;
	}

	public static bool IsInState(this Animator animator, string stateName) =>
		IsInState(animator, 0, stateName);

	public static bool IsInState(this Animator animator, int stateHash) => 
		IsInState(animator, 0, stateHash);

	public static bool IsInState(this Animator animator, int layerIndex, string stateName) =>
		animator.GetCurrentAnimatorStateInfo(layerIndex).IsName(stateName);

	public static bool IsInState(this Animator animator, int layerIndex, int stateHash) =>
		animator.GetCurrentAnimatorStateInfo(layerIndex).fullPathHash == stateHash;
}