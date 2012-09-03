Your Last About Dialog
======================

Quickstart
----------
1.) Open Content/About/Data.xml and enter your custom values
2.) Wherever you want on an existing page, put the following line to navigate to the about page:

NavigationService.Navigate(new Uri("/YourLastAboutDialog;component/AboutPage.xaml", UriKind.Relative));

Styles
------
Open and edit Content/About/AboutStyles.xaml to apply your custom styles to the about page.

More
----
YLAD supports a huge set of features like:

* Localization
* Custom items with text or XAML content
* Displaying remote content (with local fallback)
* Pre-selecting items
* Support for trial apps
* ...

To learn more about these features, please take a look at the official documentation:

http://ylad.codeplex.com/documentation

For feature requests or bug reports also use the project page:

http://ylad.codeplex.com

If you think YLAD is useful, please leave a vote or tweet about it - thanks :)

--
Peter Kuhn ("Mister Goodcat")
Twitter: @Mister_Goodcat (http://twitter.com/#!/Mister_Goodcat)
Blog: http://www.pitorque.de/MisterGoodcat