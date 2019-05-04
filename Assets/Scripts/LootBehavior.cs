using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBehavior : MonoBehaviour {

    [SerializeField]
    private int weight;
    [SerializeField]
    private int value;

    public int Weight
    {
        get
        {
            return weight;
        }
    }

    public int Value
    {
        get
        {
            return value;
        }
    }
    // Enum là kiểu số nhưng được đặt tên

}
