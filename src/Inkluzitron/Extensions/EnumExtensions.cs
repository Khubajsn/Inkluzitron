﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace Inkluzitron.Extensions
{
    static public class EnumExtensions
    {
        /// <summary>
        /// A generic extension method that aids in reflecting 
        /// and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        /// 
        /// From: https://stackoverflow.com/a/25109103/3430085
        static public TAttribute GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }

        static public string GetDisplayName(this Enum enumValue)
            => enumValue.GetAttribute<DisplayAttribute>()?.GetName() ?? enumValue.ToString();
    }
}
