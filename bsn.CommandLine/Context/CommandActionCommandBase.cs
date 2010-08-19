using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	internal abstract class CommandActionCommandBase<TExecutionContext, TItem>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: INamedItem {
		private readonly CommandBase<TExecutionContext> owner;
		private readonly TItem item;

		public CommandActionCommandBase(CommandBase<TExecutionContext> owner, TItem item): base(owner) {
			this.owner = owner;
			this.item = item;
		}

		public override string Name {
			get {
				return item.Name;
			}
		}

		public override string Description {
			get {
				return item.Description;
			}
		}

		public TItem Item {
			get {
				return item;
			}
		}
	}
}