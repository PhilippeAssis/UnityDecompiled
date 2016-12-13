using System;

namespace Unity.IL2CPP.CompilerServices
{
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public class Il2CppSetOptionAttribute : Attribute
	{
		public Option Option
		{
			get;
			private set;
		}

		public object Value
		{
			get;
			private set;
		}

		public Il2CppSetOptionAttribute(Option option, object value)
		{
			this.Option = option;
			this.Value = value;
		}
	}
}
