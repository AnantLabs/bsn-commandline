using System;

namespace bsn.CommandLine.Context {
	internal class CollectionDeleteCommand<TExecutionContext>: CollectionCommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public CollectionDeleteCommand(ContextBase<TExecutionContext> owner): base(owner) {}

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

		protected override CommandBase<TExecutionContext> CreateActionCommand(CollectionBase<TExecutionContext> item) {
			throw new NotImplementedException();
		}
	}
}