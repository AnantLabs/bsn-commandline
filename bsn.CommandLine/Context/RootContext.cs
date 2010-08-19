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

		public override IEnumerable<ContextBase<TExecutionContext>> ChildContexts {
			get {
				yield break;
			}
		}

		public override IEnumerable<CollectionBase<TExecutionContext>> Collections {
			get {
				yield break;
			}
		}

		public override IEnumerable<CommandBase<TExecutionContext>> Commands {
			get {
				yield return new ExitCommand<TExecutionContext>(this, "exit");
				yield return new ExitCommand<TExecutionContext>(this, "bye");
				yield return new ExitCommand<TExecutionContext>(this, "quit");
			}
		}

		public override IEnumerable<ConfigurationBase<TExecutionContext>> Configurations {
			get {
				yield break;
			}
		}

		public override string Description {
			get {
				return "The root context.";
			}
		}

		public override string Name {
			get {
				return name;
			}
		}
	}
}