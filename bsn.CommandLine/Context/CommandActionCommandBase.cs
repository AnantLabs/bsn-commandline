using System;

namespace bsn.CommandLine.Context {
	internal abstract class CommandActionCommandBase<TExecutionContext, TItem>: CommandBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> where TItem: class, INamedItem {
		private readonly TItem item;

		public CommandActionCommandBase(CommandBase<TExecutionContext> owner, TItem item): base(owner) {
			this.item = item;
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

		public override string Name {
			get {
				return item.Name;
			}
		}
	}
}