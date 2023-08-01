using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "MultiTaggerData", menuName = "MultiTagger/MultiTaggerData", order = 1)]
public class MultiTaggerData : SerializedScriptableObject 
{
    [ListDrawerSettings(AlwaysAddDefaultValue = false, HideAddButton = true, HideRemoveButton = true)]
    [Searchable]
    public List<string> tags = new List<string>();
    
    [HideLabel]
    [PropertyOrder(1)]
    public string tagName;

    [ButtonGroup("TagsModify"), PropertyOrder(1)]
    public void AddTag()
    {
        if (tagName == "") { return; }

        if (tags.Contains(tagName))
        {
            tagName = "";
            Debug.Log("Tag Already Exists");
            return;
        }
        
        tags.Add(tagName);
        
        UpdateTagLinks();
        tagName = "";
    }
    
    [ButtonGroup("TagsModify"), PropertyOrder(1)]
    public void RemoveTag()
    {
        if (tagName == "") { return; }

        if (!tags.Contains(tagName))
        {
            Debug.Log("Tag Does Not Exist");
            return;
        }

        tags.Remove(tagName);
        
        UpdateTagLinks();
        tagName = "";
    }

    [ContextMenu(nameof(UpdateTagLinks))]
    public void UpdateTagLinks()
    {
        foreach (var tag in tags)
        {
            if (!TagLinks.ContainsKey(tag))
            {
                TagLinks.TryAdd(tag, new List<MultiTagger>());
            }
        }

        string keyToRemove = "";
        foreach (var (key, value) in TagLinks)
        {
            if (!tags.Contains(key))
            {
                keyToRemove = key;
                break;
            }
        }

        if (keyToRemove != "")
        {
            TagLinks.Remove(keyToRemove);
        }

        #if UNITY_EDITOR
        GenerateEnum.Go("MultiTags", tags.ToArray());
        #endif
    }
    
    [PropertySpace(SpaceBefore = 10), PropertyOrder(2)]
    public Dictionary<string, List<MultiTagger>> TagLinks = new Dictionary<string, List<MultiTagger>>();

    public void AddTagData(MultiTagger tagger)
    {
        if (tagger == null)
        {
            Debug.LogError("AddTagData : MultiTagger Component Not Found");
            return;
        }

        foreach (var tag in tagger.tags)
        {
            foreach (var (key, value) in TagLinks)
            {
                if (tag == key)
                {
                    value.Add(tagger);
                }
            }
        }
    }
    
    public void RemoveTagData(MultiTagger tagger)
    {
        if (tagger == null)
        {
            Debug.LogError("AddTagData : MultiTagger Component Not Found");
            return;
        }

        foreach (var tag in tagger.tags)
        {
            foreach (var (key, value) in TagLinks)
            {
                if (tag == key)
                {
                    value.Remove(tagger);
                }
            }
        }
    }
}
