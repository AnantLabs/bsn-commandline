using System;

namespace bsn.CommandLine.Context {
	[NamedItem("set", "Updates configuration settings.")]
	internal class ConfigurationSetCommand<TExecutionContext>: ConfigurationCommandBase<TExecutionContext, IConfigurationWrite<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationSetCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandBase<TExecutionContext> CreateActionCommand(IConfigurationWrite<TExecutionContext> item) {
			return new ConfigurationSetActionCommand<TExecutionContext>(this, item);
		}
	}
}