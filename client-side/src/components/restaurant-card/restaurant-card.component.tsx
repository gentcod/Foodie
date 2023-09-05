import { RestaurantContainer, RestaurantContent, RestaurantImage, RestaurantImageContainer, RestaurantInnerLeft, RestaurantInnerRight } from './restaurant-card.style';

type RestaurantProps = {
   name: string;
   imgSrc: string;
   location: string;
}

const RestaurantCard = ({name, imgSrc, location}: RestaurantProps) => {
   return(
      <RestaurantContainer>
      <RestaurantInnerLeft>
         <RestaurantImageContainer>
            <RestaurantImage src={imgSrc}/>
         </RestaurantImageContainer>
      </RestaurantInnerLeft>
      <RestaurantInnerRight>
         <RestaurantContent>{name}</RestaurantContent>
         <RestaurantContent>{location}</RestaurantContent>
      </RestaurantInnerRight>
   </RestaurantContainer>
   )
}

export default RestaurantCard;