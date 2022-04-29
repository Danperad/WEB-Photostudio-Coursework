import React from 'react';
import {AppBar, Button, ButtonGroup} from "@mui/material";
import '../assets/css/header.css';
import {Link} from "react-router-dom";

class Header extends React.Component {
	render() {
		return (
			<AppBar position={'fixed'} className={'header'}>
				<ButtonGroup variant={'contained'}>
					<Link to={"/"}><Button>Домой</Button></Link>
					<Link to={"login"}><Button>Вход</Button></Link>
					<Link to={"registration"}><Button>Регистрация</Button></Link>
				</ButtonGroup>
			</AppBar>
		);
	}
}

export default Header;
