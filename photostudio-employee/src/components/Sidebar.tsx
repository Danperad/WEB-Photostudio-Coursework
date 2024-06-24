import {Box, Button, List} from "@mui/material";
import {ContactMail, Menu, Person, LibraryBooks, StickyNote2, Photo} from '@mui/icons-material';
import {useNavigate} from "react-router-dom";
import {useState} from "react";

export default function Sidebar() {
  const [isFullSize, setFullSize] = useState<boolean>(false);

  const navigate = useNavigate();

  return (
    <Box className={`sticky top-0 left-0 ${isFullSize ? `w-40` : `w-16`} flex items-center h-screen bg-black`}>
      <List className={"align-middle grid gap-5 w-screen"}>
        <Button aria-label={"menu"} onClick={() => {
          setFullSize(prevState => !prevState);
        }}>
          <Menu color={'primary'}/>
          {isFullSize && `Уменьшить`}
        </Button>
        <Button variant={"text"} onClick={() => {
          navigate("clients")
        }}>
          <Person color={'primary'}/>
          {isFullSize && `Клиенты`}
        </Button>
        <Button variant={"text"} onClick={() => {
          navigate("orders")
        }}>
          <LibraryBooks color={'primary'}/>
          {isFullSize && `Заявки`}
        </Button>
        <Button variant={"text"} onClick={() => {
          navigate("employees")
        }}>
          <ContactMail color={'primary'}/>
          {isFullSize && `Сотрудники`}
        </Button>
        <Button variant={"text"} onClick={() => {
          navigate("services")
        }}>
          <Photo color={'primary'}/>
          {isFullSize && `Услуги`}
        </Button>
        <Button variant={"text"} onClick={() => {
          navigate("employeesServices")
        }}>
          <StickyNote2 color={'primary'}/>
          {isFullSize && `Мои услуги`}
        </Button>
      </List>
    </Box>
  )
}
