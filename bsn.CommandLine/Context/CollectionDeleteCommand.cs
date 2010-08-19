using System;

namespace bsn.CommandLine.Context {
	internal class CollectionDeleteCommand: CollectionCommandBase {
		public CollectionDeleteCommand(ContextBase owner): base(owner) {}

		public override string Description {
			get {
				return "Deletes a configuration entry from a list of entries.";
			}
		}

		public override string Name {
			get {
				return "delete";
			}
		}

		protected override CommandBase CreateActionCommand(CollectionBase item) {
			throw new NotImplementedException();
		}
	}
}
