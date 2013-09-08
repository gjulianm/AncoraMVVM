# AncoraMVVM

## What is this

This is a small toolkit for making Windows Phone apps, and in the future Windows apps too. I've been "developing" it when I needed something for my Windows Phone apps. Thus, this project includes some utilities, MVVM-related classes and some other useful things.

## Why on earth did you create yet another toolkit

Because every other toolkit I found out there didn't exactly fit my needs. Either they weren't written targeting PCL, or they needed modifications to include things I needed.

## Ok, so is there any cool thing in this?

I didn't searched to see if anyone had these things already, I suppose I'm not the first. But there's a list of some, IMHO, neat things that I made:

* ViewModelNavigator. Don't mess with pages URL anymore. `Navigator.Navigate<YourViewModel>` will manage everything automatically.
* Automatically create and set the ViewModel in the page. Just set the attribute `[ViewModel(typeof(YourViewModel)]` on the page class definition, and add a line in App.xaml.cs.
* A SortedFilteredObservable collection (I could have come up with a longer name) which supports live sorting and filtering, everything with notifications. A nice, portable subsitute of CollectionViewSource.
* A ConfigurationManager. I'm not very convinced of the approach, but it's useful to ensure type safety, it comes with a caché to avoid calling always to IsolatedStorage, and you don't have to mess up with key strings. 
* A global ProgressIndicator. [Not mine, though](http://www.jeff.wilcox.name/2011/07/creating-a-global-progressindicator-experience-using-the-windows-phone-7-1-sdk-beta-2/)

## Things to be done

I have a lot of pending things on this, but these are the most important:

* Expand test coverage. Specially on the Windows Phone implementations of the interfaces: they don't have unit tests.
* Documentation, documentation, documentation. The code is (I think) pretty self-explanatory, but documenting the public methods would be useful. 
* Examples and instructions: there're some configuration details that can't be inferred from the code, these need to be explained.
