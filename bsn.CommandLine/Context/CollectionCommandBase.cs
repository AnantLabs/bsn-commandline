namespace bsn.CommandLine.Context {
	internal abstract class CollectionCommandBase: ActionCommandBase<CollectionBase> {
		protected CollectionCommandBase(ContextBase owner): base(owner) {}

		protected override System.Collections.Generic.IEnumerable<CollectionBase> AvailableItems() {
			return ParentContext.Collections;
		}
	}
}
