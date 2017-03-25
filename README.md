NHazm
=====
[![Build status](https://ci.appveyor.com/api/projects/status/el9vqyfy45vxsu1w?svg=true)](https://ci.appveyor.com/project/mojtaba-khallash/nhazm)
[![codecov](https://codecov.io/gh/mojtaba-khallash/NHazm/branch/master/graph/badge.svg)](https://codecov.io/gh/mojtaba-khallash/NHazm)
[![Libraries.io for GitHub](https://img.shields.io/librariesio/github/mojtaba-khallash/NHazm.svg)](https://libraries.io/github/mojtaba-khallash/NHazm)
[![License](http://img.shields.io/:license-mit-blue.svg)](http://badges.mit-license.org)

A C# version of [Hazm](https://github.com/sobhe/hazm) (Python library for digesting Persian text)

+ Text cleaning
+ Sentence and word tokenizer
+ Word lemmatizer
+ POS tagger
+ Dependency parser
+ Corpus readers for:
   * [Hamshahri](http://ece.ut.ac.ir/dbrg/hamshahri/)
   * [Bijankhan](http://ece.ut.ac.ir/dbrg/bijankhan/)
   * [Persica](https://sourceforge.net/projects/persica/)
   * [Verb Valency](http://dadegan.ir/catalog/pervallex)


## Requirements
* [Stanford Log-linear Part-Of-Speech Tagger for .NET](http://sergey-tihon.github.io/Stanford.NLP.NET/StanfordPOSTagger.html): can be installed from NuGet: 
> Install-Package Stanford.NLP.POSTagger

* [MaltParser for .NET](http://sergey-tihon.github.io/MaltParser.NET/) can be installed from NuGet: 
> Install-Package MaltParser

* You can download  [pre-trained tagger](http://dl.dropboxusercontent.com/u/90405495/resources.zip) and [parser models](http://dl.dropboxusercontent.com/u/90405495/resources-extra.zip) for persian and put these models in the `Resources` folder of your project.
