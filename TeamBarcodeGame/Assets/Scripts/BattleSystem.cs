using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class BattleSystem : MonoBehaviour
{

    public GameObject player1Prefab;
    public Transform playerSpawn;
    public Transform enemySpawn;
    public List<GameObject> enemyPrefabsByEncounter;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {

        state = BattleState.START;
        int playerHealth = GameManager.Instance.playerHealth;
        int maxPlayerHealth = GameManager.Instance.GetMaxPlayerHealth();
        StartCoroutine(Fight(playerHealth, maxPlayerHealth));

    }

    IEnumerator Fight(int playerHealth, int maxPlayerHealth)
    {
        GameObject playerGO = Instantiate(player1Prefab, playerSpawn);
        playerUnit = playerGO.GetComponent<Unit>();

        playerUnit.currentHP = playerHealth;

        GameObject enemyPrefab = enemyPrefabsByEncounter[GameManager.Instance.currentEncounter];
        GameObject enemyGO = Instantiate(enemyPrefab, enemySpawn);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = "Encountered " + enemyUnit.unitName + "!!!";

        playerHUD.SetHUD(playerUnit, maxPlayerHealth);
        enemyHUD.SetHUD(enemyUnit, enemyUnit.maxHP);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    void PlayerTurn()
    {
        dialogueText.text = "Choose a Command!";
    }

    public void OnSpellButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerMagic());
    }

    public void OnCureButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerCure());
    }

    public void OnRetreatButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerRetreat());
    }

    public void OnMeleeButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerMelee());
    }

    void BattleEnd()
    {
            StartCoroutine(EndBattle()); 
    }

    IEnumerator PlayerMagic()
    {
        bool magicSuccess = UnityEngine.Random.Range(0f, 1f) < 0.95f;
        if (magicSuccess)
        {
            bool isDead = enemyUnit.TakeDamage(GameManager.Instance.magic);

            yield return new WaitForSeconds(2f);
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = enemyUnit.unitName + " was hit for " + GameManager.Instance.magic + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                BattleEnd();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Missed!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator PlayerCure()
    {

        playerUnit.Cure(GameManager.Instance.healing);

        playerHUD.SetHP(playerUnit.currentHP);
        dialogueText.text = "Consumed a potion";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
        
    }

    IEnumerator PlayerRetreat()
    {
        bool retreatSuccessful = UnityEngine.Random.Range(0f, 1f) < 0.5f;
        dialogueText.text = "Attempting to retreat...";
        yield return new WaitForSeconds(3f);
        
        if (retreatSuccessful)
        {
            dialogueText.text = "Retreating...";
            yield return new WaitForSeconds(2f);
            SceneManager.LoadScene("Dungeon");
        }
        else
        {
            dialogueText.text = "Failed to retreat";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }    
    }

    IEnumerator PlayerMelee()
    {
        bool meleeSuccess = UnityEngine.Random.Range(0f, 1f) < 0.95f;
        if (meleeSuccess)
        {
            bool isDead = enemyUnit.TakeDamage(GameManager.Instance.melee);

            yield return new WaitForSeconds(2f);
            enemyHUD.SetHP(enemyUnit.currentHP);
            dialogueText.text = enemyUnit.unitName + " was hit for " + GameManager.Instance.melee + " damage!";

            yield return new WaitForSeconds(2f);

            if (isDead)
            {
                state = BattleState.WON;
                BattleEnd();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
        else
        {
            dialogueText.text = "Missed!";
            yield return new WaitForSeconds(2f);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

    }

    IEnumerator EndBattle()
    {
        if(state == BattleState.WON)
        {  
                dialogueText.text = enemyUnit.unitName + " defeated!";
                yield return new WaitForSeconds(2f);
                dialogueText.text = "Obtained " + enemyUnit.exp + " XP!";
                GameManager.Instance.UpdateItems(GameManager.Instance.exp + enemyUnit.exp);
                Destroy(enemyUnit.gameObject);
                yield return new WaitForSeconds(2f);
                SceneManager.LoadScene(1);    
        }
        else if(state == BattleState.LOST)
        {
            dialogueText.text = "Game Over...";
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(0);
        }
    }

    IEnumerator EnemyTurn()
    {
        bool attackSuccess = UnityEngine.Random.Range(0f, 1f) < 0.75f;
        dialogueText.text = enemyUnit.unitName + " is attacking...";
        yield return new WaitForSeconds(2f);

        if (attackSuccess)
        {
            bool isDead = playerUnit.TakeDamage(enemyUnit.melDamage);
            dialogueText.text = enemyUnit.unitName + " attacks!";
            yield return new WaitForSeconds(2f);
            playerHUD.SetHP(playerUnit.currentHP);
            GameManager.Instance.UpdatePlayerHealth(playerUnit.currentHP);
            dialogueText.text = playerUnit.unitName + " was hit for " + enemyUnit.melDamage + " damage!";
            yield return new WaitForSeconds(2f);
            if (isDead)
            {
                state = BattleState.LOST;
                BattleEnd();
            }
            else
            {
                state = BattleState.PLAYERTURN;
                PlayerTurn();
            }
        }
        else
        {
            dialogueText.text = enemyUnit.unitName + " missed!";
            yield return new WaitForSeconds(2f);
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

        yield return new WaitForSeconds(1f);

    }
 
}
