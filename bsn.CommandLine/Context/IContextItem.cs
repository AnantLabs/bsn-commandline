using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public interface IContextItem: INamedItem {
		IEnumerable<CommandBase> GetAvailableCommands();

		void WriteCommandHelp(TextWriter writer);
	}
}
