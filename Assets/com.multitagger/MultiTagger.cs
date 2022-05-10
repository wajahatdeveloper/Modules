using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

[HideMonoScript]
public class MultiTagger : MonoBehaviour
{
    public static MultiTaggerData TaggerData;

    #region UserInterface

    public static void AddTag(GameObject obj, MultiTags tagName)
    {
        var multiTag = obj.GetComponent<MultiTagger>(); 
        
        if (multiTag == null)
        {
            Debug.LogError("AddTag : MultiTagger Component Not Found on " + obj.name);
            return;
        }

        multiTag.tags.Add(tagName.ToString());
    }
    
    public static void RemoveTag(GameObject obj, MultiTags tagName)
    {
        var multiTag = obj.GetComponent<MultiTagger>(); 
        
        if (multiTag == null)
        {
            Debug.LogError("RemoveTag : MultiTagger Component Not Found on " + obj.name);
            return;
        }

        multiTag.tags.Remove(tagName.ToString());
    }
    
    public static bool HasTag( GameObject obj, MultiTags tagName )
    {
        var multiTag = obj.GetComponent<MultiTagger>(); 
        
        if (multiTag == null)
        {
            Debug.LogError("HasTag : MultiTagger Component Not Found on " + obj.name);
            return false;
        }

        return multiTag.tags.Contains(tagName.ToString());
    }

    public static List<GameObject> FindGameObjectsWithTag(MultiTags tagName)
    {
        if (!TaggerData.TagLinks.ContainsKey(tagName.ToString()))
        {
            Debug.LogError($"FindGameObjectsWithTag : Tag [{tagName}] Does not Exist");
            return null;
        }

        return TaggerData.TagLinks[tagName.ToString()].Select(x => x.gameObject).ToList();
    }

    #endregion
    
    #region HouseKeeping

    private void OnValidate()
    {
        TaggerData ??= Resources.Load<MultiTaggerData>("MultiTaggerData");
    }
    
    [ValueDropdown("@TaggerData.tags",IsUniqueList = true, FlattenTreeView = true,
        ExcludeExistingValuesInList = true, DrawDropdownForListElements = false)]
    [OnCollectionChanged(after: "After")]
    [DisplayAsString]
    public List<string> tags = new List<string>();

    public void After(CollectionChangeInfo info, object value)
    {
        Debug.Log(info + " Type:" + info.ChangeType);

        if (!TaggerData.TagLinks.ContainsKey(info.Value.ToString()))
        {
            Debug.LogError("Tag Does not exist");
            return;
        }
        
        switch (info.ChangeType)
        {
            case CollectionChangeType.Unspecified:
                break;
            case CollectionChangeType.Add:
                TaggerData.TagLinks[info.Value.ToString()].Add(this);
                break;
            case CollectionChangeType.Insert:
                break;
            case CollectionChangeType.RemoveValue:
                break;
            case CollectionChangeType.RemoveIndex:
                TaggerData.TagLinks[info.Value.ToString()].Remove(this);
                break;
            case CollectionChangeType.Clear:
                break;
            case CollectionChangeType.RemoveKey:
                break;
            case CollectionChangeType.SetKey:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void Start()
    {
        TaggerData.AddTagData(this);
    }

    private void OnDestroy()
    {
        TaggerData.RemoveTagData(this);
    }

    #endregion
}
