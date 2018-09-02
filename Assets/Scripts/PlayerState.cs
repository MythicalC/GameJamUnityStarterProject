using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState  {
   
//singleton
    private static PlayerState _instance;

    public static PlayerState Instance
    {
        get
        { 
            if (_instance == null)
            {
                _instance = new PlayerState();
            }

            return _instance;
        }
    }

    //playerstate
    public delegate void UpdatePlayerScore(int newScore);
    public UpdatePlayerScore OnScoreUpdate;

    private int _playerScore = 0;

    public void IncrementPlayerScore(int amount)
    {
        //this is the same as _playerScore = _playerScore + amount
        _playerScore += amount;

        if(OnScoreUpdate != null)
        {
            OnScoreUpdate(_playerScore);
        }
    }

    public void DecrementPlayerScore(int amount)
    {
        //this is the same as _playerScore = _playerScore - amount
        _playerScore -= amount;

        if(OnScoreUpdate != null)
        {
            OnScoreUpdate(_playerScore);
        }

    }

}
