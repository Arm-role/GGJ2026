using System;

public static class GameStateManager
{
  public static GameState Current { get; private set; }

  public static event Action<GameState> OnStateChanged;

  public static void Set(GameState state)
  {
    if (Current == state) return;

    Current = state;
    OnStateChanged?.Invoke(state);

    UnityEngine.Debug.Log($"GameState → {state}");
  }
}