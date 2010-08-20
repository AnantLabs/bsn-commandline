using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface ICollectionAdd<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		void Add(TExecutionContext executionContext, IDictionary<string, object> columns);
		IEnumerable<ITagItem<TExecutionContext>> GetAddColumns();
	}
}