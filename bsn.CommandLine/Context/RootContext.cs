using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace bsn.CommandLine.Context {
	public class RootContext<TExecutionContext>: ContextBase<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		private readonly string name;

		public RootContext(string name): base(null) {
			Debug.Assert(!string.IsNullOrEmpty(name));
			this.name = name;
		}

		public override IEnumerable<CommandBase<TExecutionContext>> Commands {
			get {
				return Merge(base.Commands, new ExitCommand<TExecutionContext>(this, "exit"), new ExitCommand<TExecutionContext>(this, "bye"), new ExitCommand<TExecutionContext>(this, "quit"));
			}
		}

		public override string Description {
			get {
				return "";
			}
		}

		public override string Name {
			get {
				return name;
			}
		}
	}
}