using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    private Character _owner;
    public Character Owner { get; set; }

    public virtual void Activate()
    {
        Debug.Log("Activated Skill");
        //Owner.is
    }
}
