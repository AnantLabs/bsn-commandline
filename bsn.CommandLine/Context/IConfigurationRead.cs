using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface IConfigurationRead<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		IEnumerable<ITagItem> GetReadParameters();

		void WriteConfiguration(TExecutionContext executionContext, IDictionary<string, object> parameters);
	}
}