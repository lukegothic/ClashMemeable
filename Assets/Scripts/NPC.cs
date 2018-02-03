using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour {
    Spell data;
    float lastAttackTime = 0f;
    string enemyTag;
    NavMeshAgent agent;
    public Attackable target;
    // Use this for initialization
    void Start () {
        GetComponent<Attackable>().hp = data.hp;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = data.speed / 2;
        agent.stoppingDistance = data.range;
        transform.localScale = transform.localScale * data.cost;
    }
	// Update is called once per frame
	void Update () {
        if (target == null) {
            AdquireTarget();
        }
        if (target != null) {
            agent.SetDestination(target.transform.position);
            agent.isStopped = !agent.pathPending && (agent.remainingDistance - target.attackDistanceThreshold) <= agent.stoppingDistance;
            Debug.DrawLine(transform.position, target.transform.position);
            if (agent.isStopped && (lastAttackTime == 0f || Time.time - lastAttackTime >= data.hitSpeed)) {
                target.GetHit(data.damage);
                if (target.Dead()) {
                    target = null;
                }
                lastAttackTime = Time.time;
            }
        }
    }
    public void SetData(Spell spellData, string controllerName, string enemyName) {
        data = spellData;
        this.tag = controllerName;
        this.enemyTag = enemyName;
    }
    void AdquireTarget() {
        List<Attackable> targets = FindObjectsOfType<Attackable>().Where(x => x.CompareTag(enemyTag)).ToList();
        if (targets.Count > 0) {
            float minDistance = 99999999f;
            float distance;
            foreach (var pTarget in targets) {
                distance = Vector3.Distance(transform.position, pTarget.gameObject.transform.position);
                if (distance < minDistance) {
                    minDistance = distance;
                    target = pTarget;
                }
            }
        }
    }
}
