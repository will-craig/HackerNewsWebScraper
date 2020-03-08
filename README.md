# HackerNews Console Application
 WebScraper for pulling data out of HackerNews

Compiled with win64 runtimes included, to allow running in self containted mode.

Only external library included is Newtonsoft Json; Included it because it makes serializing object collections a piece of cake.

How to use:

    1) Open a cli and navigate to the HackerNews.exe (from the application root) HackerNews-Console-Application/bin/Release/netcoreapp3.1/
    
    2) Execute by entering HackerNews.exe followed by args if desired (default will return 1 page). Use ./HackerNews.exe if using the bash shell. 
    
    3) --help argument is their for displaying available args/options 

notes:
    - If some posts are missing from the response, this is because they're job vacancies/adverts - so delibertatly didnt match them in the Regular Expression.
