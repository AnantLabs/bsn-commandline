using System.Collections.Generic;
using System.Diagnostics;

namespace bsn.CommandLine.Context {
	public class RootContext: ContextBase {
		private readonly string name;

		public RootContext(string name): base(null) {
			Debug.Assert(!string.IsNullOrEmpty(name));
			this.name = name;
		}

		public override IEnumerable<ContextBase> ChildContexts {
			get {
				yield break;
			}
		}

		public override IEnumerable<CollectionBase> Collections {
			get {
				yield break;
			}
		}

		public override IEnumerable<CommandBase> Commands {
			get {
				yield return new ExitCommand(this, "exit");
				yield return new ExitCommand(this, "bye");
				yield return new ExitCommand(this, "quit");
			}
		}

		public override IEnumerable<ConfigurationBase> Configurations {
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
