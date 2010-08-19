using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ActionCommandBase<TExecutionContext, TItem>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: INamedItem {
		protected ActionCommandBase(CommandBase<TExecutionContext> owner): base(owner) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			WriteItemHelp(executionContext.Output);
		}

		public override IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands() {
			foreach (TItem item in GetAvailableItems()) {
				yield return CreateActionCommand(item);
			}
		}

		protected abstract CommandBase<TExecutionContext> CreateActionCommand(TItem item);
		protected abstract IEnumerable<TItem> GetAvailableItems();
	}
}