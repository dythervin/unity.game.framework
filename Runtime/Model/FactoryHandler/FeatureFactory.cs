using Dythervin.Common;
using Dythervin.Core.Utils;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public abstract class FeatureFactory<TModelFactory, TModel, TModelData> : Feature,
        IFeatureFactory<TModel>,
        IModelInitIListener
        where TModel : class, IModelInitializable, IModel
        where TModelData : class, IModelData
        where TModelFactory : class, IModelFactory<TModel, TModelData>, IInitializable<IFeature, IModelInitIListener>
    {
        public readonly TModelFactory modelFactory;

        public IAnyFactoryControl AnyFactory { get; private set; }

        IModelInitIListener IFeatureFactory.ModelInitIListener => this;

        IModelFactory<TModel> IFeatureFactory<TModel>.ModelFactory => modelFactory;

        IModelFactory IFeatureFactory.ModelFactory => modelFactory;

        public abstract TModel Construct(TModelData data);

        public sealed override FeatureId FeatureId { get; }

        protected FeatureFactory(TModelFactory modelFactory, TypeID featureGroupId,
            IFeatureParameter[] featureParameters) : base(featureParameters)
        {
            this.modelFactory = modelFactory;
            FeatureId = new FeatureId(featureGroupId, Features.GetDataId<TModelData>());
        }

        public void Init(IAnyFactoryControl data)
        {
            DAssert.IsNotNull(data);
            AnyFactory = data;
            modelFactory.Init(this, this);
        }

        IModel IModelFactory.Construct(IModelData data)
        {
            return Construct((TModelData)data);
        }

        public abstract void Notify(IModel value);
    }
}