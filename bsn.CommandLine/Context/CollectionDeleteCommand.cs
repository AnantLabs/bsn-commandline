using System;

namespace bsn.CommandLine.Context {
	[NamedItem("delete", "Deletes a configuration entry from a list of entries.")]
	internal class CollectionDeleteCommand<TExecutionContext>: CollectionCommandBase<TExecutionContext, ICollectionDelete<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public CollectionDeleteCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandActionCommandBase<TExecutionContext, ICollectionDelete<TExecutionContext>> CreateActionCommand(ICollectionDelete<TExecutionContext> item) {
			return new CollectionDeleteActionCommand<TExecutionContext, ICollectionDelete<TExecutionContext>>(this, item);
		}
	}
}