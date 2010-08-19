using System;

namespace bsn.CommandLine.Context {
	public interface ITagItem: INamedItem {
		bool Optional {
			get;
		}

		string PatternHelp {
			get;
		}

		object ParseValue(string stringValue);

		bool TryGetDefault(out object value);
	}
}