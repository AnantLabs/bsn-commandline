using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public interface IContextItem<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		IEnumerable<CommandBase<TExecutionContext>> GetAvailableCommands();

		void WriteCommandHelp(TextWriter writer);
	}
}