using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public new string name;
    public string enemyName;
    public bool isAI;
    public float currentEnergy;
    private float startingEnergy = 5f;
    private float maxEnergy = 10f;
    private float energyFillSpeed = 1f;
    public Slider energySlider;
    public Text energyText;
    public List<Spell> deck;
    public Text card1;
    public Text card2;
    public Text card3;
    public Text card4;
    public Text nextCard;
    public Transform spawnZone;
    private float maxSpawnOffset = 3f;
    public GameObject troopPrefab;
    public Material troopMaterial;
    void Start () {
        currentEnergy = startingEnergy;
        /*
         * deck = new List<int>();
        for (var i = 1; i <= 8; i++) {
            deck.Add(i);
        }
        */
        deck.Shuffle();
        //Debug.Log(string.Join(",", deck.Select(x => x.ToString()).ToArray()));
    }

    // Update is called once per frame
    void Update () {
        currentEnergy = Mathf.Min(maxEnergy, currentEnergy + energyFillSpeed * Time.deltaTime);
	}
    private void LateUpdate() {
        energySlider.value = currentEnergy;
        energyText.text = Mathf.Floor(currentEnergy).ToString();
        card1.text = deck[0].ToString();
        card2.text = deck[1].ToString();
        card3.text = deck[2].ToString();
        card4.text = deck[3].ToString();
        nextCard.text = deck[4].ToString();
    }
    public void PlayCard(int index) {
        if (currentEnergy >= deck[index].cost) {
            Spell playedSpell = deck[index];
            deck.RemoveAt(index);
            currentEnergy -= playedSpell.cost;
            deck.Add(playedSpell);
            SpawnMonster(playedSpell);
        } else {
            Debug.Log("Not enough energy");
        }
    }
    public void SpawnMonster(Spell spell) {
        Vector3 spawnLocation = spawnZone.position;
        spawnLocation.x += Random.Range(-1 * maxSpawnOffset, maxSpawnOffset);
        GameObject newEnemy = Instantiate(troopPrefab, spawnLocation, Quaternion.identity);
        newEnemy.GetComponent<Renderer>().material = troopMaterial;
        newEnemy.GetComponent<NPC>().SetData(spell, name, enemyName);
    }
}
