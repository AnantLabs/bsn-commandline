using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ActionCommandBase<TExecutionContext, TItem>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, INamedItem {
		protected ActionCommandBase(CommandBase<TExecutionContext> owner): base(owner) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			WriteItemHelp(executionContext.Output, executionContext);
		}

		public override IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			foreach (ContextItem<TExecutionContext> item in GetAvailableItems()) {
				TItem filtered = item as TItem;
				if (filtered != null) {
					yield return CreateActionCommand(filtered);
				}
			}
		}

		protected abstract CommandActionCommandBase<TExecutionContext, TItem> CreateActionCommand(TItem item);
		protected abstract IEnumerable<ContextItem<TExecutionContext>> GetAvailableItems();
	}
}