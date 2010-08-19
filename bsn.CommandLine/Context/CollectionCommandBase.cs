using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class CollectionCommandBase<TExecutionContext, TItem>: ActionCommandBase<TExecutionContext, TItem> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, INamedItem {
		protected CollectionCommandBase(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override IEnumerable<ContextItem<TExecutionContext>> GetAvailableItems() {
			foreach (CollectionBase<TExecutionContext> collectionItem in ParentContext.GetAvailable<CollectionBase<TExecutionContext>>()) {
				yield return collectionItem;
			}
		}
	}
}