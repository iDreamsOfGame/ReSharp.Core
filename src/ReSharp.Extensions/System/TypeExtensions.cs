// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
// ReSharper disable UseArrayEmptyMethod

// ReSharper disable InvertIf

namespace System
{
    /// <summary>
    /// Extension method collection for <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets the static field value.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get static field value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The value of the static field.</returns>
        public static object GetStaticFieldValue(this Type type, string fieldName, BindingFlags bindingFlags = BindingFlagsCollection.StaticGetFieldBindingFlags)
        {
            object fieldValue = null;
            var targetType = type;

            while (targetType != null)
            {
                var fieldInfo = targetType.GetField(fieldName, bindingFlags);

                if (fieldInfo != null)
                {
                    fieldValue = fieldInfo.GetValue(null);
                    targetType = null;
                }
                else
                {
                    targetType = targetType.BaseType;
                }
            }

            return fieldValue;
        }

        /// <summary>
        /// Sets the static field value of the <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to set static field value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value of the static field to set.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void SetStaticFieldValue(this Type type,
            string fieldName,
            object value,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticSetFieldBindingFlags)
        {
            var targetType = type;

            while (targetType != null)
            {
                var fieldInfo = targetType.GetField(fieldName, bindingFlags);

                if (fieldInfo != null)
                {
                    fieldInfo.SetValue(null, value);
                    targetType = null;
                }
                else
                {
                    targetType = targetType.BaseType;
                }
            }
        }

        /// <summary>
        /// Gets the static field value pairs.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The static field value pairs of the type.</returns>
        public static Dictionary<string, object> GetStaticFieldValuePairs(this Type type, BindingFlags bindingFlags = BindingFlagsCollection.StaticGetFieldBindingFlags)
        {
            var infos = type.GetFields(bindingFlags);
            return infos.ToDictionary(fieldInfo => fieldInfo.Name, fieldInfo => fieldInfo.GetValue(null));
        }

        /// <summary>
        /// Gets the static property value.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get static property value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The value of the static property.</returns>
        public static object GetStaticPropertyValue(this Type type, string propertyName, BindingFlags bindingFlags = BindingFlagsCollection.StaticGetPropertyBindingFlags)
        {
            object propertyValue = null;
            var targetType = type;

            while (targetType != null)
            {
                var propertyInfo = targetType.GetProperty(propertyName, bindingFlags);

                if (propertyInfo != null)
                {
                    propertyValue = propertyInfo.GetValue(null, null);
                    targetType = null;
                }
                else
                {
                    targetType = targetType.BaseType;
                }
            }

            return propertyValue;
        }

        /// <summary>
        /// Sets the static property value of the <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to set static property value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value of the static property.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void SetStaticPropertyValue(this Type type,
            string propertyName,
            object value,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticSetPropertyBindingFlags)
        {
            var targetType = type;

            while (targetType != null)
            {
                var propertyInfo = targetType.GetProperty(propertyName, bindingFlags);

                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(null, value, null);
                    targetType = null;
                }
                else
                {
                    targetType = targetType.BaseType;
                }
            }
        }

        /// <summary>
        /// Gets the static property value pairs.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The static property value pairs of the type.</returns>
        public static Dictionary<string, object> GetStaticPropertyValuePairs(this Type type, BindingFlags bindingFlags = BindingFlagsCollection.StaticGetPropertyBindingFlags)
        {
            var infos = type.GetProperties(bindingFlags);
            return infos.ToDictionary(propertyInfo => propertyInfo.Name, propertyInfo => propertyInfo.GetValue(null, null));
        }

        /// <summary>
        /// Determines whether this <see cref="Type"/> has the static method by a given name.
        /// </summary>
        /// <param name="type">The <see cref="Type"/>.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns><c>true</c> if this <see cref="Type"/> [has static method]; otherwise, <c>false</c>.</returns>
        public static bool HasStaticMethod(this Type type, string methodName, BindingFlags bindingFlags = BindingFlagsCollection.StaticBindingFlags)
        {
            var info = type.GetMethod(methodName, bindingFlags);
            return info != null;
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// bitmask comprised of one or more <see cref="BindingFlags"/> and the parameters for the constructor.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke constructor.</param>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type (under the constraints of the
        /// default binder) of the parameters for this constructor.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type, object[] parameters = null, BindingFlags bindingFlags = BindingFlagsCollection.ConstructorBindingFlags)
        {
            var types = Type.EmptyTypes;

            if (parameters != null)
            {
                types = new Type[parameters.Length];

                for (int i = 0, length = parameters.Length; i < length; i++)
                {
                    types[i] = parameters[i].GetType();
                }
            }

            var ctor = type.GetConstructor(bindingFlags, null, types, new ParameterModifier[0]);
            return ctor != null ? ctor.Invoke(parameters) : null;
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// bitmask comprised of one or more <see cref="BindingFlags"/>, an array of <see
        /// cref="Type"/> objects representing the number, order, and type of the parameters for the
        /// constructor to get and the parameters for the constructor.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke constructor.</param>
        /// <param name="types">
        /// An array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the constructor to get.
        /// </param>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type (under the constraints of the
        /// default binder) of the parameters for this constructor.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type,
            Type[] types,
            object[] parameters = null,
            BindingFlags bindingFlags = BindingFlagsCollection.ConstructorBindingFlags)
        {
            var ctor = type.GetConstructor(bindingFlags, null, types, new ParameterModifier[0]);
            return ctor != null ? ctor.Invoke(parameters) : null;
        }

        /// <summary>
        /// Invokes the generic static method of the <see cref="Type"/> by the given <see
        /// cref="Type"/>, a given name, a generic <see cref="Type"/> and the parameters for the
        /// static method.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke generic static method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypes">
        /// Types to be substituted for the type parameters for the current generic method definition.
        /// </param>
        /// <param name="parameters">The parameters for the generic static method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The object of the method return.</returns>
        public static object InvokeGenericStaticMethod(this Type type,
            string methodName,
            Type[] genericTypes,
            object[] parameters = null,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticBindingFlags)
        {
            var methodInfo = type.GetMethod(methodName, bindingFlags);
            if (methodInfo == null)
                return null;

            var genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);
            return genericMethodInfo.Invoke(null, parameters);
        }

        /// <summary>
        /// Invokes the static method by a given <see cref="Type"/>, a given name of method, an
        /// array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the static method to get and the parameters for the static method.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke static method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="types">
        /// An array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the static method to get.
        /// </param>
        /// <param name="parameters">The parameters for the static method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type,
            string methodName,
            Type[] types,
            object[] parameters = null,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticBindingFlags)
        {
            var info = type.GetMethod(methodName, bindingFlags, null, CallingConventions.Any, types, new ParameterModifier[0]);
            return info != null ? info.Invoke(null, parameters) : null;
        }

        /// <summary>
        /// Invokes the static method by a given <see cref="Type"/>, a given name of method, an
        /// array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the static method to get and the parameters for the static method.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke static method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="types">
        /// An array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the static method to get.
        /// </param>
        /// <param name="parameters">The referenced parameters.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type,
            string methodName,
            Type[] types,
            ref object[] parameters,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticBindingFlags) =>
            type.InvokeStaticMethod(methodName, types, parameters, bindingFlags);

        /// <summary>
        /// Invokes the static method of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// given name of method and the parameters for the static method.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke static method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters for the static method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type,
            string methodName,
            object[] parameters = null,
            BindingFlags bindingFlags = BindingFlagsCollection.StaticBindingFlags)
        {
            var types = Type.EmptyTypes;

            if (parameters != null)
            {
                types = new Type[parameters.Length];

                for (int i = 0, length = types.Length; i < length; i++)
                {
                    types[i] = parameters[i].GetType();
                }
            }

            return InvokeStaticMethod(type, methodName, types, parameters, bindingFlags);
        }
    }
}