using Mono.Cecil;
using System;
using System.Collections.Generic;
using Unity.IL2CPP.ILPreProcessor;
using Unity.IL2CPP.Marshaling.MarshalInfoWriters;

namespace Unity.IL2CPP.Marshaling.BodyWriters.NativeToManaged
{
	internal class NativeToManagedMarshaler : InteropMarshaler
	{
		public NativeToManagedMarshaler(TypeResolver typeResolver, MarshalType marshalType, bool useUnicodeCharset) : base(typeResolver, marshalType, useUnicodeCharset)
		{
		}

		public override bool CanMarshalAsInputParameter(MarshaledParameter parameter)
		{
			return base.MarshalInfoWriterFor(parameter).CanMarshalTypeFromNative();
		}

		public override bool CanMarshalAsOutputParameter(MarshaledParameter parameter)
		{
			return base.MarshalInfoWriterFor(parameter).CanMarshalTypeToNative();
		}

		public override bool CanMarshalAsOutputParameter(MethodReturnType methodReturnType)
		{
			return base.MarshalInfoWriterFor(methodReturnType).CanMarshalTypeToNative();
		}

		public override string GetPrettyCalleeName()
		{
			return "Managed method";
		}

		public override string WriteMarshalEmptyInputParameter(CppCodeWriter writer, MarshaledParameter parameter, IList<MarshaledParameter> parameters, IRuntimeMetadataAccess metadataAccess)
		{
			return writer.WriteIfNotEmpty<string>(delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteCommentedLine("Marshaling of parameter '{0}' to managed representation", new object[]
				{
					parameter.NameInGeneratedCode
				});
			}, delegate(CppCodeWriter bodyWriter)
			{
				DefaultMarshalInfoWriter defaultMarshalInfoWriter = this.MarshalInfoWriterFor(parameter);
				return defaultMarshalInfoWriter.WriteMarshalEmptyVariableFromNative(bodyWriter, defaultMarshalInfoWriter.UndecorateVariable(parameter.NameInGeneratedCode), parameters, metadataAccess);
			}, delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteLine();
			});
		}

		public override string WriteMarshalInputParameter(CppCodeWriter writer, MarshaledParameter parameter, IList<MarshaledParameter> parameters, IRuntimeMetadataAccess metadataAccess)
		{
			return writer.WriteIfNotEmpty<string>(delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteCommentedLine("Marshaling of parameter '{0}' to managed representation", new object[]
				{
					parameter.NameInGeneratedCode
				});
			}, delegate(CppCodeWriter bodyWriter)
			{
				DefaultMarshalInfoWriter defaultMarshalInfoWriter = this.MarshalInfoWriterFor(parameter);
				return defaultMarshalInfoWriter.WriteMarshalVariableFromNative(bodyWriter, defaultMarshalInfoWriter.UndecorateVariable(parameter.NameInGeneratedCode), parameters, false, true, metadataAccess);
			}, delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteLine();
			});
		}

		public override void WriteMarshalOutputParameter(CppCodeWriter writer, string valueName, MarshaledParameter parameter, IList<MarshaledParameter> parameters, IRuntimeMetadataAccess metadataAccess)
		{
			if (!(valueName == parameter.NameInGeneratedCode))
			{
				writer.WriteIfNotEmpty(delegate(CppCodeWriter bodyWriter)
				{
					bodyWriter.WriteCommentedLine("Marshaling of parameter '{0}' back from managed representation", new object[]
					{
						parameter.NameInGeneratedCode
					});
				}, delegate(CppCodeWriter bodyWriter)
				{
					this.MarshalInfoWriterFor(parameter).WriteMarshalOutParameterToNative(bodyWriter, new ManagedMarshalValue(valueName), parameter.NameInGeneratedCode, parameter.Name, parameters, metadataAccess);
				}, delegate(CppCodeWriter bodyWriter)
				{
					bodyWriter.WriteLine();
				});
			}
		}

		public override string WriteMarshalReturnValue(CppCodeWriter writer, MethodReturnType methodReturnType, IList<MarshaledParameter> parameters, IRuntimeMetadataAccess metadataAccess)
		{
			return writer.WriteIfNotEmpty<string>(delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteCommentedLine("Marshaling of return value back from managed representation");
			}, delegate(CppCodeWriter bodyWriter)
			{
				DefaultMarshalInfoWriter defaultMarshalInfoWriter = this.MarshalInfoWriterFor(methodReturnType);
				string variableName = InteropMarshaler.Naming.ForInteropReturnValue();
				string objectVariableName = defaultMarshalInfoWriter.UndecorateVariable(variableName);
				return defaultMarshalInfoWriter.WriteMarshalReturnValueToNative(bodyWriter, new ManagedMarshalValue(objectVariableName), metadataAccess);
			}, delegate(CppCodeWriter bodyWriter)
			{
				bodyWriter.WriteLine();
			});
		}

		public override void WriteMarshalCleanupEmptyParameter(CppCodeWriter writer, string valueName, MarshaledParameter parameter, IRuntimeMetadataAccess metadataAccess)
		{
		}

		public override void WriteMarshalCleanupParameter(CppCodeWriter writer, string valueName, MarshaledParameter parameter, IRuntimeMetadataAccess metadataAccess)
		{
		}

		public override void WriteMarshalCleanupReturnValue(CppCodeWriter writer, MethodReturnType methodReturnType, IRuntimeMetadataAccess metadataAccess)
		{
		}
	}
}
