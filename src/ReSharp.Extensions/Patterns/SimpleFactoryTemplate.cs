// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System;
using System.Collections.Generic;

namespace ReSharp.Patterns
{
    /// <summary>
    /// The template of simple factory design pattern.
    /// </summary>
    /// <typeparam name="TInstance">The type of the factory instance. </typeparam>
    /// <typeparam name="TKey">The type of key to get instance type. </typeparam>
    /// <typeparam name="TInterface">The type of interface that instance implemented. </typeparam>
    public abstract class SimpleFactoryTemplate<TInstance, TKey, TInterface> : Singleton<TInstance> where TInstance : class
    {
        private readonly Dictionary<TKey, Type> keyTypeMap;

        /// <summary>
        /// Initializes a new instance of the <see>
        ///     <cref>SimpleFactoryTemplate</cref>
        /// </see>.
        /// </summary>
        /// <param name="keyTypeMap">The key and instance type pairs. </param>
        protected SimpleFactoryTemplate(Dictionary<TKey, Type> keyTypeMap)
        {
            this.keyTypeMap = keyTypeMap;
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
            return type == null ? default : (TInterface)type.InvokeConstructor(parameters);
        }

        private Type GetInstanceType(TKey key) => keyTypeMap != null && keyTypeMap.TryGetValue(key, out var type) ? type : null;
    }
}