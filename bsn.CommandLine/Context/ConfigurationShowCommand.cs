using System;

namespace bsn.CommandLine.Context {
	internal class ConfigurationShowCommand<TExecutionContext>: ConfigurationCommandBase<TExecutionContext, IConfigurationRead<TExecutionContext>> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public ConfigurationShowCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		public override string Description {
			get {
				return "Displays information.";
			}
		}

		public override string Name {
			get {
				return "show";
			}
		}

		protected override CommandBase<TExecutionContext> CreateActionCommand(IConfigurationRead<TExecutionContext> item) {
			throw new NotImplementedException();
		}
	}
}