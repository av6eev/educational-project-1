using System;
using System.Collections.Generic;
using Utilities;
using World.Cell;
using Random = UnityEngine.Random;

namespace World
{
    public class WorldPathGenerator : IWorldGenerator
    {
        private readonly List<PathWorldElementModel> _active = new List<PathWorldElementModel>();
        private readonly List<PathWorldElementModel> _remove = new List<PathWorldElementModel>();
        private readonly List<PathWorldElementModel> _add = new List<PathWorldElementModel>();

        public void Generate(WorldModel model)
        {
            var startPoint = Random.Range(0, model.CellModels.Count);
            var startElement = model[startPoint];

            while (startElement.IsUsed || startElement.IsPath)
            {
                startPoint = Random.Range(0, model.CellModels.Count);
                startElement = model[startPoint];
            }

            startElement.TransformObject(ObjectType.Path);
            startElement.PathElementModel.SetStartPath();

            startElement.PathElementModel.MoveLeft();
            startElement.PathElementModel.LeftNearbyElement.PathElementModel.SetDefault();
            startElement.PathElementModel.MoveRight();
            startElement.PathElementModel.RightNearbyElement.PathElementModel.SetDefault();
            startElement.PathElementModel.MoveTop();
            startElement.PathElementModel.TopNearbyElement.PathElementModel.SetDefault();
            startElement.PathElementModel.MoveBottom();
            startElement.PathElementModel.BottomNearbyElement.PathElementModel.SetDefault();

            _active.Add(startElement.PathElementModel.BottomNearbyElement.PathElementModel);
            _active.Add(startElement.PathElementModel.TopNearbyElement.PathElementModel);
            _active.Add(startElement.PathElementModel.LeftNearbyElement.PathElementModel);
            _active.Add(startElement.PathElementModel.RightNearbyElement.PathElementModel);

            while (_active.Count > 0)
            {
                foreach (var elementModel in _remove)
                {
                    _active.Remove(elementModel);
                }

                _remove.Clear();
                foreach (var elementModel in _add)
                {
                    _active.Add(elementModel);
                }

                _add.Clear();

                foreach (var element in _active)
                {
                    if (element.GetDirectionModel() != null)
                    {
                        element.GetDirectionModel().Direction = element.Direction;
                    }

                    if (Random.Range(0, 100) < 95)
                    {
                        if (element.GetDirectionModel() != null && !element.GetDirectionModel().IsUsed)
                        {
                            if (Random.Range(0, 100) < 45)
                            {
                                if (element.Direction == PathDirection.Left || element.Direction == PathDirection.Right)
                                {
                                    if (Random.Range(0, 100) < 50)
                                        element.GetDirectionModel()?.RotateTop();
                                    else
                                        element.GetDirectionModel()?.RotateBottom();
                                }
                                else if (element.Direction == PathDirection.Top ||
                                         element.Direction == PathDirection.Bottom)
                                {
                                    if (Random.Range(0, 100) < 50)
                                        element.GetDirectionModel()?.RotateRight();
                                    else
                                        element.GetDirectionModel()?.RotateLeft();
                                }
                            }
                            else
                            {
                                if (element.GetDirectionModel() != null)
                                {
                                    element.GetDirectionModel().SetDefault();
                                }
                            }

                            if (element.GetDirectionModel() != null)
                                _add.Add(element.GetDirectionModel());
                        }
                        else
                        {
                            element.GetDirectionModel()?.SetStartPath();
                            if (element.GetDirectionModel() != null)
                                _add.Add(element.GetDirectionModel());
                        }
                    }
                    else
                    {
                        element?.SetEndPath();
                    }


                    _remove.Add(element);
                }
            }
        }
    }
}