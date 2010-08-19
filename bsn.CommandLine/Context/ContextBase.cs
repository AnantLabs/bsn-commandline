using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public abstract class ContextBase: CommandBase, INamedItemContainer<CollectionBase>, INamedItemContainer<CommandBase>, INamedItemContainer<ConfigurationBase>, INamedItemContainer<ContextBase> {
		public ICollection<T> GetAvailable<T>() where T: INamedItem {
			SortedDictionary<string, T> items = new SortedDictionary<string, T>(StringComparer.OrdinalIgnoreCase);
			for (ContextBase context = this; context != null; context = context.ParentContext) {
				INamedItemContainer<T> container = context as INamedItemContainer<T>;
				if (container != null) {
					foreach (T item in container.GetItems()) {
						if (!items.ContainsKey(item.Name)) {
							items.Add(item.Name, item);
						}
					}
				}
			}
			return items.Values;
		}

		protected ContextBase(ContextBase parentContext): base(parentContext) {}

		IEnumerable<CollectionBase> INamedItemContainer<CollectionBase>.GetItems() {
			return Collections;
		}

		IEnumerable<CommandBase> INamedItemContainer<CommandBase>.GetItems() {
			return GetContextCommands();
		}

		IEnumerable<ConfigurationBase> INamedItemContainer<ConfigurationBase>.GetItems() {
			return Configurations;
		}

		IEnumerable<ContextBase> INamedItemContainer<ContextBase>.GetItems() {
			return ChildContexts;
		}

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

		public sealed override IEnumerable<CommandBase> GetAvailableCommands() {
			SortedDictionary<string, CommandBase> localCommands = new SortedDictionary<string, CommandBase>(StringComparer.OrdinalIgnoreCase);
			foreach (CommandBase command in GetContextCommands()) {
				localCommands.Add(command.Name, command);
			}
			if (ParentContext != null) {
				foreach (CommandBase command in ParentContext.GetAvailableCommands()) {
					if (!localCommands.ContainsKey(command.Name)) {
						yield return command;
					}
				}
			}
			foreach (CommandBase command in localCommands.Values) {
				yield return command;
			}
		}

		public override sealed void Execute(IExecutionContext executionContext, IDictionary<string, object> tags) {
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
			foreach (CommandBase command in GetAvailableCommands()) {
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

		private IEnumerable<CommandBase> GetContextCommands() {
			yield return new CollectionAddCommand(this);
			yield return new CollectionDeleteCommand(this);
			yield return new ConfigurationShowCommand(this);
			yield return new ConfigurationSetCommand(this);
			yield return new ContextHelpCommand(this, "?");
			yield return new ContextHelpCommand(this, "help");
			if (ParentContext != null) {
				yield return new ParentContextCommand(this);
			}
			foreach (CommandBase command in Commands) {
				yield return command;
			}
			foreach (ContextBase context in ChildContexts) {
				yield return context;
			}
		}
	}
}
