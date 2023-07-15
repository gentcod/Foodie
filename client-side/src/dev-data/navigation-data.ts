type NavItemsLeft = {
   id: number;
   title: string;
}[]

type NavItemsRight = {
   id: number;
   title: string;
   icon: string;
}[]

export const navItemsLeft: NavItemsLeft = [
   {
      id: 0,
      
      title: 'recipes'
   },
   {
      id: 1,
      title: 'restaurants'
   },
   {
      id: 3,
      title: 'easy makes'
   },
]

export const navItemsRight: NavItemsRight = [
   {
      id: 0,
      title: 'search',
      icon: 'icons/search.svg'
   },
   {
      id: 1,
      title: 'user',
      icon: 'icons/chef.svg'
   },
   {
      id: 3,
      title: 'bookmarks',
      icon: 'icons/bookmark.svg'
   },
]