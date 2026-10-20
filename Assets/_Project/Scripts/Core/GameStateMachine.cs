using UnityEngine;

public class GameStateMachine : MonoBehaviour
{
    private IGameState currentState;

    public IGameState CurrentState => currentState;

    public void ChangeState(IGameState nextState)
    {
        if (nextState == currentState)
        {
            return;
        }

        currentState?.Exit();
        currentState = nextState;
        currentState?.Enter();
    }

    private void Update()
    {
        currentState?.Tick();
    }

    private void OnDisable()
    {
        ChangeState(null);
    }
}
