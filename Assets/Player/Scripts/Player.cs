using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A singleton class for getting access to player-related scripts
/// </summary>
public class Player : MonoBehaviour
{
    public static Player Instance;

    public Transform Base;
    public bool Moving = true;
    [Space]
    public LegController LegController;
    public TorsoController TorsoController;
    public SwordController SwordController;
    public SwordThrowingController SwordThrowingController;

    void Awake()
    {
        Instance = this;
        
        //Make sure the player object persists between scenes (this object is not the parent)
        DontDestroyOnLoad(transform.parent.gameObject);
    }
}