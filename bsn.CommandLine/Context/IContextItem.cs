using System;
using System.Collections.Generic;
using System.IO;

namespace bsn.CommandLine.Context {
	public interface IContextItem {
		string Description {
			get;
		}

		string Name {
			get;
		}

		IEnumerable<CommandBase> AvailableCommands();

		void WriteCommandHelp(TextWriter writer);
	}
}
