using System;

namespace bsn.CommandLine.Context {
	public abstract class ConfigurationBase<TExecutionContext>: ContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {}
}