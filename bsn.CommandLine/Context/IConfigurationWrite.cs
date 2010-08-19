using System;
using System.Collections.Generic;

namespace bsn.CommandLine.Context {
	public interface IConfigurationWrite: IContextItem {
		ICollection<ITagItem> GetParameters();
	}
}
