import SearchPreview from '../search-preview/search-preview.component';
import { SearchBar, SearchBarContainer } from './search.style';

const Search = () => {
   return (
      <>
         <SearchBarContainer>
            <SearchBar placeholder='Enter keyword'/>
            <SearchPreview/>
         </SearchBarContainer>
      </>
   )
}

export default Search;