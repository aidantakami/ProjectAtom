#pragma warning disable 0649 // variable declared but not used.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdderToGameObjectRS : MonoBehaviour
{
    
    [SerializeField] private GameObjectRuntimeSet set;

    void Start() {
        set.Add(gameObject);
    }

}
