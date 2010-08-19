using System;

namespace bsn.CommandLine.Context {
	[NamedItem("delete", "Deletes a configuration entry from a list of entries.")]
	internal class CollectionDeleteCommand<TExecutionContext>: CollectionCommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public CollectionDeleteCommand(ContextBase<TExecutionContext> owner): base(owner) {}

		protected override CommandBase<TExecutionContext> CreateActionCommand(CollectionBase<TExecutionContext> item) {
			throw new NotImplementedException();
		}
	}
}