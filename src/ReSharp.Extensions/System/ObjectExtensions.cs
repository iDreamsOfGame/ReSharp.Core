// Copyright (c) Jerry Lee. All rights reserved. Licensed under the MIT License.
// See LICENSE in the project root for license information.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace System
{
    /// <summary>
    /// Extension methods collection of <see cref="object"/>.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Creates a shallow clone of the current <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The type of object return.</typeparam>
        /// <param name="source">The current <see cref="object"/>.</param>
        /// <returns>A shallow clone of the current <see cref="object"/>.</returns>
        public static T ShallowClone<T>(this T source) => (T)source.InvokeMethod("MemberwiseClone");

        /// <summary>
        /// Creates a deep clone of the current <see cref="object"/>.
        /// </summary>
        /// <param name="source">The current <see cref="object"/>.</param>
        /// <returns>A deep clone of the current <see cref="object"/>.</returns>
        public static object DeepClone(this object source)
        {
            var targetType = source.GetType();

            object target;
            if (targetType.IsValueType || source is string)
            {
                target = source;
            }
            else
            {
                target = Activator.CreateInstance(targetType);
                var memberCollection = targetType.GetMembers();

                foreach (var memberInfo in memberCollection)
                {
                    if (memberInfo.MemberType == MemberTypes.Field)
                    {
                        var fieldInfo = (FieldInfo)memberInfo;
                        var fieldValue = fieldInfo.GetValue(source);

                        if (fieldValue == null)
                            continue;

                        try
                        {
                            if (fieldValue is ICloneable cloneable)
                            {
                                fieldInfo.SetValue(target, cloneable.Clone());
                            }
                            else
                            {
                                fieldInfo.SetValue(target, fieldValue.DeepClone());
                            }
                        }
                        catch (Exception)
                        {
                            // ignored
                        }
                    }
                    else if (memberInfo.MemberType == MemberTypes.Property)
                    {
                        var propertyInfo = (PropertyInfo)memberInfo;
                        var setMethodInfo = propertyInfo.GetSetMethod(false);

                        if (setMethodInfo != null)
                        {
                            var propertyValue = propertyInfo.GetValue(source, null);

                            if (propertyValue == null)
                                continue;

                            try
                            {
                                if (propertyValue is ICloneable cloneable)
                                {
                                    propertyInfo.SetValue(target, cloneable.Clone(), null);
                                }
                                else
                                {
                                    propertyInfo.SetValue(target, propertyValue.DeepClone(), null);
                                }
                            }
                            catch (Exception)
                            {
                                // ignored
                            }
                        }
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// Creates a deep clone of the current <see cref="object"/>, and return an object which has same fields or properties of current <see cref="object"/>.
        /// </summary>
        /// <typeparam name="T">The type of object return.</typeparam>
        /// <param name="source">The current <see cref="object"/>.</param>
        /// <returns>A deep clone of the current <see cref="object"/>.</returns>
        public static T DeepClone<T>(this object source) where T : class
        {
            var sourceType = source.GetType();
            var targetType = typeof(T);

            var target = (T)Activator.CreateInstance(targetType);
            var memberCollection = sourceType.GetMembers();

            foreach (var memberInfo in memberCollection)
            {
                if (memberInfo.MemberType == MemberTypes.Field)
                {
                    var fieldInfo = (FieldInfo)memberInfo;
                    var fieldValue = fieldInfo.GetValue(source);

                    if (fieldValue == null)
                        continue;
                    try
                    {
                        if (fieldValue is ICloneable cloneable)
                        {
                            fieldInfo.SetValue(target, cloneable.Clone());
                        }
                        else
                        {
                            fieldInfo.SetValue(target, fieldValue.DeepClone());
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
                else if (memberInfo.MemberType == MemberTypes.Property)
                {
                    var propertyInfo = (PropertyInfo)memberInfo;
                    var setMethodInfo = propertyInfo.GetSetMethod(false);

                    if (setMethodInfo == null) 
                        continue;
                    
                    var propertyValue = propertyInfo.GetValue(source, null);

                    if (propertyValue == null)
                        continue;
                    try
                    {
                        if (propertyValue is ICloneable cloneable)
                        {
                            propertyInfo.SetValue(target, cloneable.Clone(), null);
                        }
                        else
                        {
                            propertyInfo.SetValue(target, propertyValue.DeepClone(), null);
                        }
                    }
                    catch (Exception)
                    {
                        // ignored
                    }
                }
            }

            return target;
        }

        /// <summary>
        /// Searches for the event field with the specified name.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="eventName"></param>
        /// <param name="bindingFlags"></param>
        /// <returns></returns>
        public static FieldInfo GetEventField(this object source, string eventName, BindingFlags bindingFlags = BindingFlagsCollection.InstanceGetFieldBindingFlags)
        {
            if (source == null || string.IsNullOrEmpty(eventName))
                return null;
            
            FieldInfo field = null;
            var type = source.GetType();
            while (type != null)
            {
                // Find events defined as field
                field = type.GetField(eventName, bindingFlags);
                if (field != null && (field.FieldType == typeof(MulticastDelegate) || field.FieldType.IsSubclassOf(typeof(MulticastDelegate))))
                    break;
                
                // Find events defined as property { add; remove; }
                var fieldName = $"EVENT_{eventName.ToUpper()}";
                field = type.GetField(fieldName, bindingFlags);
                if (field != null)
                    break;
                
                type = type.BaseType;
            }
            
            return field;
        }

        /// <summary>
        /// Gets the field value by a given field name and a bitmask comprised of one or more <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object whose field value will be returned.</param>
        /// <param name="fieldName">The string containing the name of the data field to get.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An object containing the value of the field reflected by this instance.</returns>
        public static object GetFieldValue(this object source, string fieldName, BindingFlags bindingFlags = BindingFlagsCollection.InstanceGetFieldBindingFlags)
        {
            if (string.IsNullOrEmpty(fieldName)) 
                return null;
            
            var type = source.GetType();
            var info = type.GetField(fieldName, bindingFlags);
            return info != null ? info.GetValue(source) : null;
        }
        
        /// <summary>
        /// Sets the object field value by a given field name, a bitmask comprised of one or more
        /// <see cref="BindingFlags"/> and the value to set.
        /// </summary>
        /// <param name="source">The object whose field value to set.</param>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void SetFieldValue(this object source, string fieldName, object value, BindingFlags bindingFlags = BindingFlagsCollection.InstanceSetFieldBindingFlags)
        {
            var info = source.GetType().GetField(fieldName, bindingFlags);
            if (info == null)
                return;
            
            info.SetValue(source, value);
        }

        /// <summary>
        /// Gets the field value pairs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>The field value pairs of the type.</returns>
        public static Dictionary<string, object> GetFieldValuePairs(this object source, BindingFlags bindingFlags = BindingFlagsCollection.InstanceGetFieldBindingFlags)
        {
            var type = source.GetType();
            var infos = type.GetFields(bindingFlags);
            return infos.ToDictionary(info => info.Name, info => info.GetValue(source));
        }

        /// <summary>
        /// Gets the property value of by a given property name, a bitmask comprised of one or more
        /// <see cref="BindingFlags"/> and index values for indexed properties.
        /// </summary>
        /// <param name="source">The object whose property value will be returned.</param>
        /// <param name="propertyName">The name of the property.</param>
        /// <param name="index">
        /// Optional index values for indexed properties. The indexes of indexed properties are
        /// zero-based. This value should be null for non-indexed properties.
        /// </param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An object containing the value of the property reflected by this instance.</returns>
        public static object GetPropertyValue(this object source, string propertyName, object[] index = null, BindingFlags bindingFlags = BindingFlagsCollection.InstanceGetPropertyBindingFlags)
        {
            if (string.IsNullOrEmpty(propertyName)) 
                return null;
            
            var type = source.GetType();
            var info = type.GetProperty(propertyName, bindingFlags);
            return info != null ? info.GetValue(source, index) : null;
        }
        
        /// <summary>
        /// Sets the object property value by a given property name, a bitmask comprised of one or
        /// more <see cref="BindingFlags"/> and the value to set.
        /// </summary>
        /// <param name="source">The object whose property value to set.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="value">The value to set.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void SetPropertyValue(this object source, string propertyName, object value, BindingFlags bindingFlags = BindingFlagsCollection.InstanceSetPropertyBindingFlags)
        {
            var info = source.GetType().GetProperty(propertyName, bindingFlags);
            if (info == null) 
                return;
            
            info.SetValue(source, value, null);
        }

        /// <summary>
        /// Gets the property value pairs.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="bindingAttr">The binding attribute.</param>
        /// <returns>The property value pairs of the type.</returns>
        public static Dictionary<string, object> GetPropertyValuePairs(this object source, BindingFlags bindingAttr = BindingFlagsCollection.InstanceGetPropertyBindingFlags)
        {
            var type = source.GetType();
            var infos = type.GetProperties(bindingAttr);
            return infos.ToDictionary(info => info.Name, info => info.GetValue(source, null));
        }

        /// <summary>
        /// Determines whether has the specified method by a given name and a bitmask comprised of
        /// one or more <see cref="BindingFlags"/>.
        /// </summary>
        /// <param name="source">The object to search.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns><c>true</c> if this object has the specified method; otherwise, <c>false</c>.</returns>
        public static bool HasMethod(this object source, string methodName, BindingFlags bindingFlags = BindingFlagsCollection.InstanceBindingFlags)
        {
            var type = source.GetType();
            var info = type.GetMethod(methodName, bindingFlags);
            return info != null;
        }
        
        /// <summary>
        /// Invokes the method by a given name, a bitmask comprised of one or more <see
        /// cref="BindingFlags"/> and parameters for the method.
        /// </summary>
        /// <param name="source">The object on which to invoke the method.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameters">The parameters for the method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeMethod(this object source, string methodName, object[] parameters = null, BindingFlags bindingFlags = BindingFlagsCollection.InstanceBindingFlags)
        {
            var type = source.GetType();
            var info = type.GetMethod(methodName, bindingFlags);
            return info != null ? info.Invoke(source, parameters) : null;
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
        /// <param name="parameters">The parameters for the method.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        /// <returns>An object containing the return value of the invoked method.</returns>
        public static object InvokeGenericMethod(this object source, string methodName, Type[] genericTypes, object[] parameters = null, BindingFlags bindingFlags = BindingFlagsCollection.InstanceBindingFlags)
        {
            var methodInfo = source.GetType().GetMethod(methodName, bindingFlags);
            if (methodInfo == null)
                return null;
            
            var genericMethodInfo = methodInfo.MakeGenericMethod(genericTypes);
            return genericMethodInfo.Invoke(source, parameters);
        }

        /// <summary>
        /// Removes event handlers from an event source by given name of event.
        /// </summary>
        /// <param name="source">The event source.</param>
        /// <param name="eventName">The name of the event.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void RemoveEventHandlers(this object source, string eventName, BindingFlags bindingFlags = BindingFlagsCollection.InstanceBindingFlags)
        {
            if (source == null || string.IsNullOrEmpty(eventName))
                return;

            var eventField = source.GetEventField(eventName, bindingFlags);
            if (eventField == null)
                return;
            
            eventField.SetValue(source, null);
        }
        
        /// <summary>
        /// Removes all event handlers from an event source.
        /// </summary>
        /// <param name="source">The event source.</param>
        /// <param name="bindingFlags">
        /// A bitmask comprised of one or more <see cref="BindingFlags"/> that specify how the
        /// search is conducted.
        /// </param>
        public static void RemoveAllEventHandlers(this object source, BindingFlags bindingFlags = BindingFlagsCollection.InstanceBindingFlags)
        {
            var type = source.GetType();
            var eventInfos = type.GetEvents(bindingFlags);
            
            foreach (var eventInfo in eventInfos)
            {
                source.RemoveEventHandlers(eventInfo.Name, bindingFlags);
            }
        }
    }
}