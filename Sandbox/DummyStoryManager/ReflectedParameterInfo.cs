using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;

namespace DummyStoryManager
{
    public class ReflectedParameterInfo : ParameterInfo
    {
        private readonly string _name;
        private readonly object _defaultvalue;
        private readonly Type _type;

        public ReflectedParameterInfo(string name, object defaultValue, Type type)
        {
            _name = name;
            _defaultvalue = defaultValue;
            _type = type;
        }

        public override ParameterAttributes Attributes
        {
            get
            {
                return ParameterAttributes.None;
            }
        }

        public override IEnumerable<CustomAttributeData> CustomAttributes
        {
            get
            {
                return new List<CustomAttributeData>();
            }
        }

        public override object DefaultValue
        {
            get
            {
                return this._defaultvalue;
            }
        }

        public override bool HasDefaultValue
        {
            get
            {
                return this._defaultvalue != null;
            }
        }

        public override string Name
        {
            get
            {
                return this._name;
            }
        }

        public override Type ParameterType
        {
            get
            {
                return this._type;
            }
        }

        public override MemberInfo Member
        {
            get
            {
                return new DynamicMethod("Activity",typeof(object),new Type[]{this.ParameterType});
            }
        }

        public override object[] GetCustomAttributes(bool inherit)
        {
            return new object[0];
        }

        public override object[] GetCustomAttributes(Type attributeType, bool inherit)
        {
            return this.ParameterType.GetCustomAttributes(attributeType,inherit);
        }

        public override IList<CustomAttributeData> GetCustomAttributesData()
        {
            return new List<CustomAttributeData>();
        }
    }
}