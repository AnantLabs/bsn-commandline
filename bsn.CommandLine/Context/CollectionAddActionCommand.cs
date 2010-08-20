using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class CollectionAddActionCommand<TExecutionContext, TItem>: CommandActionCommandBase<TExecutionContext, TItem> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, ICollectionAdd<TExecutionContext> {
		public CollectionAddActionCommand(CommandBase<TExecutionContext> owner, TItem item): base(owner, item) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.Add(executionContext, tags);
		}

		public override IEnumerable<ITagItem<TExecutionContext>> GetCommandTags() {
			return Item.GetAddColumns();
		}
	}
}