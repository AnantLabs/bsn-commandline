using System;
using System.Diagnostics;

namespace bsn.CommandLine.Context {
	public class CommandHelpCommand: CommandBase {
		private readonly string name;
		private readonly CommandBase owner;

		public CommandHelpCommand(CommandBase owner, string name): base(owner.ParentContext) {
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

		public override void Execute(IExecutionContext executionContext) {
			owner.WriteCommandHelp(executionContext.Output);
		}
	}
}
