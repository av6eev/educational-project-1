using UnityEngine;
using Utilities;
using World;

public class StartController : MonoBehaviour
{
    [SerializeField] private WorldView _worldView;
    [SerializeField] private GameContext _gameContext = new GameContext();
    [SerializeField] private LocationData locationData;

    private ControllerCollection _controllerCollection = new ControllerCollection();

    void Start()
    {
        var worldModel = new WorldModel(locationData);

        _gameContext.LocationData = locationData;
        _gameContext.WorldModel = worldModel;

        _controllerCollection.Add(new WorldController(worldModel, _worldView, _gameContext));

        new WorldGroundGenerator().Generate(worldModel);
        _controllerCollection.Activate();

        if (locationData.HasGroundPath)
        {
            new WorldPathGenerator().Generate(worldModel);
        }
    }
}