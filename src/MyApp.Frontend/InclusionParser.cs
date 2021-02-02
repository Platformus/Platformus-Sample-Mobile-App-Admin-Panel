// Copyright © 2021 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Linq;
using System.Reflection;
using Magicalizer.Data.Entities.Abstractions;
using Magicalizer.Data.Repositories.Abstractions;

namespace MyApp.Frontend
{
  public class InclusionParser<TEntity> where TEntity : class, IEntity
  {
    public static Inclusion<TEntity>[] Parse(string fields)
    {
      return fields?.Split(',')
        .Select(i => AutofixInclusion(typeof(TEntity), i))
        .Where(i => !string.IsNullOrEmpty(i))
        .Distinct()
        .Select(i => new Inclusion<TEntity>(i)).ToArray();
    }

    private static string AutofixInclusion(Type type, string include)
    {
      string result = string.Empty;
      string propertyName = include.Split('.')[0];
      PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

      if (property == null)
        return result;

      result += property.Name;

      if (include.Contains('.'))
      {
        string subResult = AutofixInclusion(
          property.PropertyType.IsGenericType ?
            property.PropertyType.GetGenericArguments().First() : property.PropertyType,
            include.Substring(include.IndexOf('.') + 1)
        );

        result += string.IsNullOrEmpty(subResult) ? string.Empty : "." + subResult;
      }

      return result;
    }
  }
}
