using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ConfigurationCommandBase<TExecutionContext, TItem>: ActionCommandBase<TExecutionContext, TItem> where TItem: class, IContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		protected ConfigurationCommandBase(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override IEnumerable<TItem> GetAvailableItems() {
			foreach (ConfigurationBase<TExecutionContext> configurationItem in ParentContext.GetAvailable<ConfigurationBase<TExecutionContext>>()) {
				TItem item = configurationItem as TItem;
				if (item != null) {
					yield return item;
				}
			}
		}
	}
}