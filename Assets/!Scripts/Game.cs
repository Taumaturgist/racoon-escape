using UniRx;
using UnityEngine;

public readonly struct OnGameStartMessage
{ }

public readonly struct OnGamePauseMessage
{ }

public readonly struct OnPlayerDefeatedMessage
{ }

public enum GameState
{
    Pause,
    Action,
    Defeat
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

        MessageBroker
            .Default
            .Receive<OnGamePauseMessage>()
            .Subscribe(message => _gameState = GameState.Pause);

        MessageBroker
            .Default
            .Receive<OnPlayerDefeatedMessage>()
            .Subscribe(message => _gameState = GameState.Defeat);
    }    
}
