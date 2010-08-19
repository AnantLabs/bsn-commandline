using System;

namespace bsn.CommandLine.Context {
	public interface IConfigurationRead<TExecutionContext>: IContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {}
}