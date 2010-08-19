using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal class ConfigurationSetActionCommand<TExecutionContext>: CommandActionCommandBase<TExecutionContext, IConfigurationWrite<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationSetActionCommand(CommandBase<TExecutionContext> owner, IConfigurationWrite<TExecutionContext> item): base(owner, item) {}

		public override IEnumerable<ITagItem> GetCommandTags() {
			return Item.GetWriteParameters();
		}

		public override void Execute(TExecutionContext executionContext, IDictionary<string, object> tags) {
			Item.SetConfiguration(executionContext, tags);
		}
	}
}