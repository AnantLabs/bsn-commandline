using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public abstract class ContextBase: CommandBase {
		protected ContextBase(ContextBase parentContext): base(parentContext) {}

		public abstract IEnumerable<ContextBase> ChildContexts {
			get;
		}

		public abstract IEnumerable<CollectionBase> Collections {
			get;
		}

		public abstract IEnumerable<CommandBase> Commands {
			get;
		}

		public abstract IEnumerable<ConfigurationBase> Configurations {
			get;
		}

		public override IEnumerable<CommandBase> AvailableCommands() {
			SortedDictionary<string, CommandBase> localCommands = new SortedDictionary<string, CommandBase>(StringComparer.OrdinalIgnoreCase);
			foreach (CommandBase builtin in BuiltinCommands()) {
				localCommands.Add(builtin.Name, builtin);
			}
			foreach (CommandBase command in Commands) {
				localCommands.Add(command.Name, command);
			}
			foreach (CommandBase context in ChildContexts) {
				localCommands.Add(context.Name, context);
			}
			if (ParentContext != null) {
				foreach (CommandBase command in ParentContext.AvailableCommands()) {
					if (!localCommands.ContainsKey(command.Name)) {
						yield return command;
					}
				}
			}
			foreach (CommandBase command in localCommands.Values) {
				yield return command;
			}
		}

		public override sealed void Execute(IExecutionContext executionContext) {
			executionContext.Context = this;
		}

		public RootContext FindRootContext() {
			ContextBase context = this;
			while (context.ParentContext != null) {
				context = context.ParentContext;
			}
			return context as RootContext;
		}

		public override void WriteCommandHelp(System.IO.TextWriter writer) {
			writer.WriteLine("The following commands are available:");
			ContextBase currentContext = null;
			foreach (CommandBase command in AvailableCommands()) {
				if (command.ParentContext != currentContext) {
					currentContext = command.ParentContext;
					writer.WriteLine();
					if (ReferenceEquals(currentContext, this)) {
						writer.WriteLine("Commands in this context:");
					} else {
						writer.WriteLine("Commands inherited from {0} context:", currentContext.Name);
					}
				}
				command.WriteNameLine(writer, null);
			}
		}

		private IEnumerable<CommandBase> BuiltinCommands() {
			yield return new CollectionAddCommand(this);
			yield return new CollectionDeleteCommand(this);
			yield return new ConfigurationShowCommand(this);
			yield return new ConfigurationSetCommand(this);
			yield return new ContextHelpCommand(this, "?");
			yield return new ContextHelpCommand(this, "help");
			if (ParentContext != null) {
				yield return new ParentContextCommand(this);
			}
		}
	}
}
