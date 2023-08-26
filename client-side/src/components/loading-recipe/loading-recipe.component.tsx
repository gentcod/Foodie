import Loading from '../loading/loading.component';
import { Container, ContainerCookTime, ContainerDescription, ContainerInnerLeft, ContainerInnerRight, ContainerName, ContainerOrigin, LoadingContainer } from './loading-recipe.style';

const containerNumber = [
   {id: 1},
   {id: 2},
   {id: 3},
   {id: 4}
]

const LoadingRecipe = () => {
   return (
      <Container>
         {containerNumber.map(el => (
            <LoadingContainer key={el.id}>
               <ContainerInnerLeft>
                  <Loading/>
               </ContainerInnerLeft>
               <ContainerInnerRight>
                  <ContainerName/>
                  <ContainerCookTime/>
                  <ContainerOrigin/>
                  <ContainerDescription/>
               </ContainerInnerRight>
            </LoadingContainer>
         ))}
      </Container>
   )
}

export default LoadingRecipe;