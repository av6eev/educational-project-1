using System.Collections.Generic;

namespace World.Experimental.Systems
{
    public class SystemCollection : ISystem
    {
        private readonly Dictionary<SystemTypes, ISystem> _systems = new Dictionary<SystemTypes, ISystem>();
        private readonly List<SystemTypes> _removedSystems = new List<SystemTypes>();

        public void Update()
        {
            foreach (var removedSystem in _removedSystems)
            {
                _systems.Remove(removedSystem);
            }
            _removedSystems.Clear();

            foreach (var system in _systems.Values)
            {
                system.Update();
            }
        }

        public T Get<T>(SystemTypes systemType) where T : ISystem
        {
            return (T)_systems[systemType];
        }

        public void Add(SystemTypes systemType, ISystem system)
        {
            _systems.Add(systemType, system);
        }
        
        public void Clear()
        {
            _systems.Clear();
        }

        public void Remove(SystemTypes type)
        {
            _removedSystems.Add(type);
        }
    }
}