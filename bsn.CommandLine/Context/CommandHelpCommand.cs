using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace bsn.CommandLine.Context {
	public class CommandHelpCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;
		private readonly CommandBase<TExecutionContext> owner;

		public CommandHelpCommand(CommandBase<TExecutionContext> owner, string name): base(owner.ParentContext) {
			Debug.Assert(!String.IsNullOrEmpty(name));
			this.owner = owner;
			this.name = name;
		}

		public override string Description {
			get {
				return "Displays command help.";
			}
		}

		public override string Name {
			get {
				return name;
			}
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			owner.WriteCommandHelp(executionContext.Output);
		}
	}
}