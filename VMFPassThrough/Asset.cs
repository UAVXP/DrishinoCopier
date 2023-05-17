using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VMFPassThrough
{
	enum AssetType
	{
		Undefined = -1,
		Material = 0,
		Model,
		Sound
	};

	class Asset // : IEquatable<Asset>
	{
		public AssetType Type { get; set; }
		public string Content { get; set; }
		public string DisplayName { get; set; } // Unused

		public Asset(AssetType type, string content, string displayname)
		{
			this.Type = type;
			this.Content = content;
			this.DisplayName = displayname;
		}
	}
}
