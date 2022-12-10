using System;
using System.Collections.Generic;

namespace ReSharp.Patterns
{
    /// <summary>
    /// The template of caching factory design pattern.
    /// </summary>
    /// <typeparam name="TKey">The type of key to get instance type. </typeparam>
    /// <typeparam name="TInterface">The type of interface that instance implemented. </typeparam>
    public abstract class CachingFactoryTemplate<TKey, TInterface>
    {
        private readonly Dictionary<Type, TInterface> instanceCache;

        private readonly Dictionary<TKey, Type> keyTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see>
        ///     <cref>CachingFactoryTemplate</cref>
        /// </see>.
        /// </summary>
        /// <param name="keyTypeMap">The key and instance type pairs. </param>
        protected CachingFactoryTemplate(Dictionary<TKey, Type> keyTypeMap)
        {
            this.keyTypeMap = keyTypeMap;
            instanceCache = new Dictionary<Type, TInterface>(keyTypeMap.Count);
        }

        /// <summary>
        /// Gets an instance.
        /// </summary>
        /// <param name="key">The key of the instance type to get. </param>
        /// <param name="parameters">The parameters of instance constructor. </param>
        /// <returns></returns>
        public TInterface GetInstance(TKey key, params object[] parameters)
        {
            var type = GetInstanceType(key);
            if (type == null)
                return default;
            
            if (instanceCache.TryGetValue(type, out var instance))
                return instance;
            
            instance = (TInterface)type.InvokeConstructor(parameters);
            instanceCache.Add(type, instance);
            return instance;
        }

        private Type GetInstanceType(TKey key) => keyTypeMap != null && keyTypeMap.TryGetValue(key, out var type) ? type : null;
    }
}