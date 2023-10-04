	using System;
	using UnityEngine;

	/// <summary>
	/// Defines that a MonoBehaviour's script execution should happen after other scripts.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class ExecuteAfterAttribute : Attribute
	{
		private readonly Type[] executeAfter = null;

		public Type[] ExecuteAfter
		{
			get => executeAfter;
		}

		public ExecuteAfterAttribute(params Type[] executeAfter)
		{
			if (executeAfter is null)
			{
				throw new ArgumentNullException(nameof(executeAfter));
			}

			foreach (Type type in executeAfter)
			{
				if (type is null)
				{
					throw new ArgumentNullException(nameof(type));
				}

				if (!typeof(MonoBehaviour).IsAssignableFrom(type))
				{
					throw new Exception(string.Format("The type '{0}' is not assignable from a {1}.", type.Name, typeof(MonoBehaviour).Name));
				}
			}

			this.executeAfter = executeAfter;
		}
	}