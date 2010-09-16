using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	[NamedItem("exit", "Exits the program.")]
	public class ExitCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;

		public ExitCommand(RootContext<TExecutionContext> rootContext, string name): base(rootContext) {
			this.name = name;
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