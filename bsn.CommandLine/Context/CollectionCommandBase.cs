using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class CollectionCommandBase: ActionCommandBase<CollectionBase> {
		protected CollectionCommandBase(ContextBase owner): base(owner) {}
		
		protected override IEnumerable<CollectionBase> GetAvailableItems() {
			return ParentContext.GetAvailable<CollectionBase>();
		}
	}
}
