using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class CollectionBase<TExecutionContext>: ContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
	}
}