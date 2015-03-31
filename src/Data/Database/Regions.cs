﻿// Copyright (c) Aura development team - Licensed under GNU GPL
// For more information, see license file in the main folder

using System;
using System.Linq;

namespace Aura.Data.Database
{
	[Serializable]
	public class RegionData
	{
		public int Id { get; set; }
		public string Name { get; set; }
	}

	/// <summary>
	/// Indexed by region name.
	/// </summary>
	public class RegionDb : DatabaseCsvIndexed<string, RegionData>
	{
		public RegionData Find(uint id)
		{
			return this.Entries.FirstOrDefault(a => a.Value.Id == id).Value;
		}

		public int TryGetRegionId(string region, int fallBack = 0)
		{
			int regionId = fallBack;
			if (!int.TryParse(region, out regionId))
			{
				var mapInfo = this.Find(region);
				if (mapInfo != null)
					regionId = mapInfo.Id;
			}

			return regionId;
		}

		[MinFieldCount(2)]
		protected override void ReadEntry(CsvEntry entry)
		{
			var info = new RegionData();
			info.Id = entry.ReadInt();
			info.Name = entry.ReadString();

			this.Entries[info.Name] = info;
		}
	}
}
