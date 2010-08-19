using System;
using System.Collections.Generic;
using System.Text;

namespace bsn.CommandLine.Context {
	public interface ITagItem: INamedItem {
		string PatternHelp {
			get;
		}

		bool Optional {
			get;
		}

		object ParseValue(string value);
	}
}
