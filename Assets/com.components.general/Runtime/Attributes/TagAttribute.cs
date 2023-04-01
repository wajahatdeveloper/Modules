using System;
using UnityEngine;

namespace Quartzified.EditorAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    public class TagAttribute : PropertyAttribute
    {

    }
}