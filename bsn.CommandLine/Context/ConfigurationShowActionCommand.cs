using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationShowActionCommand<TExecutionContext>: CommandActionCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationShowActionCommand(CommandBase<TExecutionContext> owner, IConfigurationRead<TExecutionContext> item): base(owner, item) {}

		public override IEnumerable<ITagItem> GetCommandTags() {
			return Item.GetReadParameters();
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.ShowConfiguration(executionContext, tags);
		}
	}
}