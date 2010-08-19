using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ActionCommandBase<T>: CommandBase where T: class, IContextItem {
		protected ActionCommandBase(ContextBase owner): base(owner) {}

		public override System.Collections.Generic.IEnumerable<CommandBase> GetAvailableCommands() {
			foreach (T item in GetAvailableItems()) {
				yield return CreateActionCommand(item);
			}
		}

		public override void Execute(IExecutionContext executionContext, IDictionary<string, object> tags) {
			WriteCommandHelp(executionContext.Output);
		}

		protected abstract IEnumerable<T> GetAvailableItems();

		protected abstract CommandBase CreateActionCommand(T item);
	}
}
