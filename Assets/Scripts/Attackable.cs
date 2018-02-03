using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackable : MonoBehaviour {
    public float hp;
    public float attackDistanceThreshold = 1f;
    public void GetHit(float damage) {
        hp -= damage;
        if (Dead()) {
            Destroy(gameObject);
        }
    }
    public bool Dead() {
        return hp <= 0f;
    }
}
