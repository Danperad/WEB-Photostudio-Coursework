import React from 'react';
import {Box, Container, Link, Modal, Typography} from "@mui/material";
import Login from "./Login";
import {useNavigate} from "react-router-dom";

interface AuthProps {
  open: boolean,
  handlerClose: () => void
}

function AuthModal(props: AuthProps) {
  const navigate = useNavigate();

  return (
    <Modal open={props.open} onClose={props.handlerClose}>
      <Box sx={{
        position: 'absolute' as const,
        bgcolor: 'background.default',
        right: '1rem',
        top: '1rem',
        width: '20rem',
        padding: 1.75,
        borderRadius: 2
      }}>
        <Login/>
        <Container sx={{marginTop: 0.5}}>
          <Typography align={"center"} fontSize={18}>Если: <Link onClick={() => {
            navigate('register');
            props.handlerClose();
          }} sx={{cursor: 'pointer'}}>Регистрация</Link>
          </Typography>
        </Container>
      </Box>
    </Modal>
  )
}

export default AuthModal;
