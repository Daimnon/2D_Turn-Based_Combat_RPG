using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour, ISkill
{
    [SerializeField] private string _name;
    public string Name { get => _name; set => _ = value; }

    private Character _invokerC;
    public Character InvokerC { get => _invokerC; set => _ = value; }

    public virtual void Activate()
    {
        Debug.Log($"{_invokerC.Data.Name} Activated {_name}");
    }
    public override string ToString()
    {
        if (Name != null)
            return Name;
        else
            return $"Uninitialized skill";
    }
}
