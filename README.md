# HackerNews Console Application
 WebScraper for pulling data out of HackerNews

How to use:

    1) Open a cli and navigate to the HackerNews.exe (from the application root) HackerNews-Console-Application/bin/Release/netcoreapp3.1/win10-x64/publish
    
    2) Execute by entering HackerNews.exe followed by args if desired (default will return 1 page). Use ./HackerNews.exe if using the bash shell. 
    
    3) --help argument is there for displaying available args/options 

notes:
    - If some posts are missing from the response, this is because they're job vacancies/adverts - so delibertatly didn't match them in the Regular Expression.
