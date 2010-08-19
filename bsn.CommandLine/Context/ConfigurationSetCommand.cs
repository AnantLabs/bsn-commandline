using System;

namespace bsn.CommandLine.Context {
	internal class ConfigurationSetCommand<TExecutionContext>: ConfigurationCommandBase<TExecutionContext, IConfigurationWrite<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationSetCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		public override string Description {
			get {
				return "Updates configuration settings.";
			}
		}

		public override string Name {
			get {
				return "set";
			}
		}

		protected override CommandBase<TExecutionContext> CreateActionCommand(IConfigurationWrite<TExecutionContext> item) {
			return new ConfigurationSetActionCommand<TExecutionContext>(ParentContext, item);
		}
	}
}