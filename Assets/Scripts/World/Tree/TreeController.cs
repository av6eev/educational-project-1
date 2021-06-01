﻿using Utilities;
using World.Experimental;
using World.WorldElement;

namespace World.Tree
{
    public class TreeController : IController
    {
        private readonly TreeBlock _model;
        private readonly WorldElementView _view;

        public TreeController(TreeBlock model, WorldElementView view)
        {
            _model = model;
            _view = view;
        }
        public void Activate()
        {
            _model.IsTree = true;
            _view.ChangeObject(ContentHelper.Trees[_model.TreeType]);
            _view.Scale = _model.TreeSize;
        }

        public void Deactivate()
        {
        }
    }
}