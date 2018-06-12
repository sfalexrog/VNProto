using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * MonoBehaviour extension for adding/getting components
 */
public static class MethodExtensionForMonoBehaviourTransform
{
    public static T GetOrAddComponent<T>(this Component child) where T : Component
    {
        T result = child.GetComponent<T>();
        if (result == null)
        {
            result = child.gameObject.AddComponent<T>();
        }

        return result;
    }
}