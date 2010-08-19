using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ParentContextCommand: CommandBase {
		public ParentContextCommand(ContextBase parentContext): base(parentContext) {}

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

		public override void Execute(IExecutionContext executionContext, IDictionary<string, object> tags) {
			executionContext.Context = ParentContext.ParentContext;
		}
	}
}
