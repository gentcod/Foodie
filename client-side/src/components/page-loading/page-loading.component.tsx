import  {ReactComponent as DishIcon } from '../../assets/dish.svg';

import { IconContainer, Overlay } from './page-loading.style';

const Loading = () => {
   return (
      <Overlay>
         <IconContainer>
            <DishIcon/>
         </IconContainer>
      </Overlay>
   )
};

export default Loading;