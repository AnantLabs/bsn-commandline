using System;

namespace bsn.CommandLine.Context {
	public interface ITagItem<TExecutionContext>: INamedItem where TExecutionContext: class, IExecutionContext<TExecutionContext> {
		bool GetOptional(TExecutionContext executionContext);

		string PatternHelp {
			get;
		}

		object ParseValue(TExecutionContext executionContext, string stringValue);

		bool TryGetDefault(TExecutionContext executionContext, out object value);
	}
}