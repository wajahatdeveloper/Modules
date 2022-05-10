using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MultiTaggerExtensions
{
    public static bool HasMultiTag(this GameObject gameObject, MultiTags tagName)
    {
        return MultiTagger.HasTag(gameObject, tagName);
    }
}
