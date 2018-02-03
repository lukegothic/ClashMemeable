using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "New Spell")]
public class Spell : ScriptableObject {
    public new string name;
    public float cost;
    public float speed;
    public float damage;
    public float hp;
    public float hitSpeed;
    public float range;
    public override string ToString() {
        return string.Format("[{0}] {1}", this.cost.ToString(), this.name);
    }
}
