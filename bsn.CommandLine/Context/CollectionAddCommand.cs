using System;

namespace bsn.CommandLine.Context {
	[NamedItem("add", "Adds a configuration entry to a list of entries.")]
	internal class CollectionAddCommand<TExecutionContext>: CollectionCommandBase<TExecutionContext, ICollectionAdd<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public CollectionAddCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandActionCommandBase<TExecutionContext, ICollectionAdd<TExecutionContext>> CreateActionCommand(ICollectionAdd<TExecutionContext> item) {
			return new CollectionAddActionCommand<TExecutionContext, ICollectionAdd<TExecutionContext>>(this, item);
		}
	}
}