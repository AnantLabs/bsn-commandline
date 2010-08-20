using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	[NamedItem("?", "Displays a list of the available commands.")]
	internal class ContextHelpCommand<TExecutionContext>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;

		public ContextHelpCommand(ContextBase<TExecutionContext> owner, string name): base(owner) {
			this.name = name;
		}

		public override string Name {
			get {
				return name;
			}
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			object commandName;
			if (tags.TryGetValue("command", out commandName)) {
				bool commandFound = false;
				foreach (CommandBase<TExecutionContext> command in Filter(ParentContext.GetAvailable<CommandBase<TExecutionContext>>(), (string)commandName)) {
					command.WriteItemHelp(executionContext.Output);
					commandFound = true;
				}
				if (!commandFound) {
					executionContext.Output.WriteLine("Unknown command: {0}", commandName);
				}
			} else {
				ParentContext.WriteItemHelp(executionContext.Output);
			}
		}

		public override IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			yield break;
		}

		public override IEnumerable<ITagItem<TExecutionContext>> GetCommandTags() {
			yield return new Tag<TExecutionContext, string>("command", "The command name to get help for.").SetOptional(context => true);
		}
	}
}