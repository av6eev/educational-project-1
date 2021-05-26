using System.Collections.Generic;

namespace Utilities
{
    public class ControllerCollection
    {
        private readonly List<IController> _controllerCollection = new List<IController>();

        public void Activate()
        {
            foreach (var controller in _controllerCollection)
            {
                controller.Activate();
            }
        }

        public void Deactivate()
        {
            foreach (var controller in _controllerCollection)
            {
                controller.Deactivate();
            }
        }

        public void Add(IController controller)
        {
            _controllerCollection.Add(controller);
        }

        public void Clear()
        {
            _controllerCollection.Clear();
        }
    }
}