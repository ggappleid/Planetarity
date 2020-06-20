using Entitas;
using Entitas.Features.Game.Gameplay;
using Entitas.Features.Game.GameplayUi;
using Entitas.Features.GameState;
using Entitas.Features.GameState.SaveLoadGameFeature;
using Entitas.Features.Input;
using UnityEngine;

public class GameStarter : MonoBehaviour
{
    private Systems _systems;
    private Contexts _contexts;

    void Awake()
    {
        _contexts = Contexts.sharedInstance;
    }
    
    void Start()
    {
        _systems = CreateSystems(_contexts);
        _systems.Initialize();
    }

    void Update()
    {
        _systems.Execute();
        _systems.Cleanup();
    }
    
    void OnDestroy()
    { 
        _systems.TearDown();
        
        _systems.DeactivateReactiveSystems();
        _systems.ClearReactiveSystems();

        _contexts.Reset();
    }
    
    Systems CreateSystems(Contexts contexts)
    {
        var defaultDataSaver = new PlayerPrefsDataSaver("GameData");
        var defaultDataLoader = new PlayerPrefsDataLoader<GameBoardState>("GameData");
        
        var gameplayFeature = new GameplayFeature(contexts);
        
        var root = new Feature("Systems")
            .Add(new GameStateFeature(contexts, gameplayFeature))
            .Add(new GameplayUiFeature(contexts))
            .Add(gameplayFeature)
            .Add(new SaveLoadGameFeature(contexts, defaultDataSaver, defaultDataLoader))
            .Add(new InputFeature(contexts));
        
        return root;
    }
}
