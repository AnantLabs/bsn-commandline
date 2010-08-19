using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class CollectionBase: IContextItem {
		public abstract string Name {
			get;
		}

		public abstract string Description {
			get;
		}

		public abstract void WriteCommandHelp(TextWriter writer);

		IEnumerable<CommandBase> IContextItem.GetAvailableCommands() {
			yield break;
		}
	}
}
