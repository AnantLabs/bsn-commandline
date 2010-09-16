using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationSetActionCommand<TExecutionContext, TItem>: CommandActionCommandBase<TExecutionContext, TItem> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, IConfigurationWrite<TExecutionContext> {
		public ConfigurationSetActionCommand(CommandBase<TExecutionContext> owner, TItem item): base(owner, item) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.SetConfiguration(executionContext, tags);
		}

		public override IEnumerable<ITagItem<TExecutionContext>> GetCommandTags() {
			return Item.GetWriteParameters();
		}
	}
}