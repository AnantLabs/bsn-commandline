using System;
using System.Collections;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public class ContextCommandList: IEnumerable<CommandBase> {
		private readonly ContextCommandList child;
		private readonly SortedDictionary<string, CommandBase> commands = new SortedDictionary<string, CommandBase>(StringComparer.OrdinalIgnoreCase);
		private readonly ContextCommandList parent;

		public ContextCommandList(ContextBase context): this(context, null) {}

		private ContextCommandList(ContextBase context, ContextCommandList child) {
			if (context == null) {
				throw new ArgumentNullException("context");
			}
			this.child = child;
			foreach (CommandBase command in context.GetAvailableCommands()) {
				if (!IsNameUsedByChild(command.Name)) {
					commands.Add(command.Name, command);
				}
			}
			if (context.ParentContext != null) {
				parent = new ContextCommandList(context.ParentContext, this);
			}
		}

		public bool IsTop {
			get {
				return child == null;
			}
		}

		public ContextCommandList Parent {
			get {
				return parent;
			}
		}

		private bool IsNameUsedByChild(string name) {
			return (child != null) && (child.commands.ContainsKey(name) || child.IsNameUsedByChild(name));
		}

		public IEnumerator<CommandBase> GetEnumerator() {
			throw new NotImplementedException();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}
