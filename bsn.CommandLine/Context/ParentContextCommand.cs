using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	[NamedItem("..", "Goes up one context level.")]
	internal class ParentContextCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ParentContextCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			executionContext.Context = ParentContext.ParentContext;
		}
	}
}