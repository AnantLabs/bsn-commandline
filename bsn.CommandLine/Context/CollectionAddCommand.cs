using System;

namespace bsn.CommandLine.Context {
	internal class CollectionAddCommand<TExecutionContext>: CollectionCommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public CollectionAddCommand(ContextBase<TExecutionContext> owner): base(owner) {}

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

		protected override CommandBase<TExecutionContext> CreateActionCommand(CollectionBase<TExecutionContext> item) {
			throw new NotImplementedException();
		}
	}
}