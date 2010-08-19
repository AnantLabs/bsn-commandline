using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class ActionCommandBase<T>: CommandBase where T: class, IContextItem {
		protected ActionCommandBase(ContextBase owner): base(owner) {}

		public override System.Collections.Generic.IEnumerable<CommandBase> AvailableCommands() {
			foreach (T item in AvailableItems()) {
				yield return CreateActionCommand(item);
			}
		}

		protected abstract IEnumerable<T> AvailableItems();

		protected abstract CommandBase CreateActionCommand(T item);
	}
}
