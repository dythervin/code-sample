using System;
using Dythervin.Game.Framework;
using Dythervin.Game.Framework.Data;
using Game.GameComponents;

namespace Game.Game
{
    public class GameExt : Model<IGameData, IModelContextExt, IGameComponentExt>, IGameExt
    {
        private readonly ObjectHashList<IEntityExt> _entities = new();

        private readonly ObjectHashList<IEntityExt> _activeEntities = new();

        private readonly Action<IObject> _entityOnObjectDestroyed;

        private readonly Action<IEntity> _entityOnObjectActiveChanged;

        IReadOnlyObjectListOut<IGameComponent> IGame.Components => Components;

        public IReadOnlyObjectListOut<IEntityExt> Entities => _entities;

        public IReadOnlyObjectListOut<IEntityExt> ActiveEntities => _activeEntities;

        IReadOnlyObjectListOut<IEntity> IGame.Entities => Entities;

        public GameExt(IGameData data, IModelContextExt context, IModelConstructorContext constructorContext) :
            base(data, context, constructorContext)
        {
            context.AnyFactory.OnConstruct += AnyFactoryOnConstruct;
            _entityOnObjectDestroyed = EntityOnObjectDestroyed;
            _entityOnObjectActiveChanged = EntityOnObjectActiveChanged;
        }

        private void EntityOnObjectActiveChanged(IEntity obj)
        {
            if (obj.IsActive)
                _activeEntities.Add((IEntityExt)obj);
            else
                _activeEntities.Remove((IEntityExt)obj);
        }

        private void AnyFactoryOnConstruct(IModel obj)
        {
            if (obj is IEntityExt entity)
            {
                if (_entities.Add(entity))
                    entity.OnObjectDestroyed += _entityOnObjectDestroyed;

                entity.OnObjectActiveChanged += _entityOnObjectActiveChanged;
                EntityOnObjectActiveChanged(entity);
            }
        }

        protected override void Destroyed()
        {
            for (int i = _entities.Count - 1; i >= 0; i--)
            {
                if (i >= _entities.Count)
                    continue;

                IEntityExt entityExt = _entities[i];
                entityExt.Dispose();
            }

            base.Destroyed();
        }

        private void EntityOnObjectDestroyed(IObject obj)
        {
            obj.OnObjectDestroyed -= _entityOnObjectDestroyed;
            ((IEntity)obj).OnObjectActiveChanged -= _entityOnObjectActiveChanged;
            _entities.Remove((IEntityExt)obj);
        }

        IReadOnlyObjectListOut<IModelComponent> IModelComponentOwner.Components => Components;
    }
}