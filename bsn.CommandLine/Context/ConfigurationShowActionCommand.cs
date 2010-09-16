using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationShowActionCommand<TExecutionContext, TItem>: CommandActionCommandBase<TExecutionContext, TItem> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, IConfigurationRead<TExecutionContext> {
		public ConfigurationShowActionCommand(CommandBase<TExecutionContext> owner, TItem item): base(owner, item) {}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.ShowConfiguration(executionContext, tags);
		}

		public override IEnumerable<ITagItem<TExecutionContext>> GetCommandTags() {
			return Item.GetReadParameters();
		}
	}
}