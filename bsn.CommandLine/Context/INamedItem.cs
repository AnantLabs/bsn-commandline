using System;

namespace bsn.CommandLine.Context {
	public interface INamedItem {
		string Description {
			get;
		}

		string Name {
			get;
		}
	}
}