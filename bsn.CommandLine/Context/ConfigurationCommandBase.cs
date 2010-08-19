namespace bsn.CommandLine.Context {
	internal abstract class ConfigurationCommandBase<T>: ActionCommandBase<T> where T: class, IContextItem {
		protected ConfigurationCommandBase(ContextBase owner): base(owner) {}

		protected override System.Collections.Generic.IEnumerable<T> AvailableItems() {
			foreach (ConfigurationBase configurationItem in ParentContext.Configurations) {
				T item = configurationItem as T;
				if (item != null) {
					yield return item;
				}
			}
		}
	}
}
