using System;

namespace bsn.CommandLine.Context {
	[NamedItem("show", "Displays configuration information.")]
	internal class ConfigurationShowCommand<TExecutionContext>: ConfigurationCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationShowCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandActionCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> CreateActionCommand(IConfigurationRead<TExecutionContext> item) {
			return new ConfigurationShowActionCommand<TExecutionContext, IConfigurationRead<TExecutionContext>>(this, item);
		}
	}
}