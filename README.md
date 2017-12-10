NHazm
=====
[![Build status](https://ci.appveyor.com/api/projects/status/el9vqyfy45vxsu1w?svg=true)](https://ci.appveyor.com/project/mojtaba-khallash/nhazm)
[![codecov](https://codecov.io/gh/mojtaba-khallash/NHazm/branch/master/graph/badge.svg)](https://codecov.io/gh/mojtaba-khallash/NHazm)
[![Dependency Status](https://www.versioneye.com/user/projects/58e6585c24ef3e003b526e78/badge.svg?style=flat)](https://www.versioneye.com/user/projects/58e6585c24ef3e003b526e78)

[![GitHub release](https://img.shields.io/github/release/mojtaba-khallash/NHazm.svg)](https://github.com/mojtaba-khallash/NHazm/releases)
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

* You can download  [pre-trained tagger](https://www.dropbox.com/s/rfbo13u11wkh0yu/resources.zip?dl=0) and [parser models](https://www.dropbox.com/s/vuchhc4tlriiudk/resources-extra.zip?dl=0) for persian and put these models in the `Resources` folder of your project.
