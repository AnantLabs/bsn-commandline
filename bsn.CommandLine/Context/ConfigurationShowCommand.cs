using System;

namespace bsn.CommandLine.Context {
	internal class ConfigurationShowCommand: ConfigurationCommandBase<IConfigurationRead> {
		public ConfigurationShowCommand(ContextBase owner): base(owner) {}

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

		protected override CommandBase CreateActionCommand(IConfigurationRead item) {
			throw new NotImplementedException();
		}
	}
}
