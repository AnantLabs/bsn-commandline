using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface IConfigurationWrite<TExecutionContext>: IContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		IEnumerable<Tag<string>> GetParameters();

		void SetConfiguration(TExecutionContext executionContext, IDictionary<string, object> parameters);
	}
}