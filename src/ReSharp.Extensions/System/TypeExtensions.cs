// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace System
{
    /// <summary>
    /// Extension method collection for <see cref="System.Type"/>.
    /// </summary>
    public static class TypeExtensions
    {
        #region Methods

        /// <summary>
        /// Gets the static field value.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get static field value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The value of the static field.</returns>
        public static object GetStaticFieldValue(this Type type, string fieldName, BindingFlags bindingAttr = BindingAttributes.DefaultStaticGetFieldBindingAttr)
        {
            object fieldValue = null;

            Type targetType = type;

            while (targetType != null)
            {
                FieldInfo fieldInfo = targetType.GetField(fieldName, bindingAttr);

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
        /// Gets the static field value pairs.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The static field value pairs of the type.</returns>
        public static Dictionary<string, object> GetStaticFieldValuePairs(this Type type, BindingFlags bindingAttr = BindingAttributes.DefaultStaticGetFieldBindingAttr)
        {
            FieldInfo[] infos = type.GetFields(bindingAttr);
            Dictionary<string, object> map = new Dictionary<string, object>();

            foreach (FieldInfo fieldInfo in infos)
            {
                map.Add(fieldInfo.Name, fieldInfo.GetValue(null));
            }

            return map;
        }

        /// <summary>
        /// Gets the static property value.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to get static property value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The value of the static property.</returns>
        public static object GetStaticPropertyValue(this Type type, string propertyName, BindingFlags bindingAttr = BindingAttributes.DefaultStaticGetPropertyBindingAttr)
        {
            object propertyValue = null;

            Type targetType = type;

            while (targetType != null)
            {
                PropertyInfo propertyInfo = targetType.GetProperty(propertyName, BindingAttributes.DefaultStaticGetPropertyBindingAttr);

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
        /// Gets the static property value pairs.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The static property value pairs of the type.</returns>
        public static Dictionary<string, object> GetStaticPropertyValuePairs(this Type type, BindingFlags bindingAttr = BindingAttributes.DefaultStaticGetPropertyBindingAttr)
        {
            PropertyInfo[] infos = type.GetProperties(bindingAttr);
            Dictionary<string, object> map = new Dictionary<string, object>();

            foreach (PropertyInfo propertyInfo in infos)
            {
                map.Add(propertyInfo.Name, propertyInfo.GetValue(null, null));
            }

            return map;
        }

        /// <summary>
        /// Determines whether this <see cref="Type"/> has the static method by a given name.
        /// </summary>
        /// <param name="type">The <see cref="Type"/>.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns><c>true</c> if this <see cref="Type"/> [has static method]; otherwise, <c>false</c>.</returns>
        public static bool HasStaticMethod(this Type type, string methodName)
        {
            MethodInfo info = type.GetMethod(methodName, BindingAttributes.DefaultStaticBindingAttr);

            if (info != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// bitmask comprised of one or more <see cref="BindingFlags"/>, an array of <see
        /// cref="Type"/> objects representing the number, order, and type of the parameters for the
        /// constructor to get and the parameters for the constructor.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke constructor.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="types">
        /// An array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the constructor to get.
        /// </param>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type (under the constraints of the
        /// default binder) of the parameters for this constructor.
        /// </param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type, BindingFlags bindingAttr, Type[] types, object[] parameters = null)
        {
            ConstructorInfo ctor = type.GetConstructor(bindingAttr, null, types, new ParameterModifier[0]);

            if (ctor != null)
            {
                return ctor.Invoke(parameters);
            }

            return null;
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/> and the
        /// parameters for the constructor with the default instance bitmask comprised of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke constructor.</param>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type (under the constraints of the
        /// default binder) of the parameters for this constructor.
        /// </param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type, object[] parameters = null)
        {
            return InvokeConstructor(type, BindingAttributes.DefaultInstanceBindingAttr, parameters);
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// bitmask comprised of one or more <see cref="BindingFlags"/> and the parameters for the constructor.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke constructor.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="parameters">
        /// An array of values that matches the number, order and type (under the constraints of the
        /// default binder) of the parameters for this constructor.
        /// </param>
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type, BindingFlags bindingAttr, object[] parameters = null)
        {
            Type[] types = Type.EmptyTypes;

            if (parameters != null)
            {
                types = new Type[parameters.Length];

                for (int i = 0, length = parameters.Length; i < length; i++)
                {
                    types[i] = parameters[i].GetType();
                }
            }

            ConstructorInfo ctor = type.GetConstructor(bindingAttr, null, types, new ParameterModifier[0]);

            if (ctor != null)
            {
                return ctor.Invoke(parameters);
            }

            return null;
        }

        /// <summary>
        /// Invokes the constructor of the <see cref="Type"/> by a given <see cref="Type"/>, an
        /// array of <see cref="Type"/> objects representing the number, order, and type of the
        /// parameters for the constructor to get and the parameters for the constructor.
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
        /// <returns>An instance of the class associated with the constructor.</returns>
        public static object InvokeConstructor(this Type type, Type[] types, object[] parameters = null)
        {
            return InvokeConstructor(type, BindingAttributes.DefaultInstanceBindingAttr, types, parameters);
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
        /// <returns>The object of the method return.</returns>
        public static object InvokeGenericStaticMethod(this Type type, string methodName, Type[] genericTypes, object[] parameters = null)
        {
            return type.InvokeGenericStaticMethod(methodName, genericTypes, BindingAttributes.DefaultStaticBindingAttr, parameters);
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
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="parameters">The parameters for the generic static method.</param>
        /// <returns>The object of the method return.</returns>
        public static object InvokeGenericStaticMethod(this Type type, string methodName, Type[] genericTypes, BindingFlags bindingAttr, object[] parameters = null)
        {
            MethodInfo methodInfo = type.GetMethod(methodName, bindingAttr);

            if (methodInfo != null)
            {
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);

                if (genericMethodInfo != null)
                {
                    return genericMethodInfo.Invoke(null, parameters);
                }
            }

            return null;
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
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type, string methodName, Type[] types, object[] parameters = null)
        {
            MethodInfo info = type.GetMethod(methodName,
                BindingAttributes.DefaultStaticBindingAttr,
                null,
                CallingConventions.Any,
                types,
                null);

            object result = info.Invoke(null, parameters);
            return result;
        }

        /// <summary>
        /// Invokes the static method.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="types">The types array of parameters.</param>
        /// <param name="parameters">The referenced parameters.</param>
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type,
            string methodName,
            Type[] types,
            ref object[] parameters)
        {
            MethodInfo info = type.GetMethod(methodName,
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic,
                null,
                CallingConventions.Any,
                types,
                null);

            object result = info.Invoke(null, parameters);
            return result;
        }

        /// <summary>
        /// Invokes the static method of the <see cref="Type"/> by a given <see cref="Type"/>, a
        /// given name of method and the parameters for the static method.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to invoke static method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters for the static method.</param>
        /// <returns>The object of static method return.</returns>
        public static object InvokeStaticMethod(this Type type, string methodName, object[] parameters = null)
        {
            Type[] types = Type.EmptyTypes;

            if (parameters != null)
            {
                types = new Type[parameters.Length];

                for (int i = 0, length = types.Length; i < length; i++)
                {
                    types[i] = parameters[i].GetType();
                }
            }

            return InvokeStaticMethod(type, methodName, types, parameters);
        }

        /// <summary>
        /// Sets the static field value of the <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to set static field value.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value of the static field to set.</param>
        public static void SetStaticFieldValue(this Type type, string fieldName, object value)
        {
            Type targetType = type;

            while (targetType != null)
            {
                FieldInfo fieldInfo = targetType.GetField(fieldName, BindingAttributes.DefaultStaticGetFieldBindingAttr);

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
        /// Sets the static property value of the <see cref="Type"/>.
        /// </summary>
        /// <param name="type">The <see cref="Type"/> to set static property value.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value of the static property.</param>
        public static void SetStaticPropertyValue(this Type type, string propertyName, object value)
        {
            Type targetType = type;

            while (targetType != null)
            {
                PropertyInfo propertyInfo = targetType.GetProperty(propertyName, BindingAttributes.DefaultStaticGetPropertyBindingAttr);

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

        #endregion Methods
    }
}