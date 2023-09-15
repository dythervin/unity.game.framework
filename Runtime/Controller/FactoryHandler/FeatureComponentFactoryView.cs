using System;
using System.Collections.Generic;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;

namespace Dythervin.Game.Framework.Controller
{
    public class FeatureComponentFactoryView<TFactory, TComponent, TData> :
        FeatureFactoryController<TFactory, TComponent, TData>,
        IFeatureComponentFactory
        where TComponent : class, IModelInitializable, IModelComponent
        where TData : class, IModelComponentData
        where TFactory : class, IModelFactory<TComponent, TData>, IInitializable<IFeature, IModelInitIListener>
    {
        public FeatureComponentFactoryView<TFactory, TComponent, TData> AllowMultiple()
        {
            IsMultipleAllowed = true;
            return this;
        }

        public FeatureComponentFactoryView(TFactory modelFactory, IViewContext viewContext, TypeID featureGroupId,
            IFeatureParameter[] parameters) : base(modelFactory, viewContext, featureGroupId, parameters)
        {
        }

        private readonly HashList<Type> _fastTypeAccess = new HashList<Type>();

        private static Type GetInterface(Type dataType)
        {
            if (dataType.Implements(typeof(IModelDataWrapped<>)))
            {
                foreach (Type @interface in dataType.GetInterfaces())
                {
                    Type type = @interface;
                    if (!type.IsGenericType || type.GetGenericTypeDefinition() == typeof(IModelDataWrapped<>))
                        continue;

                    return GetInterface(type.GenericTypeArguments[0]);
                }
            }

            return dataType;
        }

        IFeatureComponentFactory IFeatureComponentFactory.AddFastTypeAccess<T>()
        {
            return AddFastTypeAccess<T>();
        }

        public FeatureComponentFactoryView<TFactory, TComponent, TData> AddFastTypeAccess<T>()
            where T : class, IObject
        {
            Type type = typeof(T);
            DAssert.IsTrue(typeof(TComponent).Implements(type));
            _fastTypeAccess.Add(type);
            return this;
        }

        public bool IsMultipleAllowed { get; private set; }

        public IReadOnlyList<Type> FastTypeAccess => _fastTypeAccess;
    }
}