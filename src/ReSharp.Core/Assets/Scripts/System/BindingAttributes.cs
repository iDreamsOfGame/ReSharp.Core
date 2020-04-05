// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License. See LICENSE in the
// project root for license information.

using System.Reflection;

namespace System
{
    internal static class BindingAttributes
    {
        #region Fields

        /// <summary>
        /// The default binding attribute.
        /// </summary>
        public const BindingFlags DefaultBindingAttr = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default instance binding attribute.
        /// </summary>
        internal const BindingFlags DefaultInstanceBindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default static binding attribute.
        /// </summary>
        internal const BindingFlags DefaultStaticBindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default static get field binding attribute.
        /// </summary>
        internal const BindingFlags DefaultStaticGetFieldBindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetField;

        /// <summary>
        /// The default static get property binding attribute.
        /// </summary>
        internal const BindingFlags DefaultStaticGetPropertyBindingAttr = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.GetProperty;

        #endregion Fields
    }
}