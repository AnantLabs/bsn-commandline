using System;

namespace bsn.CommandLine.Context {
	internal class ConfigurationSetCommand: ConfigurationCommandBase<IConfigurationWrite> {
		public ConfigurationSetCommand(ContextBase owner): base(owner) {}

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

		protected override CommandBase CreateActionCommand(IConfigurationWrite item) {
			throw new NotImplementedException();
		}
	}
}
