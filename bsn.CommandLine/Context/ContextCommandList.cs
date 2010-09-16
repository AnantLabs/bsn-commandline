using System;
using System.Collections;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public class ContextCommandList<TExecutionContext>: IEnumerable<CommandBase<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly ContextCommandList<TExecutionContext> child;
		private readonly SortedDictionary<string, CommandBase<TExecutionContext>> commands = new SortedDictionary<string, CommandBase<TExecutionContext>>(StringComparer.OrdinalIgnoreCase);
		private readonly ContextCommandList<TExecutionContext> parent;

		public ContextCommandList(ContextBase<TExecutionContext> context): this(context, null) {}

		private ContextCommandList(ContextBase<TExecutionContext> context, ContextCommandList<TExecutionContext> child) {
			if (context == null) {
				throw new ArgumentNullException("context");
			}
			this.child = child;
			foreach (CommandBase<TExecutionContext> command in context.GetAvailableCommands()) {
				if (!IsNameUsedByChild(command.Name)) {
					commands.Add(command.Name, command);
				}
			}
			if (context.ParentContext != null) {
				parent = new ContextCommandList<TExecutionContext>(context.ParentContext, this);
			}
		}

		public bool IsTop {
			get {
				return child == null;
			}
		}

		public ContextCommandList<TExecutionContext> Parent {
			get {
				return parent;
			}
		}

		private bool IsNameUsedByChild(string name) {
			return (child != null) && (child.commands.ContainsKey(name) || child.IsNameUsedByChild(name));
		}

		public IEnumerator<CommandBase<TExecutionContext>> GetEnumerator() {
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}