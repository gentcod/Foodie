import { useState } from 'react';
import SearchPreview from '../search-preview/search-preview.component';
import { SearchBar, SearchBarContainer } from './search.style';

const Search = () => {
   const [searchString, setSearchString] = useState("")

   const searchHandler = (event: React.FormEvent<HTMLInputElement>) => {
      event.preventDefault()
      setSearchString(`?search=${event.currentTarget.value}`)
   }

   return (
      <>
         <SearchBarContainer>
            <SearchBar placeholder='Enter keyword' onChange={searchHandler}/>
            <SearchPreview searchString={searchString}/>
         </SearchBarContainer>
      </>
   )
}

export default Search;