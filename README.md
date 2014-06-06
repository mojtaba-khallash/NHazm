NHazm
=====

A C# version of [Hazm](https://github.com/sobhe/hazm) (Python library for digesting Persian text)

+ Text cleaning
+ Sentence and word tokenizer
+ Word lemmatizer
+ POS tagger
+ Dependency parser
+ Corpus readers for [Hamshahri](http://ece.ut.ac.ir/dbrg/hamshahri/) and [Bijankhan](http://ece.ut.ac.ir/dbrg/bijankhan/)
+ [![Build Status](https://travis-ci.org/sobhe/hazm.png)](https://travis-ci.org/sobhe/hazm)


## Requirements
* [Stanford Log-linear Part-Of-Speech Tagger for .NET](http://sergey-tihon.github.io/Stanford.NLP.NET/StanfordPOSTagger.html): can be installed from NuGet: 
> Install-Package Stanford.NLP.POSTagger

* [MaltParser for .NET](http://sergey-tihon.github.io/MaltParser.NET/) can be installed from NuGet: 
> Install-Package MaltParser

* You can download [pre-trained tagger and parser models](http://dl.dropboxusercontent.com/u/90405495/resources.zip) for persian and put these models in the `resources` folder of your project.
