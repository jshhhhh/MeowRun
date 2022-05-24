using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectTags : MonoBehaviour
{
    string[] tagLists = UnityEditorInternal.InternalEditorUtility.tags;

    public bool isTagExisting(string tagName) 
    {
        if (tagLists.Length != 0) {
            for (int i=0; i<tagLists.Length; i++) 
            {
                if (tagLists[i] == tagName) return true;
            }
        }
        return false;
    }

    public void LogAllTags() 
    {
        if (tagLists.Length != 0) {
            for (int i=0; i<tagLists.Length; i++) { print(tagLists[i]); }
        }
    }
}
