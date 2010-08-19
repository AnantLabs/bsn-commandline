using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class ContextBase<TExecutionContext>: CommandBase<TExecutionContext>, INamedItemContainer<CollectionBase<TExecutionContext>>, INamedItemContainer<CommandBase<TExecutionContext>>, INamedItemContainer<ConfigurationBase<TExecutionContext>>,
	                                                      INamedItemContainer<ContextBase<TExecutionContext>>, INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		protected ContextBase(ContextBase<TExecutionContext> parentContext): base(parentContext) {}

		public abstract IEnumerable<ContextBase<TExecutionContext>> ChildContexts {
			get;
		}

		public abstract IEnumerable<CollectionBase<TExecutionContext>> Collections {
			get;
		}

		public abstract IEnumerable<CommandBase<TExecutionContext>> Commands {
			get;
		}

		public abstract IEnumerable<ConfigurationBase<TExecutionContext>> Configurations {
			get;
		}

		public override sealed void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			executionContext.Context = this;
		}

		public RootContext<TExecutionContext> FindRootContext() {
			ContextBase<TExecutionContext> context = this;
			while (context.ParentContext != null) {
				context = context.ParentContext;
			}
			return context as RootContext<TExecutionContext>;
		}

		public ICollection<TItem> GetAvailable<TItem>() where TItem: INamedItem {
			SortedDictionary<string, TItem> items = new SortedDictionary<string, TItem>(StringComparer.OrdinalIgnoreCase);
			for (ContextBase<TExecutionContext> context = this; context != null; context = context.ParentContext) {
				INamedItemContainer<TItem> container = context as INamedItemContainer<TItem>;
				if (container != null) {
					foreach (TItem item in container.GetItems()) {
						if (!items.ContainsKey(item.Name)) {
							items.Add(item.Name, item);
						}
					}
				}
			}
			return items.Values;
		}

		public override sealed IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			SortedDictionary<string, CommandBase<TExecutionContext>> localCommands = new SortedDictionary<string, CommandBase<TExecutionContext>>(StringComparer.OrdinalIgnoreCase);
			foreach (CommandBase<TExecutionContext> command in GetContextCommands()) {
				localCommands.Add(command.Name, command);
			}
			if (ParentContext != null) {
				foreach (CommandBase<TExecutionContext> command in ParentContext.GetAvailableCommands()) {
					if (!localCommands.ContainsKey(command.Name)) {
						yield return command;
					}
				}
			}
			foreach (CommandBase<TExecutionContext> command in localCommands.Values) {
				yield return command;
			}
		}

		public override void WriteItemHelp(TextWriter writer) {
			writer.WriteLine("The following commands are available:");
			ContextBase<TExecutionContext> currentContext = null;
			foreach (CommandBase<TExecutionContext> command in GetAvailableCommands()) {
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

		private IEnumerable<CommandBase<TExecutionContext>> GetContextCommands() {
			yield return new CollectionAddCommand<TExecutionContext>(this);
			yield return new CollectionDeleteCommand<TExecutionContext>(this);
			yield return new ConfigurationShowCommand<TExecutionContext>(this);
			yield return new ConfigurationSetCommand<TExecutionContext>(this);
			yield return new ContextHelpCommand<TExecutionContext>(this, "?");
			yield return new ContextHelpCommand<TExecutionContext>(this, "help");
			if (ParentContext != null) {
				yield return new ParentContextCommand<TExecutionContext>(this);
			}
			foreach (CommandBase<TExecutionContext> command in Commands) {
				yield return command;
			}
			foreach (ContextBase<TExecutionContext> context in ChildContexts) {
				yield return context;
			}
		}

		IEnumerable<CollectionBase<TExecutionContext>> INamedItemContainer<CollectionBase<TExecutionContext>>.GetItems() {
			return Collections;
		}

		IEnumerable<CommandBase<TExecutionContext>> INamedItemContainer<CommandBase<TExecutionContext>>.GetItems() {
			return GetContextCommands();
		}

		IEnumerable<ConfigurationBase<TExecutionContext>> INamedItemContainer<ConfigurationBase<TExecutionContext>>.GetItems() {
			return Configurations;
		}

		IEnumerable<ContextBase<TExecutionContext>> INamedItemContainer<ContextBase<TExecutionContext>>.GetItems() {
			return ChildContexts;
		}
	                                                      }
}