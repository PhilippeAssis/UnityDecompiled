using Mono.Cecil;
using System;

namespace Unity.IL2CPP
{
	public class ErrorTypeAndMethod
	{
		public const string NameOfErrorType = "_ErrorTypeForTestingOnly_";

		public const string NameOfErrorMethod = "_ErrorMethodForTestingOnly_";

		public static void ThrowIfIsErrorType(TypeDefinition typeDefinition)
		{
			ErrorTypeAndMethod.VerifyRequiredCodeGenOptions();
			if (typeDefinition.Name == "_ErrorTypeForTestingOnly_")
			{
				throw new NotSupportedException(ErrorTypeAndMethod.FormatExceptionMessage("type", "_ErrorTypeForTestingOnly_"));
			}
		}

		public static void ThrowIfIsErrorMethod(MethodReference method)
		{
			ErrorTypeAndMethod.VerifyRequiredCodeGenOptions();
			if (method.Name == "_ErrorMethodForTestingOnly_")
			{
				throw new NotSupportedException(ErrorTypeAndMethod.FormatExceptionMessage("method", "_ErrorTypeForTestingOnly_"));
			}
		}

		private static string FormatExceptionMessage(string typeOrMethod, string name)
		{
			return string.Format("The managed {0} {1} always causes this exception when (--enable-error-message-test) is passed to il2cpp.exe. This exception is used only to test the il2cpp error reporting code.", typeOrMethod, name);
		}

		private static void VerifyRequiredCodeGenOptions()
		{
			if (!CodeGenOptions.EnableErrorMessageTest)
			{
				throw new InvalidOperationException("This method should only be called when the EnableErrorMessageTest command line option for il2cpp is set.");
			}
		}
	}
}
