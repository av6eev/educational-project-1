using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World;
using World.Experimental;
using World.Experimental.Systems;
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
        var blocksWorldModel = new BlocksWorldModel();

        _gameContext.LocationData = _locationData;
        _gameContext.BlocksWorldModel = blocksWorldModel;
        _gameContext.SystemCollection = _systemCollection;
        
        _systemCollection.Add(SystemTypes.GeneratePathSystem, new GeneratePathSystem(_gameContext.BlocksWorldModel, _gameContext, EndGeneration));

        new WorldGroundGenerator().Generate(_gameContext);
        // new WorldTreeGenerator().Generate(worldModel);

        if (_locationData.HasGroundPath)
        {
            new WorldPathGenerator().Generate(_gameContext);
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
        _controllerCollection.Activate();
    }
}