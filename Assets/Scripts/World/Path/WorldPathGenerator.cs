using System;
using System.Collections.Generic;
using UnityEngine;
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
            var startPoint = Random.Range(0, model.WorldElementModels.Count);
            var startElement = model[startPoint];

            while (startElement.IsUsed || startElement.IsPath)
            {
                startPoint = Random.Range(0, model.WorldElementModels.Count);
                startElement = model[startPoint];
            }

            startElement.TransformObject(ObjectTypes.Path);
            startElement.PathElementModel.SetStartPath();

            startElement.PathElementModel.MoveLeft();
            startElement.PathElementModel?.LeftNearbyElement.PathElementModel?.SetDefault();
            startElement.PathElementModel.MoveRight();
            startElement.PathElementModel?.RightNearbyElement.PathElementModel?.SetDefault();
            startElement.PathElementModel.MoveTop();
            startElement.PathElementModel?.TopNearbyElement.PathElementModel?.SetDefault();
            startElement.PathElementModel.MoveBottom();
            startElement.PathElementModel?.BottomNearbyElement.PathElementModel?.SetDefault();

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

                    if (Random.Range(0, 100) < element.Data.ChanceToGeneratePath)
                    {
                        if (element.GetDirectionModel() != null && !element.GetDirectionModel().IsUsed)
                        {
                            if (Random.Range(0, 100) < element.Data.RotationChance)
                            {
                                var randomNumber = Random.Range(0, 100);

                                if (element.Direction == PathDirection.Left)
                                {
                                    if (randomNumber < 50)
                                    {
                                        Debug.Log("Слева вверх");
                                        element.GetDirectionModel()?.RotateFromLeftToTop();
                                    }
                                    else
                                    {
                                        Debug.Log("Слева вниз");
                                        element.GetDirectionModel()?.RotateFromLeftToBottom();
                                    }
                                }

                                if (element.Direction == PathDirection.Right)
                                {
                                    if (randomNumber < 50)
                                    {
                                        Debug.Log("Справа вверх");
                                        element.GetDirectionModel()?.RotateFromRightToTop();
                                    }
                                    else
                                    {
                                        Debug.Log("Справа вниз");
                                        element.GetDirectionModel()?.RotateFromRightToBottom();
                                    }
                                }

                                if (element.Direction == PathDirection.Top)
                                {
                                    if (randomNumber < 50)
                                    {
                                        Debug.Log("Сверху налево");
                                        element.GetDirectionModel()?.RotateFromTopToLeft();
                                    }
                                    else
                                    {
                                        Debug.Log("Сверху направо");
                                        element.GetDirectionModel()?.RotateFromTopToRight();
                                    }
                                }

                                if (element.Direction == PathDirection.Bottom)
                                {
                                    if (randomNumber < 50)
                                    {
                                        Debug.Log("Снизу налево");
                                        element.GetDirectionModel()?.RotateFromBottomToLeft();
                                    }
                                    else
                                    {
                                        Debug.Log("Снизу направо");
                                        element.GetDirectionModel()?.RotateFromBottomToRight();
                                    }
                                }
                            }
                            else
                            {
                                Debug.Log("НЕ БУДЕТ ПОВОРОТА, СТАВЛЮ ДЕФОЛТ"); //не работает
                                element.GetDirectionModel()?.SetDefault();
                            }
                        }
                        else
                        {
                            Debug.Log("ПУТЬ ЗАНЯТ. ПОСТАВЛЮ НОВОЕ НАЧАЛО"); //неправильно работает
                            element.GetDirectionModel()?.SetStartPath();
                        }

                        if (element.GetDirectionModel() != null)
                            _add.Add(element.GetDirectionModel());
                    }
                    else
                    {
                        Debug.Log("ПУТЬ НЕ СГЕНЕРИРОВАН. СТАВЛЮ КОНЕЦ");
                        element.GetDirectionModel()?.SetEndPath();
                    }

                    _remove.Add(element);
                }
            }
        }
    }
}