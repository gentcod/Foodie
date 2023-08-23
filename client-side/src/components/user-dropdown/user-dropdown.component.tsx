import { DropdownContainer, UserDetailsContainer, UserModList, UserModListItem, UserName, UserPicture } from "./user-dropdown.style";

type ModListProp = {
   id: number;
   item: string;
   link: string;
}[]

const modList: ModListProp = [
   {
      id: 0,
      item: "profile",
      link: '',
   },
   {
      id: 1,
      item: "favorites",
      link: '',
   },
   {
      id: 2,
      item: "logout",
      link: '',
   },
]

type UserProp = {
   name: string;
   imgSrc: string;
}


const UserDropdown = ({name, imgSrc}: UserProp) => {
   return (
      <DropdownContainer>
         <UserDetailsContainer>
            <UserPicture src={imgSrc}/>
            <UserName>{name}</UserName>
         </UserDetailsContainer>
         <UserModList>
            {
               modList.map(el => (
                  <UserModListItem to={el.link} key={el.id}>{el.item}</UserModListItem>
               ))
            }
         </UserModList>
      </DropdownContainer>
   )
};

export default UserDropdown;