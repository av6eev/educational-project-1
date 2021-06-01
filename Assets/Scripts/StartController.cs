using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World;
using World.Experimental;
using World.Experimental.Systems;
using World.Experimental.Systems.Tree;
using World.Path;
using World.Tree;

public class StartController : MonoBehaviour
{
    [SerializeField] private WorldView _worldView;
    [SerializeField] private GameContext _gameContext = new GameContext();
    [SerializeField] private LocationData _locationData;
    
    private SystemCollection _systemCollection = new SystemCollection();
    private ControllerCollection _controllerCollection = new ControllerCollection();

    void Start()
    {
        var blocksWorldModel = new BlockWorldModel();

        _gameContext.LocationData = _locationData;
        _gameContext.BlockWorldModel = blocksWorldModel;
        _gameContext.SystemCollection = _systemCollection;
        
        _systemCollection.Add(SystemTypes.GeneratePathSystem, new GeneratePathSystem(_gameContext.BlockWorldModel, _gameContext, EndGeneration));
        _systemCollection.Add(SystemTypes.GenerateTreeSystem, new GenerateTreeSystem(_gameContext.BlockWorldModel, _gameContext, EndGeneration));

        new GroundGenerator().Generate(_gameContext);
        new TreeGenerator().Generate(_gameContext);

        if (_locationData.HasGroundPath)
        {
            new PathGenerator().Generate(_gameContext);
        }
        
        _controllerCollection.Add(new WorldController(blocksWorldModel, _worldView, _gameContext));
    }

    private void Update()
    {
        _systemCollection.Update();
    }

    private void EndGeneration()
    {
        _systemCollection.Remove(SystemTypes.GeneratePathSystem);
        _systemCollection.Remove(SystemTypes.GenerateTreeSystem);
        _controllerCollection.Activate();
    }
}