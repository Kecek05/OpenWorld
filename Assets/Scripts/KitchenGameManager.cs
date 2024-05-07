using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenGameManager : MonoBehaviour
{
    
    public static KitchenGameManager Instance { get; private set; }
    private enum State
    {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
    }

    private State state;

    private float waitingToStartTimer = .2f;

    private float countdownToStartTimer = .5f;

 //   private float gamePlayingTimer = 10f;

    private void Awake()
    {
        Instance = this;
        state = State.WaitingToStart;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                waitingToStartTimer -= Time.deltaTime;
                if(waitingToStartTimer < 0f)
                {
                    state = State.CountdownToStart;
                }
                break;
            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                }
                break;
            case State.GamePlaying:
                break;
       

        }
       // Debug.Log(state);

    }


    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }
}
