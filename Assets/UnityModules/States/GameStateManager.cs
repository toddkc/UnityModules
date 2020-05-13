namespace Utility
{
    using System.Collections.Generic;
    using UnityEngine;

    public class GameStateManager : MonoBehaviour
    {
        public static GameStateManager instance;
        public GameState currentGameState { get; private set; }
        public BaseState currentState { get; private set; }
        private Dictionary<GameState, BaseState> stateDict = new Dictionary<GameState, BaseState>();

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            // create states here:
            //stateDict.Add(GameState.Running, new RunningState());
            
            //ChangeState(GameState.Running);
        }

        public static void ChangeState(GameState newState)
        {
            if (instance.currentState != null)
            {
                instance.currentState.ExitState();
            }
            BaseState nextstate;
            instance.stateDict.TryGetValue(newState, out nextstate);
            instance.currentState = nextstate;
            instance.currentGameState = newState;
            nextstate.EnterState();
        }
    }

    public enum GameState
    {
        Running
    }

    public abstract class BaseState
    {
        public abstract void EnterState();
        public abstract void ExitState();
    }

    
}