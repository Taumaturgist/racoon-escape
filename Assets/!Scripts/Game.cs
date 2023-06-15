using UniRx;
using UnityEngine;

public readonly struct OnGameStartMessage
{ }

public enum GameState
{
    Pause,
    Action
}
public class Game : MonoBehaviour
{
    private GameState _gameState = GameState.Pause;
    
    public GameState GetGameState()
    {
        return _gameState;
    }
    private void Awake()
    {
        MessageBroker
            .Default
            .Receive<OnGameStartMessage>()
            .Subscribe(message => _gameState = GameState.Action);
    }    
}
