// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Reflection;

namespace System
{
    /// <summary>
    /// The collection of <see cref="BindingFlags"/>.
    /// </summary>
    public static class BindingFlagsCollection
    {
        /// <summary>
        /// The default binding flags.
        /// </summary>
        public const BindingFlags DefaultBindingFlags = BindingFlags.Static | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default instance binding flags.
        /// </summary>
        public const BindingFlags InstanceBindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default instance get field binding flags.
        /// </summary>
        public const BindingFlags InstanceGetFieldBindingFlags = BindingFlags.GetField | InstanceBindingFlags;

        /// <summary>
        /// The default instance set field binding flags.
        /// </summary>
        public const BindingFlags InstanceSetFieldBindingFlags = BindingFlags.SetField | InstanceBindingFlags;

        /// <summary>
        /// The default instance get and set field binding flags.
        /// </summary>
        public const BindingFlags InstanceFieldBindingFlags = BindingFlags.GetField | BindingFlags.SetField | InstanceBindingFlags;

        /// <summary>
        /// The default instance get property binding flags.
        /// </summary>
        public const BindingFlags InstanceGetPropertyBindingFlags = BindingFlags.GetProperty | InstanceBindingFlags;

        /// <summary>
        /// The default instance set property binding flags.
        /// </summary>
        public const BindingFlags InstanceSetPropertyBindingFlags = BindingFlags.SetProperty | InstanceBindingFlags;

        /// <summary>
        /// The default instance property binding flags.
        /// </summary>
        public const BindingFlags InstancePropertyBindingFlags = BindingFlags.GetProperty | BindingFlags.SetProperty | InstanceBindingFlags;

        /// <summary>
        /// The default binding flags for constructor.
        /// </summary>
        public const BindingFlags ConstructorBindingFlags = BindingFlags.CreateInstance | InstanceBindingFlags;

        /// <summary>
        /// The default static binding flags.
        /// </summary>
        public const BindingFlags StaticBindingFlags = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        /// The default binding flags for static constructor.
        /// </summary>
        public const BindingFlags StaticConstructorBindingFlags = BindingFlags.CreateInstance | StaticBindingFlags;

        /// <summary>
        /// The default static get field binding flags.
        /// </summary>
        public const BindingFlags StaticGetFieldBindingFlags = BindingFlags.GetField | StaticBindingFlags;

        /// <summary>
        /// The default static set field binding flags.
        /// </summary>
        public const BindingFlags StaticSetFieldBindingFlags = BindingFlags.SetField | StaticBindingFlags;

        /// <summary>
        /// The default static field binding flags.
        /// </summary>
        public const BindingFlags StaticFieldBindingFlags = BindingFlags.GetField | BindingFlags.SetField | StaticBindingFlags;

        /// <summary>
        /// The default static get property binding flags.
        /// </summary>
        public const BindingFlags StaticGetPropertyBindingFlags = BindingFlags.GetProperty | StaticBindingFlags;

        /// <summary>
        /// The default static set property binding flags.
        /// </summary>
        public const BindingFlags StaticSetPropertyBindingFlags = BindingFlags.SetProperty | StaticBindingFlags;

        /// <summary>
        /// The default static property binding flags.
        /// </summary>
        public const BindingFlags StaticPropertyBindingFlags = BindingFlags.GetProperty | BindingFlags.SetProperty | StaticBindingFlags;
    }
}