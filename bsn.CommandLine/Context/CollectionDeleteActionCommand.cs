using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class CollectionDeleteActionCommand<TExecutionContext, TItem>: CommandActionCommandBase<TExecutionContext, TItem> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, ICollectionDelete<TExecutionContext> {
		public CollectionDeleteActionCommand(CommandBase<TExecutionContext> owner, TItem item): base(owner, item) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.Delete(executionContext, tags);
		}

		public override IEnumerable<ITagItem> GetCommandTags() {
			return Item.GetFilters();
		}
	}
}