import { Container, HeaderImage, HeaderTitle } from './header.style';

const Header = () => {
   return (
      <Container to={'/'}>
         <HeaderImage src='images/foodie.png'/>
         <HeaderTitle>Foodie</HeaderTitle>
      </Container>
   )
}

export default Header;