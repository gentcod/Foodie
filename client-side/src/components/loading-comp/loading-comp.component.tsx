import Loading from "../page-loading/page-loading.component";
import {
  Container,
  ContainerContent,
  ContainerContentMid,
  ContainerContentShort,
  ContainerInnerLeft,
  ContainerInnerRight,
  LoadingContainer,
} from "./loading-comp.style";

const containerNumber = [{ id: 1 }, { id: 2 }, { id: 3 }, { id: 4 }];

const LoadingComp = () => {
  return (
    <Container>
      {containerNumber.map((el) => (
        <LoadingContainer key={el.id}>
          <ContainerInnerLeft>
            <Loading />
          </ContainerInnerLeft>
          <ContainerInnerRight>
            <ContainerContentMid />
            <ContainerContentShort />
            <ContainerContent />
            <ContainerContentMid />
          </ContainerInnerRight>
        </LoadingContainer>
      ))}
    </Container>
  );
};

export default LoadingComp;
