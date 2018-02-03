using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAI : MonoBehaviour {
    Player player;
    private int AIDecidedCard = -1;
    // Use this for initialization
    void Start () {
        player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        // jugar cartas automaticamente y etc;
        if (AIDecidedCard == -1) {
            AIDecidedCard = Random.Range(0, 3);
        }
        if (player.currentEnergy >= player.deck[AIDecidedCard].cost) {
            player.PlayCard(AIDecidedCard);
            AIDecidedCard = -1;
        }
    }
}
