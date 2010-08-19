using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface IConfigurationWrite<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		IEnumerable<ITagItem> GetWriteParameters();

		void SetConfiguration(TExecutionContext executionContext, IDictionary<string, object> parameters);
	}
}