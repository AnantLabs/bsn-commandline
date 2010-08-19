using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class CollectionCommandBase<TExecutionContext>: ActionCommandBase<TExecutionContext, CollectionBase<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		protected CollectionCommandBase(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override IEnumerable<CollectionBase<TExecutionContext>> GetAvailableItems() {
			return ParentContext.GetAvailable<CollectionBase<TExecutionContext>>();
		}
	}
}