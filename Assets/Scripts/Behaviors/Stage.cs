using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    [SerializeField] protected List<Enemy> _potentialEnemies;
    public List<Enemy> PotenrialEnemies { get => _potentialEnemies; set => _ = value; }
}
