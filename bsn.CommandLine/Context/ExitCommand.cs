using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public class ExitCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;

		public ExitCommand(RootContext<TExecutionContext> rootContext, string name): base(rootContext) {
			this.name = name;
		}

		public override string Description {
			get {
				return "Exits the program.";
			}
		}

		public override string Name {
			get {
				return name;
			}
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			executionContext.Context = null;
		}
	}
}