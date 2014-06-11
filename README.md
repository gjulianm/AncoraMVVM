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
* The [AncoraMVVM.Rest](https://github.com/gjulianm/AncoraMVVM/wiki/AncoraMVVM.Rest) base client.
* A generator to create automatically settings pages in your app.

## Things to be done

I have a lot of pending things on this, but these are the most important:

* Expand test coverage. Specially on the Windows Phone implementations of the interfaces: they don't have unit tests.
* Documentation, documentation, documentation. The code is (I think) pretty self-explanatory, but documenting the public methods would be useful. 
* Examples and instructions: there're some configuration details that can't be inferred from the code, these need to be explained.

# How to's

## Windows Phone

To include AncoraMVVM in your Windows Phone app, you'll need to include some code in your `App.xaml.cs` file, specifically in the constructor:

			// Initialize the ViewModel locator (automatic setup of viewmodels in each page)
 			var locator = new PhoneViewModelLocator();
            locator.InitializeAndFindPages(RootFrame);
	
			// Setup the global progress indicator.
            ((GlobalProgress)Dependency.Resolve<IProgressIndicator>()).Initialize(RootFrame);
			
			// Setup navigation with viewmodels.
            var navigator = Dependency.Resolve<INavigationService>() as ViewModelNavigationService;

            if (navigator == null)
                Debug.WriteLine("The INavigationService configured is not a ViewModelNavigationService (or there isn't a INavigationService registered).");
            else
                navigator.Initialize();

### ViewModelNavigationService

For the ViewModelNavigator to work correctly, you should specify the root namespace your app is using. This is because of how the navigator handles navigation requests.

On initializaton, ViewModelNavigationService finds every type with the `ViewModel` attribute. Let's suppose you attribute your type `Myapp.Windows.Phone.Views.Settings` with the attribute. The inferred URI for the page will be `/Views/Settings`. Basically, we remove the root namespace (`MyApp.Windows.Phone`) and we suppose that each child namespace represents a directory. 

So, this leads us to the two requirements to use `ViewModelNavigationService`:

* In `AssemblyInfo.cs`, set the attribute `[assembly: RootNamespace("myrootnamespace")]`.
* For each page, namespace must match folder structure. If your page is inside a `Views` folder, it should be inside of a `Views` namespace, and viceversa. 

