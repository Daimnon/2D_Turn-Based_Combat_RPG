using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlot : MonoBehaviour
{
    [SerializeField] private int _slotNum;
    public int SlotNum { get => _slotNum; set => _ = value; }
}
