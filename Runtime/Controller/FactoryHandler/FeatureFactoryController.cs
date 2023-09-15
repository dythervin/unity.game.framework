using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Dythervin.Collections;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework.Data;
using Dythervin.Game.Framework.View;
using JetBrains.Annotations;
using Zenject;

namespace Dythervin.Game.Framework.Controller
{
    public class FeatureFactoryController<TFactory, TModel, TData> : FeatureFactory<TFactory, TModel, TData>,
        IFeatureFactoryController<TModel>
        where TModel : class, IModelInitializable, IModel
        where TData : class, IModelData
        where TFactory : class, IModelFactory<TModel, TData>, IInitializable<IFeature, IModelInitIListener>
    {
        [CanBeNull] private IViewFactory _viewFactory;

        private readonly IViewContext _viewContext;

        private readonly HashSet<IControllerFactory> _controllerFactory = new HashSet<IControllerFactory>();

        private readonly bool _shouldInitAfterConstruct;

        private readonly bool _shouldConstructComponentsAfterConstruct;

        private readonly bool _shouldInitComponentsAfterInit;

        private readonly HashSet<IModel> _initializingModels = new HashSet<IModel>();

        private readonly bool _isComponentOwner = typeof(TModel).Implements<IModelComponentOwnerInitializer>();

        [Inject] private IControllerMap _map;

        /// <summary>
        ///     Called in constructor
        /// </summary>
        protected virtual bool ShouldInitAfterConstruct => !typeof(TModel).Implements(typeof(IModelComponent));

        /// <summary>
        ///     Called in constructor
        /// </summary>
        protected virtual bool ShouldConstructComponentsAfterConstruct => true;

        /// <summary>
        ///     Called in constructor
        /// </summary>
        protected virtual bool ShouldInitComponentsAfterInit => true;

        [SuppressMessage("ReSharper", "VirtualMemberCallInConstructor")]
        public FeatureFactoryController(TFactory modelFactory, IViewContext viewContext, TypeID featureGroupId,
            IFeatureParameter[] parameters) : base(modelFactory, featureGroupId, parameters)
        {
            _viewContext = viewContext;
            _shouldInitAfterConstruct = ShouldInitAfterConstruct;
            _shouldConstructComponentsAfterConstruct = _isComponentOwner && ShouldConstructComponentsAfterConstruct;
            _shouldInitComponentsAfterInit = _isComponentOwner && ShouldInitComponentsAfterInit;
        }

        public IFeatureFactoryController SetControllerFactory<TController>(
            ControllerFactory<TModel, TController> controllerFactory)
            where TController : class, IControllerInitializable, IController<TModel>, new()
        {
            AddController(controllerFactory);

            return this;
        }

        private void AddController(IControllerFactory controllerFactory)
        {
            if (!_controllerFactory.Add(controllerFactory))
                throw new Exception($"Already added controller {_controllerFactory.GetType()}");
        }

        public FeatureFactoryController<TFactory, TModel, TData> SetViewFactory(IViewFactory viewFactory)
        {
            if (viewFactory != null)
            {
                if (!viewFactory.ModelType.IsAssignableFrom(typeof(TModel)))
                {
                    throw new InvalidCastException($"{viewFactory.ModelType} is not assignable from {typeof(TModel)}");
                }

                _viewFactory = viewFactory;
            }

            return this;
        }

        IFeatureFactoryView IFeatureFactoryView.SetViewFactory(IViewFactory viewFactory)
        {
            return SetViewFactory(viewFactory);
        }

        IFeatureFactoryController IFeatureFactoryController.SetController(IControllerFactory controllerFactory)
        {
            if (controllerFactory != null)
            {
                if (!controllerFactory.ModelType.IsAssignableFrom(typeof(TModel)))
                {
                    throw new InvalidCastException(
                        $"{controllerFactory.ModelType} is not assignable from {typeof(TModel)}");
                }

                AddController(controllerFactory);
            }

            return this;
        }

        public override TModel Construct(TData data)
        {
            TModel model = modelFactory.Construct(data);
            IModelViewInitializable modelView = null;
            if (_viewFactory != null && _viewFactory.TryConstruct(model, out modelView))
            {
            }

            HashList<IController> controllers = null;
            if (_controllerFactory.Count > 0)
            {
                controllers = new HashList<IController>(_controllerFactory.Count);
                foreach (IControllerFactory controllerFactory in _controllerFactory)
                {
                    controllers.Add(controllerFactory.Construct(model));
                }
            }

            if (_shouldConstructComponentsAfterConstruct)
            {
                AnyFactory.ConstructComponents((IModelComponentOwnerInitializer)model);
            }

            if (_shouldInitAfterConstruct)
            {
                if (!_initializingModels.Add(model))
                    throw new Exception($"{typeof(TModel)} is already initializing, count : {_initializingModels.Count}");
                model.Init();
                InitModel(model, modelView, controllers);
            }

            return model;
        }

        private void InitModel(TModel model, [CanBeNull] IModelViewInitializable viewInitializable,
            [CanBeNull] IReadOnlyList<IController> controllers)
        {
            if (_shouldInitComponentsAfterInit)
                ((IModelComponentOwnerInitializer)model).ComponentListInitializer.InitComponents();

            viewInitializable?.Init(model, _viewContext);

            if (controllers != null)
            {
                foreach (IController controller in controllers.ToEnumerable())
                {
                    ((IControllerInitializable)controller)?.Init();
                }
            }

            _initializingModels.Remove(model);
        }

        IModel IModelFactory.Construct(IModelData data)
        {
            return Construct((TData)data);
        }

        public override void Notify(IModel value)
        {
            TryInitModel(value);
        }

        private void TryInitModel(IModel value)
        {
            if (!_initializingModels.Add(value))
                return;

            IModelViewInitializable viewInitializable = null;
            _viewFactory?.ViewMap.TryGet(value, out viewInitializable);

            _map.TryGet(value, out var controllers);

            InitModel((TModel)value, viewInitializable, controllers);
        }
    }
}