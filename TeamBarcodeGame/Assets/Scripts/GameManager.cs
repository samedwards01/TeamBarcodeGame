using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    public int exp;
    public int playerHealth;
    public int maxPlayerHealth;
    public int magic;
    public int melee;
    public int healing;
    public Vector3 playerPosition;
    public int currentEncounter = 0;

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    public void UpdatePlayerHealth(int newHealth)
    {
        playerHealth = newHealth;
    }

    public void UpdateMaxPlayerHealth(int newMaximumPlayerHealth)
    {
        maxPlayerHealth = newMaximumPlayerHealth;
    }

    public void UpdateItems(int newExp)
    {
        exp = newExp;
    }


    public int GetMaxPlayerHealth()
    {
        return maxPlayerHealth;
    }

    public void StorePlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }


}