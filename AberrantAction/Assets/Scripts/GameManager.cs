using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject boss;
    [SerializeField]
    private Text playerHealthBar;
    [SerializeField]
    private Text bossHealthBar;
    private PlayerStats playerStats;
    private BossController bossController;

    void Start () {
        playerStats = player.GetComponent<PlayerStats>();
        bossController = boss.GetComponent<BossController>();
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
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
        playerHealthBar.text = playerStats.GetCurrentHealth().ToString();
        bossHealthBar.text = bossController.GetCurrentHealth().ToString();
    }
}
