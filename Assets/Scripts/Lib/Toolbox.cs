using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * A Toolbox pattern implementation to hold our global state
 * See http://wiki.unity3d.com/index.php/Toolbox
 */
public class Toolbox : Singleton<Toolbox> {
    protected Toolbox() {}
    
    public static T RegisterComponent<T>() where T : Component
    {
        return Instance.GetOrAddComponent<T>();
    }
}
