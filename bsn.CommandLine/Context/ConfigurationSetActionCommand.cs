using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationSetActionCommand<TExecutionContext>: CommandActionCommandBase<TExecutionContext, IConfigurationWrite<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationSetActionCommand(ContextBase<TExecutionContext> parentContext, IConfigurationWrite<TExecutionContext> item): base(parentContext, item) {}

		public override IEnumerable<ITagItem> GetCommandTags() {
			return Item.GetWriteParameters();
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.SetConfiguration(executionContext, tags);
		}
	}
}