using System;

public static partial class Extensions
{
    /// <summary>
		/// Invokes the action if it is not null.
		/// </summary>
		public static void InvokeIfNotNull(this Action del)
		{
			if (del != null)
			{
				del.Invoke();
			}
		}

		/// <summary>
		/// Invokes the action if it is not null.
		/// </summary>
		public static void InvokeIfNotNull<T1>(this Action<T1> del, T1 p1)
		{
			if (del != null)
			{
				del.Invoke(p1);
			}
		}

		/// <summary>
		/// Invokes the action if it is not null.
		/// </summary>
		public static void InvokeIfNotNull<T1, T2>(this Action<T1, T2> del, T1 p1, T2 p2)
		{
			if (del != null)
			{
				del.Invoke(p1, p2);
			}
		}

		/// <summary>
		/// Invokes the action if it is not null.
		/// </summary>
		public static void InvokeIfNotNull<T1, T2, T3>(this Action<T1, T2, T3> del, T1 p1, T2 p2, T3 p3)
		{
			if (del != null)
			{
				del.Invoke(p1, p2, p3);
			}
		}

		/// <summary>
		/// Invokes the action if it is not null.
		/// </summary>
		public static void InvokeIfNotNull<T1, T2, T3, T4>(this Action<T1, T2, T3, T4> del, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			if (del != null)
			{
				del.Invoke(p1, p2, p3, p4);
			}
		}

		/// <summary>
		/// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
		/// </summary>
		public static TResult InvokeIfNotNull<TResult>(this Func<TResult> func)
		{
			return (func != null) ? func() : default(TResult);
		}

		/// <summary>
		/// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
		/// </summary>
		public static TResult InvokeIfNotNull<T1, TResult>(this Func<T1, TResult> func, T1 p1)
		{
			return (func != null) ? func(p1) : default(TResult);
		}

		/// <summary>
		/// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
		/// </summary>
		public static TResult InvokeIfNotNull<T1, T2, TResult>(this Func<T1, T2, TResult> func, T1 p1, T2 p2)
		{
			return (func != null) ? func(p1, p2) : default(TResult);
		}

		/// <summary>
		/// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
		/// </summary>
		public static TResult InvokeIfNotNull<T1, T2, T3, TResult>(this Func<T1, T2, T3, TResult> func, T1 p1, T2 p2, T3 p3)
		{
			return (func != null) ? func(p1, p2, p3) : default(TResult);
		}

		/// <summary>
		/// Invokes the function if not null and returns the result. If the function is null, then the default value for type of TResult is returned.
		/// </summary>
		public static TResult InvokeIfNotNull<T1, T2, T3, T4, TResult>(this Func<T1, T2, T3, T4, TResult> func, T1 p1, T2 p2, T3 p3, T4 p4)
		{
			return (func != null) ? func(p1, p2, p3, p4) : default(TResult);
		}
    
    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe(this Action action)
    {
        if (action != null) action.Invoke();
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T>(this Action<T> action, T param)
    {
        if (action != null) action.Invoke(param);
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T1, T2>(this Action<T1, T2> action, T1 param1, T2 param2)
    {
        if (action != null) action.Invoke(param1, param2);
    }

    /// <summary>
    /// Invokes the <paramref name="action"/> if it is not null.
    /// </summary>
    public static void InvokeSafe<T1, T2, T3>(this Action<T1, T2, T3> action, T1 param1, T2 param2, T3 param3)
    {
        if (action != null) action.Invoke(param1, param2, param3);
    }
}
