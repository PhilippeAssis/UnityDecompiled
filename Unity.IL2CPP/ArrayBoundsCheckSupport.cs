using Mono.Cecil;
using System;
using Unity.IL2CPP.IoC;
using Unity.IL2CPP.IoCServices;

namespace Unity.IL2CPP
{
	public class ArrayBoundsCheckSupport
	{
		private readonly MethodDefinition _methodDefinition;

		private readonly bool _arrayBoundsChecksGloballyEnabled;

		[Inject]
		public static IStatsService StatsService;

		public ArrayBoundsCheckSupport(MethodDefinition methodDefinition, bool arrayBoundsChecksGloballyEnabled)
		{
			this._methodDefinition = methodDefinition;
			this._arrayBoundsChecksGloballyEnabled = arrayBoundsChecksGloballyEnabled;
		}

		public bool ShouldEmitBoundsChecksForMethod()
		{
			return CompilerServicesSupport.HasArrayBoundsChecksSupportEnabled(this._methodDefinition, this._arrayBoundsChecksGloballyEnabled);
		}

		public void RecordArrayBoundsCheckEmitted()
		{
			if (this.ShouldEmitBoundsChecksForMethod())
			{
				ArrayBoundsCheckSupport.StatsService.RecordArrayBoundsCheckEmitted(this._methodDefinition);
			}
		}
	}
}
