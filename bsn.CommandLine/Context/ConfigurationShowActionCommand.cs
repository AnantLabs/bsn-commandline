using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationShowActionCommand<TExecutionContext>: CommandActionCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationShowActionCommand(ContextBase<TExecutionContext> parentContext, IConfigurationRead<TExecutionContext> item): base(parentContext, item) {}

		public override IEnumerable<ITagItem> GetCommandTags() {
			return Item.GetReadParameters();
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.WriteConfiguration(executionContext, tags);
		}
	}
}