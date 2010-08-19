using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ParentContextCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ParentContextCommand(ContextBase<TExecutionContext> parentContext): base(parentContext) {}

		public override string Description {
			get {
				return "Goes up one context level.";
			}
		}

		public override string Name {
			get {
				return "..";
			}
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			executionContext.Context = ParentContext.ParentContext;
		}
	}
}