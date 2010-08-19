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

		public override IEnumerable<CommandBase> GetAvailableCommands() {
			yield break;
		}

		public override IEnumerable<ITagItem> GetCommandTags() {
			yield return new Tag<string>("command", "The command name to get help for.", true);
		}

		public override void Execute(IExecutionContext executionContext, IDictionary<string, object> tags) {
			object commandName;
			if (tags.TryGetValue("command", out commandName)) {
				bool commandFound = false;
				foreach (CommandBase command in Filter(ParentContext.GetAvailable<CommandBase>(), (string)commandName)) {
					command.WriteCommandHelp(executionContext.Output);
					commandFound = true;
				}
				if (!commandFound) {
					executionContext.Output.WriteLine("Unknown command: {0}", commandName);
				}
			} else {
				ParentContext.WriteCommandHelp(executionContext.Output);
			}
		}
	}
}
