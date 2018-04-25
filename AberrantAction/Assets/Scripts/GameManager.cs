using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text healthBar;
    private PlayerStats playerStats;

    void Start () {
        playerStats = player.GetComponent<PlayerStats>();
        healthBar.text = playerStats.GetCurrentHealth().ToString();
        InvokeRepeating("UpdateHPBar", 0.1f, 0.1f);
    }
	
	
	void Update () {
		if(Input.GetKeyDown(KeyCode.R))
        {
            DealDamageToPlayer(10);
        }
	}

    private void DealDamageToPlayer(int damage)
    {
        playerStats.TakeDamage(damage);

        UpdateHPBar();
    }

    public void UpdateHPBar()
    {
        healthBar.text = playerStats.GetCurrentHealth().ToString();
    }
}
