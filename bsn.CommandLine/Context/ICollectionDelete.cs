using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface ICollectionDelete<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		void Delete(TExecutionContext executionContext, IDictionary<string, object> filter);
		IEnumerable<ITagItem<TExecutionContext>> GetFilters();
	}
}