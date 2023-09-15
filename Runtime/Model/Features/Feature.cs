using System;
using Dythervin.Common;
using Dythervin.Core.Extensions;
using Dythervin.Game.Framework.Data;

namespace Dythervin.Game.Framework
{
    public abstract class Feature : IFeature
    {
        private readonly IFeatureParameter[] _featureParameters;

        public abstract FeatureId FeatureId { get; }

        protected Feature(IFeatureParameter[] featureParameters)
        {
            _featureParameters = featureParameters ?? Array.Empty<IFeatureParameter>();
            foreach (IFeatureParameter featureParameter in _featureParameters)
            {
                if (featureParameter == null)
                    throw new NullReferenceException(nameof(featureParameter));
            }
        }

        public bool TryGetParameter<T>(out T parameter, Predicate<T> predicate = null)
            where T : class
        {
            predicate ??= Predicates<T>.True;
            foreach (IFeatureParameter featureParameter in _featureParameters)
            {
                if (featureParameter is T foo && predicate.Invoke(foo))
                {
                    parameter = foo;
                    return true;
                }
            }

            parameter = null;
            return false;
        }

        public bool TryGetParameter(Type type, out IFeatureParameter parameter,
            Predicate<IFeatureParameter> predicate = null)
        {
            predicate ??= Predicates<IFeatureParameter>.True;
            foreach (IFeatureParameter featureParameter in _featureParameters)
            {
                if (featureParameter.GetType().Implements(type) && predicate.Invoke(featureParameter))
                {
                    parameter = featureParameter;
                    return true;
                }
            }

            parameter = null;
            return false;
        }
    }
}