import { HeaderContainer, Header } from "./heading.style";

type HeadingProp = { 
   text: string;
}

const Heading = ({text}: HeadingProp) => {
   return (
      <HeaderContainer>
         <Header>{text}</Header>
      </HeaderContainer>
   )
}

export default Heading;