# Foodie-V2
A food recipe application with an API built using C#, .Net and client side/frontend built using React

## Scripts guide:

To get the necessary dependencies, run:

`dotnet retore`
##

To run the application, navigate into the /API directory and then you can run:

`dotnet wtach run`
##

## Other dependencies:

- Create a dev-data dir, and add two files ~recipes.json~ and ~restaurant.json~. This two files would be used to create the recipe and restaurant data to be stored in the database
### Script guide:

`mkdir dev-data`
`touch recipes.json`
`touch restaurant.json`

recipe.json should contain fields for - { Name, Ingredients, Description, ImageSrc, CookTime, Origin, Category} to match the Object props or Model props for Recipe, reference ~Models/Recipe.cs~ for more details
restaurant.json should contain fields for - { Name, Location, ImageSrc, Geolocation { Latitude, Longitude } } to match the Object props or Model props for Recipe, reference ~Models/Recipe.cs~ for more details
