using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoveOnClick : MonoBehaviour {
	float speed = 3.5f;
	NavMeshAgent agent;
	public Vector3 destiny;
	private int waterMask;
	void Start () {
		agent = GetComponent<NavMeshAgent>();
		waterMask = 1 << NavMesh.GetAreaFromName("Water");
	}
	void Update() {
		if (Input.GetMouseButtonDown(0)) {
			RaycastHit hit;
			if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity)) {
				if (hit.collider.CompareTag ("Terrain")) {
					agent.destination = hit.point;
				} else if (hit.collider.CompareTag ("Enemy")) {
					
				}
			}
		}
		NavMeshHit meshHit;
		if (!agent.SamplePathPosition(NavMesh.AllAreas, 1.0F, out meshHit)) {
			//agent.speed = speed / NavMesh.GetAreaCost (meshHit.mask);
			Debug.Log(meshHit.mask);
			if ((meshHit.mask & waterMask) != 0) {
				agent.speed = speed / 10f;
			} else {
				agent.speed = speed;
			}
		}
	}
}