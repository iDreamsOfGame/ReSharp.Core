// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Reflection;

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        #region Methods

        /// <summary>
        /// Creates a deep copy of the current <see cref="object"/>.
        /// </summary>
        /// <param name="source">The current <see cref="object"/>.</param>
        /// <returns>A deep copy of the current <see cref="object"/>.</returns>
        public static object DeepClone(this object source)
        {
            object target = null;
            Type targetType = source.GetType();

            if (targetType.IsValueType || source is string)
            {
                target = source;
            }
            else
            {
                target = Activator.CreateInstance(targetType);
                MemberInfo[] memberCollection = targetType.GetMembers();

                foreach (MemberInfo item in memberCollection)
                {
                    if (item.MemberType == MemberTypes.Field)
                    {
                        FieldInfo fieldInfo = (FieldInfo)item;
                        object fieldValue = fieldInfo.GetValue(source);

                        if (fieldValue is ICloneable)
                        {
                            fieldInfo.SetValue(target, (fieldValue as ICloneable).Clone());
                        }
                        else
                        {
                            fieldInfo.SetValue(target, fieldValue.DeepClone());
                        }
                    }
                    else if (item.MemberType == MemberTypes.Property)
                    {
                        PropertyInfo propertyInfo = (PropertyInfo)item;
                        MethodInfo setMethodInfo = propertyInfo.GetSetMethod(false);

                        if (setMethodInfo != null)
                        {
                            object propertyValue = propertyInfo.GetValue(source, null);

                            if (propertyValue is ICloneable)
                            {
                                propertyInfo.SetValue(target, (propertyValue as ICloneable).Clone(), null);
                            }
                            else
                            {
                                propertyInfo.SetValue(target, propertyValue.DeepClone(), null);
                            }
                        }
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// Creates a deep copy of the current <see cref="object"/>, and return an object which is
        /// inherited from the type of current <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The type of object return.</typeparam>
        /// <param name="source">The current <see cref="object"/>.</param>
        /// <returns>The object which is inherited from the type of current <see cref="object"/>.</returns>
        public static T DeepClone<T>(this object source) where T : class
        {
            Type sourceType = source.GetType();
            Type targetType = typeof(T);

            T target = (T)Activator.CreateInstance(targetType);
            MemberInfo[] memberCollection = sourceType.GetMembers();

            foreach (MemberInfo item in memberCollection)
            {
                if (item.MemberType == MemberTypes.Field)
                {
                    FieldInfo fieldInfo = (FieldInfo)item;
                    object fieldValue = fieldInfo.GetValue(source);

                    if (fieldValue is ICloneable)
                    {
                        fieldInfo.SetValue(target, (fieldValue as ICloneable).Clone());
                    }
                    else
                    {
                        fieldInfo.SetValue(target, fieldValue.DeepClone());
                    }
                }
                else if (item.MemberType == MemberTypes.Property)
                {
                    PropertyInfo propertyInfo = (PropertyInfo)item;
                    MethodInfo setMethodInfo = propertyInfo.GetSetMethod(false);

                    if (setMethodInfo != null)
                    {
                        object propertyValue = propertyInfo.GetValue(source, null);

                        if (propertyValue is ICloneable)
                        {
                            propertyInfo.SetValue(target, (propertyValue as ICloneable).Clone(), null);
                        }
                        else
                        {
                            propertyInfo.SetValue(target, propertyValue.DeepClone(), null);
                        }
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// Returns the invocation list of the specific <see cref="EventInfo"/> of the target <see cref="object"/>.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="eventInfo">The specific <see cref="EventInfo"/>.</param>
        /// <returns>
        /// The invocation list of the specific <see cref="EventInfo"/> of the target <see cref="object"/>
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>eventInfo</c> is <c>null</c>.</exception>
        public static Delegate[] GetEventInvocationList(this object source, EventInfo eventInfo)
        {
            if (eventInfo == null)
            {
                throw new ArgumentNullException(nameof(eventInfo));
            }

            Delegate[] invocationList = new Delegate[] { };

            object fieldValue = source.GetFieldValue(eventInfo.Name);

            // Try field
            if (fieldValue != null && fieldValue is Delegate)
            {
                invocationList = (fieldValue as Delegate).GetInvocationList();
            }
            else
            {
                // Try property
                object propertyValue = source.GetPropertyValue(eventInfo.Name);

                if (propertyValue != null && propertyValue is Delegate)
                {
                    invocationList = (propertyValue as Delegate).GetInvocationList();
                }
            }

            return invocationList;
        }

        /// <summary>
        /// Gets the normal field value by a given field name.
        /// </summary>
        /// <param name="source">The object whose field value will be returned.</param>
        /// <param name="fieldName">The string containing the name of the data field to get.</param>
        /// <returns>An object containing the value of the field reflected by this instance.</returns>
        public static object GetFieldValue(this object source, string fieldName)
        {
            return source.GetFieldValue(fieldName, BindingAttributes.DefaultInstanceBindingAttr);
        }

        /// <summary>
        /// Gets the field value by a given field name and a bitmask comprised of one or more <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object whose field value will be returned.</param>
        /// <param name="fieldName">The string containing the name of the data field to get.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An object containing the value of the field reflected by this instance.</returns>
        public static object GetFieldValue(this object source, string fieldName, BindingFlags bindingAttr)
        {
            if (!string.IsNullOrEmpty(fieldName))
            {
                Type type = source.GetType();
                FieldInfo info = type.GetField(fieldName, bindingAttr);

                if (info != null)
                {
                    return info.GetValue(source);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the field value pairs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>The field value pairs of the type.</returns>
        public static Dictionary<string, object> GetFieldValuePairs(this object source, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
        {
            Type type = source.GetType();
            FieldInfo[] infos = type.GetFields(bindingAttr);

            Dictionary<string, object> map = new Dictionary<string, object>();

            foreach (FieldInfo info in infos)
                map.Add(info.Name, info.GetValue(source));

            return map;
        }

        /// <summary>
        /// Gets the property value of by a given property name and index values for indexed properties.
        /// </summary>
        /// <param name="source">The object whose property value will be returned.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="index">
        /// Optional index values for indexed properties. The indexes of indexed properties are
        /// zero-based. This value should be null for non-indexed properties.
        /// </param>
        /// <returns>An object containing the value of the property reflected by this instance.</returns>
        public static object GetPropertyValue(this object source, string propertyName, object[] index = null)
        {
            return source.GetPropertyValue(propertyName, BindingAttributes.DefaultInstanceBindingAttr);
        }

        /// <summary>
        /// Gets the property value of by a given property name, a bitmask comprised of one or more
        /// <see cref="BindingFlags"/> and index values for indexed properties.
        /// </summary>
        /// <param name="source">The object whose property value will be returned.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="index">
        /// Optional index values for indexed properties. The indexes of indexed properties are
        /// zero-based. This value should be null for non-indexed properties.
        /// </param>
        /// <returns>An object containing the value of the property reflected by this instance.</returns>
        public static object GetPropertyValue(this object source, string propertyName, BindingFlags bindingAttr, object[] index = null)
        {
            if (!string.IsNullOrEmpty(propertyName))
            {
                Type type = source.GetType();
                PropertyInfo info = type.GetProperty(propertyName, bindingAttr);

                if (info != null)
                {
                    return info.GetValue(source, index);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the property value pairs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>The property value pairs of the type.</returns>
        public static Dictionary<string, object> GetPropertyValuePairs(this object source, BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance)
        {
            Type type = source.GetType();
            PropertyInfo[] infos = type.GetProperties(bindingAttr);

            Dictionary<string, object> map = new Dictionary<string, object>();

            foreach (PropertyInfo info in infos)
                map.Add(info.Name, info.GetValue(source, null));

            return map;
        }

        /// <summary>
        /// Determines whether has the specified method by a given name with the default instance of
        /// bitmask comprised of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object to search.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <returns><c>true</c> if this object has the specified method; otherwise, <c>false</c>.</returns>
        public static bool HasMethod(this object source, string methodName)
        {
            return source.HasMethod(methodName, BindingAttributes.DefaultInstanceBindingAttr);
        }

        /// <summary>
        /// Determines whether has the specified method by a given name and a bitmask comprised of
        /// one or more <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object to search.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns><c>true</c> if this object has the specified method; otherwise, <c>false</c>.</returns>
        public static bool HasMethod(this object source, string methodName, BindingFlags bindingAttr)
        {
            Type type = source.GetType();
            MethodInfo info = type.GetMethod(methodName, bindingAttr);

            if (info != null)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Invokes the generic method by a given name, the type to be substituted for the type
        /// parameters of the current generic method definition and parameters for the method with
        /// the default instance bitmask comprised of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object on which to invoke the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypes">
        /// Types to be substituted for the type parameters of the current generic method definition.
        /// </param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeGenericMethod(this object source, string methodName, Type[] genericTypes, object[] parameters = null)
        {
            return source.InvokeGenericMethod(methodName, genericTypes, BindingAttributes.DefaultInstanceBindingAttr, parameters);
        }

        /// <summary>
        /// Invokes the generic method by a given name, the type to be substituted for the type
        /// parameters of the current generic method definition, a bitmask comprised of one or more
        /// <see cref="BindingFlags"/> and parameters for the method.
        /// </summary>
        /// <param name="source">The object on which to invoke the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypes">
        /// Types to be substituted for the type parameters of the current generic method definition.
        /// </param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeGenericMethod(this object source, string methodName, Type[] genericTypes, BindingFlags bindingAttr, object[] parameters = null)
        {
            Type type = source.GetType();
            MethodInfo methodInfo = type.GetMethod(methodName, bindingAttr);

            if (methodInfo != null)
            {
                MethodInfo genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);

                if (genericMethodInfo != null)
                {
                    return genericMethodInfo.Invoke(source, parameters);
                }
            }

            return null;
        }

        /// <summary>
        /// Invokes the method by a given name and parameters for the method with the default
        /// instance bitmask comprised of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object on which to invoke the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeMethod(this object source, string methodName, object[] parameters = null)
        {
            return source.InvokeMethod(methodName, BindingAttributes.DefaultInstanceBindingAttr, parameters);
        }

        /// <summary>
        /// Invokes the method by a given name, a bitmask comprised of one or more <see
        /// cref="BindingFlags"/> and parameters for the method.
        /// </summary>
        /// <param name="source">The object on which to invoke the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeMethod(this object source, string methodName, BindingFlags bindingAttr, object[] parameters = null)
        {
            Type type = source.GetType();
            MethodInfo info = type.GetMethod(methodName, bindingAttr);

            if (info != null)
            {
                return info.Invoke(source, parameters);
            }

            return null;
        }

        /// <summary>
        /// Removes all event handlers from an event source.
        /// </summary>
        /// <param name="source">The event source.</param>
        public static void RemoveAllEventHandlers(this object source)
        {
            Type type = source.GetType();
            EventInfo[] eventInfos = type.GetEvents();

            foreach (EventInfo eventInfo in eventInfos)
            {
                source.RemoveEventHandlers(eventInfo.Name);
            }
        }

        /// <summary>
        /// Removes event handlers from an event source by given name of event.
        /// </summary>
        /// <param name="source">The event source.</param>
        /// <param name="eventName">The name of the event.</param>
        public static void RemoveEventHandlers(this object source, string eventName)
        {
            if (!string.IsNullOrEmpty(eventName))
            {
                Type type = source.GetType();
                EventInfo eventInfo = type.GetEvent(eventName);

                if (eventInfo != null)
                {
                    Delegate[] invocations = source.GetEventInvocationList(eventInfo);

                    foreach (Delegate invocation in invocations)
                    {
                        eventInfo.RemoveEventHandler(source, invocation);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the object field value by a given field name and the value to set with the default
        /// instance attribute of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object whose field value to set.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value to set.</param>
        public static void SetObjectFieldValue(this object source, string fieldName, object value)
        {
            source.SetObjectFieldValue(fieldName, BindingAttributes.DefaultInstanceBindingAttr, value);
        }

        /// <summary>
        /// Sets the object field value by a given field name, a bitmask comprised of one or more
        /// <see cref="BindingFlags"/> and the value to set.
        /// </summary>
        /// <param name="source">The object whose field value to set.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="value">The value to set.</param>
        public static void SetObjectFieldValue(this object source, string fieldName, BindingFlags bindingAttr, object value)
        {
            Type type = source.GetType();
            FieldInfo info = type.GetField(fieldName, bindingAttr);

            if (info != null)
            {
                info.SetValue(source, value);
            }
        }

        /// <summary>
        /// Sets the object property value by a given property name and the value to set with the
        /// default instance attribute of <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object whose property value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value to set.</param>
        public static void SetObjectPropertyValue(this object source, string propertyName, object value)
        {
            source.SetObjectPropertyValue(propertyName, BindingAttributes.DefaultInstanceBindingAttr, value);
        }

        /// <summary>
        /// Sets the object property value by a given property name, a bitmask comprised of one or
        /// more <see cref="BindingFlags"/> and the value to set.
        /// </summary>
        /// <param name="source">The object whose property value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="bindingAttr">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <param name="value">The value to set.</param>
        public static void SetObjectPropertyValue(this object source, string propertyName, BindingFlags bindingAttr, object value)
        {
            Type type = source.GetType();
            PropertyInfo info = type.GetProperty(propertyName, bindingAttr);

            if (info != null)
            {
                info.SetValue(source, value, null);
            }
        }

        #endregion Methods
    }
}