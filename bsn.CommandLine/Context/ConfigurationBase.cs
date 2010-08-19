using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public abstract class ConfigurationBase<TExecutionContext>: IContextItem<TExecutionContext> where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		public abstract string Name {
			get;
		}

		public abstract string Description {
			get;
		}

		public abstract void WriteCommandHelp(TextWriter writer);

		IEnumerable<CommandBase<TExecutionContext>> IContextItem<TExecutionContext>.GetAvailableCommands() {
			yield break;
		}
	}
}