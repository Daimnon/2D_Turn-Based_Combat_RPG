using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public virtual void Activate()
    {
        Debug.Log("Activated Skill");
    }
}
