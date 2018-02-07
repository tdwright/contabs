![ConTabs logo](RepoAssets/contabs%20logo.png?raw=true)

***Simple yet flexible tables for console apps.***

[![Build status](https://ci.appveyor.com/api/projects/status/vv7d29f86a534tw2/branch/master?svg=true)](https://ci.appveyor.com/project/tdwright/contabs/branch/master)
[![Coverage Status](https://coveralls.io/repos/github/tdwright/contabs/badge.svg?branch=master)](https://coveralls.io/github/tdwright/contabs?branch=master)
[![NuGet](https://img.shields.io/nuget/dt/ConTabs.tdwright.svg?style=plastic)](https://www.nuget.org/packages/ConTabs.tdwright/)

[![Waffle.io - Columns and their card count](https://badge.waffle.io/tdwright/contabs.svg?columns=all)](https://waffle.io/tdwright/contabs)

## Usage

You can make a table in just two lines:

    var tab = Table<Sample>.Create(list);
    Console.WriteLine(tab.ToString());

![A table in a console window](RepoAssets/Example-Basic-Heavy.PNG?raw=true)

## Documentation

The project Wiki contains the following guides:

1. [Getting started](https://github.com/tdwright/contabs/wiki/1.-Getting-started-with-ConTabs)
2. [Using styles](https://github.com/tdwright/contabs/wiki/2.-Styling-your-table)
3. [Hiding, naming, and ordering columns](https://github.com/tdwright/contabs/wiki/3.-Working-with-columns)
4. [Format strings and alignment](https://github.com/tdwright/contabs/wiki/4.-Format-strings-and-alignment)
5. [Long strings](https://github.com/tdwright/contabs/wiki/5.-Handling-long-strings)

## Contributing

If you're interested in contributing, we'd love to welcome you. Check out our [contributing guidelines](CONTRIBUTING.md) which should contain everything you need to make a successful contribution.


## Background

ConTabs started life as an excuse for me (tdwright) to explore some bits and pieces. [I am writing up my experiences on my blog.](http://blog.tdwright.co.uk/series/modern-dotnet-dev-contabs/)
