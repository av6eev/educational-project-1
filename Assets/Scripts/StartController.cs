using System;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using World;
using World.Block;
using World.Crop;
using World.Ground;
using World.Lake;
using World.Path;
using World.River;
using World.Systems.Crop;
using World.Systems.Path;
using World.Systems.River;
using World.Systems.Tree;
using World.Systems.Utilities;
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
        // _systemCollection.Add(SystemTypes.GenerateRiverSystem, new GenerateRiverSystem(_gameContext.BlockWorldModel, _gameContext, EndGeneration));
        _systemCollection.Add(SystemTypes.GenerateTreeSystem, new GenerateTreeSystem(_gameContext.BlockWorldModel, _gameContext, EndGeneration));
        _systemCollection.Add(SystemTypes.GenerateCropSystem, new GenerateCropSystem(_gameContext.BlockWorldModel, _gameContext, EndGeneration));

        new GroundGenerator().Generate(_gameContext);
        
        if (_locationData.HasTree)
        {
            new TreeGenerator().Generate(_gameContext);
            
        }
        
        if (_locationData.HasCrop) 
        {
            new CropGenerator().Generate(_gameContext);
            
        }

        if (_locationData.HasPath)
        {
            new PathGenerator().Generate(_gameContext);
        }

        if (_locationData.HasRiver)
        {
            // new RiverGenerator().Generate(_gameContext);
        }
        
        if (_locationData.HasLake)
        {
            new LakeGenerator().Generate(_gameContext);
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
        _systemCollection.Remove(SystemTypes.GenerateRiverSystem);
        _systemCollection.Remove(SystemTypes.GenerateTreeSystem);
        _systemCollection.Remove(SystemTypes.GenerateCropSystem);
        _controllerCollection.Activate();
    }
}