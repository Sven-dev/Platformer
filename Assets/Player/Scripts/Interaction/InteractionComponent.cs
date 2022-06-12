using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractionType
{
    Dialogue,
    SpriteChange
}

public abstract class InteractionComponent : MonoBehaviour
{
    public InteractionType Type;
    [HideInInspector] public bool Active = false;
}