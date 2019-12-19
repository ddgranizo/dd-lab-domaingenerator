using DD.Lab.Wpf.Drm.Attributes;
using DD.Lab.Wpf.Models.Inputs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace DD.Lab.Wpf.Drm.Models
{
    public class Attribute
    {
        public enum AttributeType
        {
            Guid = 1,
            Bool = 10,
            Int = 11,
            Decimal = 13,
            Double = 14,
            Datetime = 15,
            String = 20,
            OptionSet = 40,

            State = 50,

            EntityReference = 90,
        }

        public Attribute()
        {
        }


        public bool IsMandatory { get; set; }
        public AttributeType Type { get; set; }
        public string LogicalName { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<OptionSetValue> Options { get; set; }

        public string ReferencedEntity { get; set; }

        public Attribute(AttributeType type, string logicalName, string displayName, string description, bool isMandatory = false)
        {
            if (string.IsNullOrEmpty(logicalName))
            {
                throw new ArgumentException("message", nameof(logicalName));
            }

            if (string.IsNullOrEmpty(displayName))
            {
                throw new ArgumentException("message", nameof(displayName));
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentException("message", nameof(description));
            }

            Type = type;
            LogicalName = logicalName;
            DisplayName = displayName;
            Description = description;
            IsMandatory = isMandatory;
        }

        public static List<OptionSetValue> GetOptionsFromPropertyInfo(Type mainType, PropertyInfo propertyInfo)
        {
            var enumAttribute = GetCustomAttributeType<EnumAtrribute>(propertyInfo);
            if (enumAttribute != null)
            {
                var enumName = GetCustomNameAttributes<string>(enumAttribute, "EnumName");
                var enumWithName = mainType.GetMembers().FirstOrDefault(k => k.Name == enumName);
                if (enumWithName == null)
                {
                    throw new Exception($"Can't find any enum with name '{enumWithName}' defined in the class");
                }

                var items = (MemberInfo[])GetPropValue(enumWithName, "DeclaredMembers");
                var data = items
                    .Where(k => k.Name != "value__")
                    .Select(k => new OptionSetValue(k.Name, (int)Enum.Parse((Type)k.ReflectedType, k.Name)))
                    .ToList();

                return data;
            }
            else
            {
                var options = GetCustomAttributesType<OptionSetAtrribute>(propertyInfo);
                if (options.Count == 0)
                {
                    throw new Exception("For attributes of type 'OptionSetValue' use tags [OptionSetAtrribute(DisplayName = \"MyDisplayName\", Value = 2)]");
                }
                return options.Select(k => new OptionSetValue()
                {
                    DisplayName = GetCustomNameAttributes<string>(k, "DisplayName"),
                    Value = GetCustomNameAttributes<int>(k, "Value")
                }).ToList();
            }

        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        private static T GetCustomNameAttributes<T>(CustomAttributeData attribute, string attributeName)
        {
            var data = attribute.NamedArguments.FirstOrDefault(k => k.MemberName == attributeName);
            if (data == null)
            {
                return default(T);
            }
            return (T)data.TypedValue.Value;
        }

        public static string GetDescriptionFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var attr = GetCustomAttributeType<DescriptionAttribute>(propertyInfo);
            if (attr == null)
            {
                return null;
            }
            return attr.ConstructorArguments.First().Value.ToString();
        }


        public static bool GetMandatoryFromPropertyInfo(PropertyInfo propertyInfo)
        {
            var attr = GetCustomAttributeType<RequiredAttribute>(propertyInfo);
            return attr != null;
        }


        private static CustomAttributeData GetCustomAttributeType<T>(PropertyInfo data)
        {
            return data.CustomAttributes.Where(l => l.AttributeType == typeof(T)).FirstOrDefault();
        }

        private static List<CustomAttributeData> GetCustomAttributesType<T>(PropertyInfo data)
        {
            return data.CustomAttributes.Where(l => l.AttributeType == typeof(T)).ToList();
        }



        public static AttributeType GetAttributeTypeFromPropertyInfo(PropertyInfo propertyInfo)
        {
            if (propertyInfo.PropertyType.Name == "Guid")
            {
                return AttributeType.Guid;
            }
            else if (propertyInfo.PropertyType.Name == "String")
            {
                return AttributeType.String;
            }
            else if (propertyInfo.PropertyType.Name == "EntityReferenceValue")
            {
                return AttributeType.EntityReference;
            }
            else if (propertyInfo.PropertyType.Name == "OptionSetValue")
            {
                return AttributeType.OptionSet;
            }
            else if (propertyInfo.PropertyType.Name == "Int32")
            {
                return AttributeType.Int;
            }
            else if (propertyInfo.PropertyType.Name == "Boolean")
            {
                return AttributeType.Bool;
            }

            throw new NotImplementedException();
        }
    }
}
