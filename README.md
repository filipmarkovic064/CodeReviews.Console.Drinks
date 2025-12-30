# Drinks Info
 
### Challenges
    Quite a hop in difficulty from the previous few projects as i have some experience with programming from before but i have never worked with APIs, but im loving it. 
<ol>
    <li> Biggest challenge right now is how to read Ingredients (1-15) and Measurements (1-15) in a nice and organised way without having to hard-code them in </li>
    <li> I also learned a lot about async methods where i struggled to get my menu to sync up properly when trying to initiate it inside async methods, so i figured out that im supposed to use async methods to get the values from services running in the background and then get the menu/info to pop up in a seperate method after ive gotten the value from the API for example. </li>
    <li> Took me a very long time to figure out how to show an image in a console app, most of the info i found online was for Windows Forms </li>
</ol>

### What i learned:

<ol>
    <li>Learning quite a lot about APIs and how to call them using .NETs HttpClient.
    I have been slowly using the microsofts example project and slowly implementing it in my own project (<url> https://learn.microsoft.com/en-us/dotnet/csharp/tutorials/console-webapiclient </url>)</li>
    <li>Learned how to open Images in a console app</li>
    <li>Made sure to follow feedback from previous project: Cleaned up Using-statements that im not Using :D, organised files into directories, not using main, code cleanup turned on, tried not to overfill a single method</li>
</ol>

### How it works:

<ol>
    <li>When opening the app user is greeted with the drink Categories along with Favorites and Exit Options. I was going to make a "Categories" option so the menu looks nicer and tidier but the requirments said that the user should be greeted with categories.</li>
    <li> I didnt use enums for menu choices (Favorites, Exit) because i didnt see the difference between using a string or enum since the user chooses by choosing the actual button</li>
    <li>Upon clicking on a category, the user is greeted with all the drinks in that category and if they click on a drink they get all the info around it, along with the options to add/remove it from favorites or to view the image of the drink</li>
    <li>I know that the requirments said that DB is not neccessary but i used it to save Views and Favorites</li>
</ol>

