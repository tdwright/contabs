# contabs
Simple yet flexible tables for console apps.

[![Build status](https://ci.appveyor.com/api/projects/status/vv7d29f86a534tw2/branch/master?svg=true)](https://ci.appveyor.com/project/tdwright/contabs/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/tdwright/contabs/badge.svg?branch=master)](https://coveralls.io/github/tdwright/contabs?branch=master)
[![NuGet](https://img.shields.io/nuget/dt/ConTabs.tdwright.svg?style=plastic)](https://www.nuget.org/packages/ConTabs.tdwright/)

[![Waffle.io - Columns and their card count](https://badge.waffle.io/tdwright/contabs.svg?columns=all)](https://waffle.io/tdwright/contabs)

## Installation

ConTabs is on NuGet, so installation is easy using your package manager of choice...

    Install-Package ConTabs.tdwright

Or

    dotnet add package ConTabs.tdwright

## Usage

You can make a table in just two lines:

    var tab = Table<Sample>.Create(list);
    Console.WriteLine(tab.ToString());

By default, ConTabs will create a column for each public property on the type that you pass.

Here's a more full example that also demonstrates how to select an alternative style:

	class Sample
	{
		public int Number { get; set; }
		public string Word { get; set; }
	}

	static void Main(string[] args)
	{
		var list = new List<Sample>
		{
			new Sample{Number=1, Word="Spider"},
			new Sample{Number=2, Word="Monkey"}
		};

		var tab = Table<Sample>.Create(list);
		tab.TableStyle = Style.Heavy;

		Console.WriteLine(tab.ToString());

		Console.ReadKey();
	}

This renders like this:

![A table in a console window](RepoAssets/Example-Basic-Heavy.PNG?raw=true)

## Contributing

If you're interested in contributing, we'd love to welcome you. Check out our [contributing guidelines](CONTRIBUTING.md) which should contain everything you need to make a successful contribution.


## Background

ConTabs started life as an excuse for me (tdwright) to explore some bits and pieces. [I am writing up my experiences on my blog.](http://blog.tdwright.co.uk/series/modern-dotnet-dev-contabs/)
