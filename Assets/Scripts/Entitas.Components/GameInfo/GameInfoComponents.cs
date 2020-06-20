using Entitas;
using Entitas.CodeGeneration.Attributes;

public enum GameState
{
    Menu,
    Gameplay
}

[GameInfo, Unique]
public class CurrentStateComponent : IComponent
{
    public GameState Value;
}

public enum GameComplexityType
{
    Random = 0,
    Easy,
    Normal,
    Hard
}

[GameInfo, Unique]
public class PreferredComplexityComponent : IComponent
{
    public GameComplexityType GameComplexity;
}

[GameInfo, Unique]
public class GameStartComponent : IComponent
{
    public bool IsNew;
    public GameComplexityType GameComplexity;
}

[GameInfo, Unique]
public class GameEndedComponent : IComponent
{
    public bool Win;
}

[GameInfo, Unique]
public class SaveGameComponent : IComponent
{
}

[GameInfo, Unique]
public class LoadGameComponent : IComponent
{
}
