using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ContextHelpCommand: CommandBase {
		private readonly string name;

		public ContextHelpCommand(ContextBase parentContext, string name): base(parentContext) {
			this.name = name;
		}

		public override string Description {
			get {
				return "Displays a list of commands.";
			}
		}

		public override string Name {
			get {
				return name;
			}
		}

		public override IEnumerable<CommandBase> AvailableCommands() {
			foreach (CommandBase command in ParentContext.AvailableCommands()) {
				yield return new CommandHelpCommand(command, command.Name);
			}
		}

		public override void Execute(IExecutionContext executionContext) {
			ParentContext.WriteCommandHelp(executionContext.Output);
		}
	}
}
