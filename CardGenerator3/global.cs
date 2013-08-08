// Martin Ogg 2013/8/8
// CardGenerator App
// See LICENSE.TXT for licensing of this software
// If you like it, let me know! www.martinogg.com

using System;

// Windows builds dont like POSIX, this is an at a translation GetString stub if POSIX is switched off

namespace global
{
	public static class Mono
	{
		public static class Unix
		{
			public static class Catalog
			{
				public static string GetString(string s)
				{

					return s;
				}
			}
		}
	}
}
