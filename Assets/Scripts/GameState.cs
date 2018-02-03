using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour {
    enum GameStates {
        MENU,
        PLAY,
        END
    }
    private GameStates gameState = GameStates.PLAY;
    public GameObject player1;
    private Attackable player1alive;
    public GameObject player2;
    private Attackable player2alive;
    // Use this for initialization
    void Start () {
        player1alive = player1.GetComponent<Attackable>();
        player2alive = player2.GetComponent<Attackable>();
    }
	// Update is called once per frame
	void Update () {
        if (gameState == GameStates.PLAY) {
            if (player2alive.Dead()) {
                EndGame("YOU");
            } else if (player1alive.Dead()) {
                EndGame("AI");
            }
        }
	}
    void EndGame(string winner) {
        gameState = GameStates.END;
        foreach (var attackable in FindObjectsOfType<Attackable>()) {
            Destroy(attackable.gameObject);
        }
        Debug.Log(winner + " WIN");
    }
}
