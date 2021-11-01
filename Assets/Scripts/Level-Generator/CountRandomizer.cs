using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ChildrenDestroier))]

public class CountRandomizer : MonoBehaviour
{
    [SerializeField] private int leave_count_min;
    [SerializeField] private int leave_count_max;

    void Start()
    {
        ChildrenDestroier killer = GetComponent<ChildrenDestroier>();
        killer.Initialize(Random.Range(leave_count_min, leave_count_max));
        killer.enabled = true;
    }

}
