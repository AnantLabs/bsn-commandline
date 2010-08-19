using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ConfigurationCommandBase<TExecutionContext, TItem>: ActionCommandBase<TExecutionContext, TItem> where TItem: class, INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		protected ConfigurationCommandBase(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override IEnumerable<ContextItem<TExecutionContext>> GetAvailableItems() {
			foreach (ConfigurationBase<TExecutionContext> configurationItem in ParentContext.GetAvailable<ConfigurationBase<TExecutionContext>>()) {
				yield return configurationItem;
			}
			foreach (CollectionBase<TExecutionContext> collectionItem in ParentContext.GetAvailable<CollectionBase<TExecutionContext>>()) {
				yield return collectionItem;
			}
		}
	}
}