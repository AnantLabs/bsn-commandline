using System;

namespace bsn.CommandLine.Context {
	internal class CollectionAddCommand: CollectionCommandBase {
		public CollectionAddCommand(ContextBase owner): base(owner) {}

		public override string Description {
			get {
				return "Adds a configuration entry to a list of entries.";
			}
		}

		public override string Name {
			get {
				return "add";
			}
		}

		protected override CommandBase CreateActionCommand(CollectionBase item) {
			throw new NotImplementedException();
		}
	}
}
