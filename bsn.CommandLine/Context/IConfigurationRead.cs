using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface IConfigurationRead<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		IEnumerable<ITagItem> GetReadParameters();

		void ShowConfiguration(TExecutionContext executionContext, IDictionary<string, object> parameters);
	}
}