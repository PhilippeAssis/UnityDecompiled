using Mono.Cecil;
using System;
using System.Collections.Generic;

namespace Unity.IL2CPP.Marshaling.MarshalInfoWriters
{
	internal sealed class DelegateMarshalInfoWriter : MarshalableMarshalInfoWriter
	{
		public override string MarshaledTypeName
		{
			get
			{
				return "Il2CppMethodPointer";
			}
		}

		public DelegateMarshalInfoWriter(TypeReference type) : base(type)
		{
		}

		public override void WriteMarshalVariableToNative(CppCodeWriter writer, ManagedMarshalValue sourceVariable, string destinationVariable, string managedVariableName, IRuntimeMetadataAccess metadataAccess)
		{
			writer.WriteLine("{0} = il2cpp_codegen_marshal_delegate(reinterpret_cast<Il2CppCodeGenMulticastDelegate*>({1}));", new object[]
			{
				destinationVariable,
				sourceVariable.Load()
			});
		}

		public override void WriteMarshalVariableFromNative(CppCodeWriter writer, string variableName, ManagedMarshalValue destinationVariable, IList<MarshaledParameter> methodParameters, bool returnValue, bool forNativeWrapperOfManagedMethod, IRuntimeMetadataAccess metadataAccess)
		{
			writer.WriteLine(destinationVariable.Store("il2cpp_codegen_marshal_function_ptr_to_delegate<{0}>({1}, {2})", new object[]
			{
				DefaultMarshalInfoWriter.Naming.ForType(this._typeRef),
				variableName,
				metadataAccess.TypeInfoFor(this._typeRef)
			}));
		}

		public override void WriteMarshalCleanupVariable(CppCodeWriter writer, string variableName, IRuntimeMetadataAccess metadataAccess, string managedVariableName = null)
		{
		}

		public override void WriteNativeVariableDeclarationOfType(CppCodeWriter writer, string variableName)
		{
			writer.WriteLine("{0} {1} = NULL;", new object[]
			{
				this.MarshaledDecoratedTypeName,
				variableName
			});
		}

		public override void WriteMarshaledTypeForwardDeclaration(CppCodeWriter writer)
		{
		}
	}
}
