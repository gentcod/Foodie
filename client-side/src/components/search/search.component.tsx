import { useState } from 'react';
import SearchPreview from '../search-preview/search-preview.component';
import { SearchBar, SearchBarContainer } from './search.style';
// import { RecipeParams } from '../../app/models/recipes';

const Search = () => {
   const [searchString, setSearchString] = useState("")
   // const [searchParams, setSearchParams] = useState({} as RecipeParams)

   const searchHandler = (event: React.FormEvent<HTMLInputElement>) => {
      event.preventDefault()
      setSearchString(`?search=${event.currentTarget.value}`)
   }

   return (
      <>
         <SearchBarContainer>
            <SearchBar placeholder='Enter keyword' onChange={searchHandler} name='search'/>
            <SearchPreview searchString={searchString}/>
         </SearchBarContainer>
      </>
   )
}

export default Search;